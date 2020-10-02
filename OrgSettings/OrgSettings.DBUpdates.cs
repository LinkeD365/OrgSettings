using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using McTools.Xrm.Connection;
using XrmToolBox.Extensibility;

namespace LinkeD365.OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase
    {
        private XmlDocument _smneXml;

        private XmlDocument _linkeD365Xml;
        private void RemoveConfig(ConnectionDetail connection, List<OrgSetting> curOrgSettings)
        {
            var removeOs = curOrgSettings.Single(os => os.Name == grpAttribute.Tag.ToString());
            curOrgSettings.Remove(removeOs);

            ClearConfig(connection);
            UpdateConfig(connection, curOrgSettings);

        }

        private void ClearConfig(ConnectionDetail connection)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Clearing Config",
                Work = (worker, args) =>
                {
                    Entity orgEntity = new Entity("organization", _orgGuid);
                    orgEntity["orgdborgsettings"] = string.Empty;
                    connection.ServiceClient.Update(orgEntity);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
        }

        private void UpdateConfig(ConnectionDetail connection, List<OrgSetting> curOrgSettings)
        {
            _ai.WriteEvent("Updating Config", curOrgSettings.Count(os => !String.IsNullOrEmpty(os.NewSetting)));
            string updateString = "<orgSettings>";
            foreach (OrgSetting os in curOrgSettings)
            {
                updateString += "<" + os.Name + ">" +
                    (String.IsNullOrEmpty(os.NewSetting)
                    ? os.CurrentSetting
                    : os.NewSetting)
                    + "</" + os.Name + ">";
            }

            updateString += "</orgSettings>";

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating Config",
                Work = (worker, args) =>
                {
                    Entity orgEntity = new Entity("organization", _orgGuid);
                    //orgEntity["organizationid"] = orgGuid.ToString();
                    orgEntity["orgdborgsettings"] = updateString;
                    connection.ServiceClient.Update(orgEntity);
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            });
            LoadAllConfigs();
            //  ExecuteMethod(LoadAllConfigs());
        }

        private void LoadSingleConfig()
        {
            LoadConfig(ConnectionDetail, _primaryOrgSettings);
            UpdateGroups();

            // LoadXml();
        }
        private void LoadAllConfigs()
        {
            LoadConfig(ConnectionDetail, _primaryOrgSettings);

            foreach (var connectionDetail in AdditionalConnectionDetails)
            {
                LoadConfig(connectionDetail, _secondaryOrgSettings,true);


            }
            UpdateGroups();

        }



        private void LoadXml()
        {
            if (this._smneXml == null)
            {
                string xmlSeanMcNe = new WebClient().DownloadString(
                    @"https://raw.githubusercontent.com/seanmcne/OrgDbOrgSettings/master/mspfedyn_/OrgDbOrgSettings/Settings.xml");
                _smneXml = new XmlDocument();
                _smneXml.LoadXml(xmlSeanMcNe);
            }

            _fullList.Clear();

            _fullList.AddRange(from XmlNode childNode in _smneXml.SelectSingleNode("defaultOrgSettings")
                              where childNode.Name == "orgSetting" && childNode.Attributes["isOrganizationAttribute"].Value == "false"
                              select new OrgSetting()
                              {
                                  Name = childNode.Attributes["name"].Value,
                                  Description = childNode.Attributes["description"].Value,
                                  MinVersion = childNode.Attributes["minSupportedVersion"].Value,
                                  MaxVersion = childNode.Attributes["maxSupportedVersion"].Value,
                                  OrgAttribute = Convert.ToBoolean(childNode.Attributes["isOrganizationAttribute"].Value),
                                  MinValue = childNode.Attributes["min"].Value,
                                  MaxValue = childNode.Attributes["max"].Value,
                                  DefaultValue = String.IsNullOrEmpty(childNode.Attributes["defaultValue"].Value)
                                    ? ""
                                    : childNode.Attributes["defaultValue"].Value,
                                  Type = childNode.Attributes["settingType"].Value,
                                  Url = childNode.Attributes["supportUrl"].Value,
                                  UrlTitle = childNode.Attributes["urlTitle"].Value,
                                  CurrentSetting = _primaryOrgSettings.FindIndex(os => os.Name.ToString() == childNode.Attributes["name"].Value) == -1
                                    ? ""
                                    : _primaryOrgSettings.Find(os => os.Name.ToString() == childNode.Attributes["name"].Value).CurrentSetting,
                                  SecondaryCurrentSetting = _secondaryOrgSettings.FindIndex(os => os.Name.ToString() == childNode.Attributes["name"].Value) == -1
                                      ? ""
                                      : _secondaryOrgSettings.Find(os => os.Name.ToString() == childNode.Attributes["name"].Value).CurrentSetting
                              });

            try
            {
                if (_linkeD365Xml == null)
                {
#if DEBUG

                    _linkeD365Xml = new XmlDocument();
                    _linkeD365Xml.Load("E:\\Live\\OrgSettings\\LinkeD65OrgSettings.xml");
#else
                        string xmlLinkeD365 =
 new WebClient().DownloadString("https://raw.githubusercontent.com/LinkeD365/OrgSettings/master/LinkeD65OrgSettings.xml");
                        linkeD365XML = new XmlDocument();
                        linkeD365XML.LoadXml(xmlLinkeD365);
#endif
                }

                foreach (XmlNode childNode in _linkeD365Xml.SelectSingleNode("orgSettings").ChildNodes)
                {
                    OrgSetting orgSetting = _fullList.Find(os => os.Name == childNode.Attributes["name"].Value);
                    if (orgSetting != null)
                    {
                        orgSetting.LinkeD365Description = childNode.Attributes["description"].Value;
                        orgSetting.LinkeD365Url = childNode.Attributes["url"].Value;
                    }
                }
            }
            catch (Exception err)
            {
                LogError("Can not retrieve LinkeD365 data", err.Message);
            }
        }

        /// <summary>
        /// Retrieves Sean McNellis file & displays. If file from LinkeD365 contains descirption, adds this
        /// </summary>
        private void LoadConfig(ConnectionDetail connection, List<OrgSetting> orgSettings,  bool secondary = false)
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting config",
                Work = (worker, args) =>
                {
                    QueryExpression qe = new QueryExpression("organization") { ColumnSet = new ColumnSet("orgdborgsettings") };
                    args.Result = connection.ServiceClient.RetrieveMultiple(qe);

                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Error != null)
                    {
                        MessageBox.Show(args.Error.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    var result = args.Result as EntityCollection;
                    if (result == null) return;

                    var orgRow = result.Entities.First();
                    _orgGuid = orgRow.Id;
                    string orgString = orgRow.GetAttributeValue<string>("orgdborgsettings");
                    txtCurrentXml.Text = orgString;
                    orgSettings.Clear();
                    if (string.IsNullOrEmpty(orgString)) orgSettings.Clear();
                    else
                    {
                        XmlDocument orgXml = new XmlDocument();
                        orgXml.LoadXml(orgString);
                        orgSettings.AddRange(from XmlNode childNode in orgXml.ChildNodes[0].ChildNodes
                                             let orgValue = new OrgSetting()
                                             { Name = childNode.Name, CurrentSetting = childNode.InnerText }
                                             select orgValue);
                    }

                    LoadXml();
                    _filteredList.Clear();
                    //int[] orgVersion = ConnectionDetail.OrganizationVersion.Split('.').Select(int.Parse).ToArray();
                    double orgVersion = double.Parse(string.Join("", connection.OrganizationVersion.Split('.').Select(ov => int.Parse(ov).ToString("D6"))));
                    foreach (OrgSetting os in _fullList)
                    {
                        double max = double.Parse(string.Join("", os.MaxVersion.Split('.').Select(osmax => int.Parse(osmax).ToString("D6"))));
                        double min = double.Parse(string.Join("", os.MinVersion.Split('.').Select(osmin => int.Parse(osmin).ToString("D6"))));

                        if (min <= orgVersion && max >= orgVersion) _filteredList.Add(os);
                    }

                    //if (secondary)
                    //{
                    //    gvSecondary.DataSource = null;
                    //    gvSecondary.DataSource = filteredList;

                    //    InitGridView(gvSecondary, true);
                    //}
                    //else
                    //{
                        gvSettings.DataSource = null;
                        gvSettings.DataSource = _filteredList;

                        InitGridView();
                    //}
                }
            });
        }

        private void UpdateManualComplete(ConnectionDetail connection)
        {
            try
            {
                ClearConfig(connection);
                Entity orgEntity = new Entity("organization", _orgGuid);

                orgEntity["orgdborgsettings"] = txtOverride.Text;
                connection.ServiceClient.Update(orgEntity);

                LoadAllConfigs();

                ShowInfoNotification("Manual override of configuration has been complete", null);

                txtOverride.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}