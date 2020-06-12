using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml;
using XrmToolBox.Extensibility;

namespace LinkeD365.OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase
    {
        private void RemoveConfig()
        {
            var removeOs = curOrgSettings.Single(os => os.name == grpAttribute.Text);
            curOrgSettings.Remove(removeOs);

            ExecuteMethod(ClearConfig);

            ExecuteMethod(UpdateConfig);
        }

        private void ClearConfig()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Clearing Config",
                Work = (worker, args) =>
                {
                    Entity orgEntity = new Entity("organization", orgGuid);
                    orgEntity["orgdborgsettings"] = string.Empty;
                    Service.Update(orgEntity);
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

        private void UpdateConfig()
        {
            string updateString = "<orgSettings>";
            foreach (OrgSetting os in curOrgSettings)
            {
                updateString += "<" + os.name + ">" +
                    (String.IsNullOrEmpty(os.newSetting)
                    ? os.currentSetting
                    : os.newSetting)
                    + "</" + os.name + ">";
            }

            updateString += "</orgSettings>";

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating Config",
                Work = (worker, args) =>
                {
                    Entity orgEntity = new Entity("organization", orgGuid);
                    //orgEntity["organizationid"] = orgGuid.ToString();
                    orgEntity["orgdborgsettings"] = updateString;
                    Service.Update(orgEntity);
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

            ExecuteMethod(LoadConfig);
        }

        private void LoadConfig()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting config",
                Work = (worker, args) =>
                {
                    QueryExpression qe = new QueryExpression("organization") { ColumnSet = new ColumnSet("orgdborgsettings") };
                    args.Result = Service.RetrieveMultiple(qe);
                    //{
                    //    TopCount = 50
                    //});
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
                    orgGuid = orgRow.Id;
                    string orgString = orgRow.GetAttributeValue<string>("orgdborgsettings");
                    txtCurrentXml.Text = orgString;
                    if (String.IsNullOrEmpty(orgString)) curOrgSettings = new List<OrgSetting>();
                    else
                    {
                        XmlDocument orgXML = new XmlDocument();
                        orgXML.LoadXml(orgString);
                        curOrgSettings = (from XmlNode childNode in orgXML.ChildNodes[0].ChildNodes
                                          let orgValue = new OrgSetting() { name = childNode.Name, currentSetting = childNode.InnerText }
                                          select orgValue).ToList();
                    }
                    string xmlSeanMcNe = new WebClient().DownloadString(@"https://raw.githubusercontent.com/seanmcne/OrgDbOrgSettings/master/mspfedyn_/OrgDbOrgSettings/Settings.xml");
                    XmlDocument smneXML = new XmlDocument();
                    smneXML.LoadXml(xmlSeanMcNe);

                    List<OrgSetting> fullList = new List<OrgSetting>();

                    fullList.AddRange(from XmlNode childNode in smneXML.SelectSingleNode("defaultOrgSettings")
                                      where childNode.Name == "orgSetting" && childNode.Attributes["isOrganizationAttribute"].Value == "false"
                                      select new OrgSetting()
                                      {
                                          name = childNode.Attributes["name"].Value,
                                          description = childNode.Attributes["description"].Value,
                                          minVersion = childNode.Attributes["minSupportedVersion"].Value,
                                          maxVersion = childNode.Attributes["maxSupportedVersion"].Value,
                                          orgAttribute = Convert.ToBoolean(childNode.Attributes["isOrganizationAttribute"].Value),
                                          minValue = childNode.Attributes["min"].Value,
                                          maxValue = childNode.Attributes["max"].Value,
                                          defaultValue = String.IsNullOrEmpty(childNode.Attributes["defaultValue"].Value)
                                            ? ""
                                            : childNode.Attributes["defaultValue"].Value,
                                          type = childNode.Attributes["settingType"].Value,
                                          url = childNode.Attributes["supportUrl"].Value,
                                          urlTitle = childNode.Attributes["urlTitle"].Value,
                                          currentSetting = curOrgSettings.FindIndex(os => os.name.ToString() == childNode.Attributes["name"].Value) == -1
                                            ? ""
                                            : curOrgSettings.Find(os => os.name.ToString() == childNode.Attributes["name"].Value).currentSetting,
                                          newSetting = ""
                                      });

                    filteredList = new List<OrgSetting>();
                    //int[] orgVersion = ConnectionDetail.OrganizationVersion.Split('.').Select(int.Parse).ToArray();
                    double orgVersion = double.Parse(string.Join("", ConnectionDetail.OrganizationVersion.Split('.').Select(ov => int.Parse(ov).ToString("D6"))));
                    foreach (OrgSetting os in fullList)
                    {
                        double max = double.Parse(string.Join("", os.maxVersion.Split('.').Select(osmax => int.Parse(osmax).ToString("D6"))));
                        double min = double.Parse(string.Join("", os.minVersion.Split('.').Select(osmin => int.Parse(osmin).ToString("D6"))));

                        if (min <= orgVersion && max >= orgVersion) filteredList.Add(os);
                    }

                    gvSettings.DataSource = filteredList;

                    InitGridView();
                }
            });
        }

        private void UpdateManualComplete()
        {
            try
            {
                ExecuteMethod(ClearConfig);
                Entity orgEntity = new Entity("organization", orgGuid);

                orgEntity["orgdborgsettings"] = txtOverride.Text;
                Service.Update(orgEntity);

                ExecuteMethod(LoadConfig);

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