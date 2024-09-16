using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace LinkeD365.OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase
    {
        private APIConn bapConn;
        private ApiConnection apiConnection;
        private HttpClient bapClient;
        public APIConns apiConns;
        private EnvProps envProps;

        #region Load and Save Settings
        private void LoadEnvironmentConfig()
        {
            if (bapConn == null) Connect();
            if (bapClient == null) return;

            SettingsManager.Instance.Save(typeof(APIConns), apiConns);

            gridEnv.DataSource = null;
            gridEnv.AutoGenerateColumns = false;

            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Environment Settings",
                Work = (worker, args) =>
                {
                    string url = $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments?api-version=2021-04-01&$filter=properties/linkedEnvironmentMetadat/resourceId%20eq%20 {bapConn.Environment}";
                    HttpResponseMessage response = bapClient.GetAsync(url).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var jsonResponse = JObject.Parse(response.Content.ReadAsStringAsync().Result);

                        if (envProps == null)
                        {
#if DEBUG
                            //load json from file
                            envProps = JsonConvert.DeserializeObject<EnvProps>(System.IO.File.ReadAllText("C:\\Live\\OrgSettings\\Environment.json"));
#endif
                        }
                        envProps.EnvId = jsonResponse.SelectToken("value[0].name")?.ToString() ?? string.Empty;
                        foreach (EnvProp envProp in envProps.Properties)
                        {
                            envProp.OldValue = jsonResponse.SelectToken("value[0]." + envProp.Path.ToString())?.ToString() ?? string.Empty;
                            if (string.IsNullOrEmpty(envProp.EditableLink))
                            {
                                envProp.Enabled = envProp.Editable;
                            }
                            else
                            {
                                envProp.Enabled = envProp.Editable &&
                                    envProps.Properties.Any(prop => (prop.Name == envProp.EditableLink)
                                    && prop.Enabled
                                    && prop.Options.Any(opt =>
                                    opt.IntValue >= 1 && opt.Value == prop.OldValue)
                                    );
                            }
                        }
                        args.Result = envProps;
                    }
                    else
                    {
                        args.Result = null;
                    }
                },
                PostWorkCallBack = (args) =>
                {
                    if (args.Result != null)
                    {
                        gridEnv.DataSource = envProps.Properties;
                        SetupColumns();
                    }
                }

            });
        }

        private void SetupColumns()
        {
            if (gridEnv.Columns.Count > 0)
                return;

            var propLabel = new DataGridViewTextBoxColumn { DataPropertyName = "Label", Name = "Property", ReadOnly = true };
            gridEnv.Columns.Add(propLabel);

            var propCurrentValue = new DataGridViewTextBoxColumn { DataPropertyName = "Old", Name = "Current Value", ReadOnly = true };
            gridEnv.Columns.Add(propCurrentValue);


            //var btnConfig = new DataGridViewButtonColumn { HeaderText = " ", Name = "Config" };
            //btnConfig.Text = "c";
            //btnConfig.ReadOnly = true;
            //gridEnv.Columns.Add(btnConfig);

            var propNewValue = new DataGridViewTextBoxColumn { DataPropertyName = "New", Name = "New Value" };
            gridEnv.Columns.Add(propNewValue);
        }

        private void Connect()
        {
            if (bapConn == null)
            {
                ApiConnection apiConnection = new ApiConnection(apiConns, false);
                try
                {
                    //if (Graph)
                    //{
                    //    graphClient = apiConnection.GetClient();
                    //    if (graphClient != null)d
                    //        graphConn = apiConnection.graphConn;
                    //    else
                    //        return;

                    //    graphConn = apiConnection.graphConn;
                    //}
                    //else
                    //{
                    bapClient = apiConnection.GetClient();
                    if (bapClient != null)
                    {
                        bapConn = apiConnection.bapConn;
                    }
                    else return;
                    //}
                }
                catch (AdalServiceException adalExec)
                {
                    LogError("Adal Error", adalExec.GetBaseException());

                    if (adalExec.ErrorCode == "authentication_canceled")
                    {
                        return;
                    }

                    // ShowError(adalExec, "Error in
                    // connecting, please check details");
                }
                catch (Exception e)
                {
                    LogError("Error getting connection", e.Message);
                    // ShowError(e, "Error in connecting,
                    // please check entered details");
                    return;
                }
                //apiConnection = new ApiConnection(bapConn, false);
                //apiConnection.Connect();
            }
        }

        private void UpdateEnvConfig()
        {
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Updating Enviornment Settings ",
                Work = (w, args) =>
                {


                    if (envProps.Properties.Any(prop => string.IsNullOrEmpty(prop.Group) && !string.IsNullOrEmpty(prop.New)))
                    {
                        JObject jobj = new JObject();
                        string url = $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments/{envProps.EnvId}?api-version=2021-04-01";
                        foreach (EnvProp envProp in envProps.Properties.Where(prop => string.IsNullOrEmpty(prop.Type) && !string.IsNullOrEmpty(prop.New)))
                        {
                            jobj.ReplaceNested(envProp.Path, envProp.New);
                        }

                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/lifecycleOperations/11cd451b-67e9-4de6-84ec-3c365cbe88eb?api-version=2020-08-01
                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/34c5feef-3303-4b79-99d3-46895bca0ba5/governanceConfiguration?api-version=2020-08-01
                        // https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/34c5feef-3303-4b79-99d3-46895bca0ba5/governanceConfiguration?api-version=2020-08-01

                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/lifecycleOperations/268164ae-6a45-472c-af90-68929434cd0d?api-version=2020-08-01

                        // jobj.Add(envProp.Path, envProp.New);
                        //string json = $"{{\"{envProp.Path}\":\"{envProp.New}\"}}";
                        HttpRequestMessage patchRequest = new HttpRequestMessage(new HttpMethod("Patch"), url);
                        patchRequest.Content = new StringContent(jobj.ToString(), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = bapClient.SendAsync(patchRequest).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            args.Result = false;
                            return;
                        }
                    }

                    if (envProps.Properties.Any((prop => prop.Group == "Managed" && !string.IsNullOrEmpty(prop.New))))
                    {
                        JObject jobj = new JObject();
                        string url = $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/{envProps.EnvId}/governanceConfiguration?api-version=2021-04-01";
                        foreach (EnvProp envProp in envProps.Properties.Where(prop => prop.Group == "Managed")) //&& !string.IsNullOrEmpty(prop.New)))
                        {
                            jobj.ReplaceNested(envProp.UpdatePath, envProp.NewValue ?? envProp.OldValue ?? envProp.Default);
                        }
                        //if (envProps.Properties.Any(prop => prop.Name == "MgdMkrMrkDown" && !string.IsNullOrEmpty(prop.NewValue))   )
                        //{
                        //    jobj.ReplaceNested("settings.extendedSettings.makerOnboardingTimestamp", DateTime.UtcNow.ToString("R"));
                        //}
                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/lifecycleOperations/11cd451b-67e9-4de6-84ec-3c365cbe88eb?api-version=2020-08-01
                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/34c5feef-3303-4b79-99d3-46895bca0ba5/governanceConfiguration?api-version=2020-08-01
                        // https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments/34c5feef-3303-4b79-99d3-46895bca0ba5/governanceConfiguration?api-version=2020-08-01

                        //https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/lifecycleOperations/268164ae-6a45-472c-af90-68929434cd0d?api-version=2020-08-01

                        // jobj.Add(envProp.Path, envProp.New);
                        //string json = $"{{\"{envProp.Path}\":\"{envProp.New}\"}}";
                        HttpRequestMessage patchRequest = new HttpRequestMessage(new HttpMethod("Post"), url);
                        patchRequest.Content = new StringContent(jobj.ToString(), Encoding.UTF8, "application/json");
                        HttpResponseMessage response = bapClient.SendAsync(patchRequest).Result;
                        if (!response.IsSuccessStatusCode)
                        {
                            args.Result = false;
                            return;
                        }

                    }


                    args.Result = true;


                },
                PostWorkCallBack = (args) =>
                {
                }
            });
        }

        #endregion

        #region Events
        private void gridEnv_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            EnvProp envProp = (EnvProp)gridEnv.Rows[e.RowIndex].DataBoundItem;
            SetEnvConfigLabels(envProp);
        }

        private void radioEnvToggle_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if (!radioButton.Checked) return;

            EnvProp envProp = envProps.Properties.FirstOrDefault(prop => prop.Name == radioButton.Tag.ToString());

            if (radioButton.Name == radioEnvToggleTrue.Name)
            {
                envProp.NewValue = envProp.Options.FirstOrDefault(opt => opt.IntValue == 1).Value;
                
            }
            else
            {
                envProp.NewValue = envProp.Options.FirstOrDefault(opt => opt.IntValue == 0).Value;
            }
            CheckEnabled(envProp);
            gridEnv.Update();
            gridEnv.Refresh();
        }

        private void txtEnvTextNew_TextChanged(object sender, EventArgs e)
        {
            EnvProp envProp = envProps.Properties.FirstOrDefault(prop => prop.Name == txtEnvTextNew.Tag.ToString());
            envProp.NewValue = txtEnvTextNew.Text;
            gridEnv.Update();
            gridEnv.Refresh();
        }
        private void numEnvNew_ValueChanged(object sender, EventArgs e)
        {
            EnvProp envProp = envProps.Properties.FirstOrDefault(prop => prop.Name == numEnvNew.Tag.ToString());
            envProp.NewValue = numEnvNew.Value.ToString();
            gridEnv.Update();
            gridEnv.Refresh();
        }

        private void cboEnvDropNew_SelectionChangeCommitted(object sender, EventArgs e)
        {
            EnvProp envProp = envProps.Properties.FirstOrDefault(prop => prop.Name == cboEnvDropNew.Tag.ToString());
            envProp.NewValue = cboEnvDropNew.SelectedValue.ToString();
            gridEnv.Update();
            gridEnv.Refresh();
            CheckEnabled(envProp);
        }
        #endregion

        #region Methods
        private void CheckEnabled(EnvProp envProp)
        {
            foreach(EnvProp prop in envProps.Properties.Where(prop => prop.EditableLink == envProp.Name))
            {
                prop.Enabled = prop.Editable && envProp.Enabled && envProp.Options.Any(opt =>
                                    opt.IntValue >= 1 && opt.Value == envProp.NewValue);
            }
        }

        private void SetEnvConfigLabels(EnvProp envProp)
        {
            grpEnvSetting.Visible = false;
            panelToggle.Visible = false;
            splitEnvText.Visible = false;
            splitEnvNumber.Visible = false;
            splitEnvDropDown.Visible = false;

            grpEnvSetting.Text = !envProp.Editable ? "The setting for " + envProp.Label + " is not editable"
                : !envProp.Enabled ? "The setting for " + envProp.Label + " requires " + envProps.Properties.FirstOrDefault(prop => prop.Name == envProp.EditableLink).Label + " before editing"
                : "Setting for " + envProp.Label;
            grpEnvSetting.Enabled = envProp.Enabled;
            grpEnvSetting.Tag = envProp.Label;
            switch (envProp.Type)
            {
                case "Toggle":
                    radioEnvToggleTrue.Tag = envProp.Name;
                    radioEnvToggleFalse.Tag = envProp.Name;
                    radioEnvToggleFalse.Text = envProp.Options.FirstOrDefault(opt => opt.IntValue == 0).Label;
                    radioEnvToggleTrue.Text = envProp.Options.FirstOrDefault(opt => opt.IntValue == 1).Label;
                    //radioEnvToggleFalse.CheckedChanged -= radioEnvToggleFalse_CheckedChanged;
                    radioEnvToggleTrue.Checked = string.IsNullOrEmpty(envProp.NewValue) ? false : envProp.NewValue == envProp.Options.FirstOrDefault(opt => opt.IntValue == 1).Value;
                    radioEnvToggleFalse.Checked = string.IsNullOrEmpty(envProp.NewValue) ? false : envProp.NewValue == envProp.Options.FirstOrDefault(opt => opt.IntValue == 0).Value;
                    panelToggle.Visible = true;
                    break;
                case "Text":
                    txtEnvTextNew.Text = envProp.NewValue;
                    txtEnvTextNew.Tag = envProp.Name;
                    txtEnvTextOld.Text = envProp.OldValue;
                    splitEnvText.Visible = true;
                    break;
                case "Number":
                    numEnvNew.Minimum = Convert.ToDecimal(envProp.MinValue);
                    numEnvNew.Maximum = Convert.ToDecimal(envProp.MaxValue);

                    numEnvNew.Value = Convert.ToDecimal(envProp.NewValue);
                    numEnvNew.Tag = envProp.Name; 
                    numEnvOld.Minimum = Convert.ToDecimal(envProp.MinValue);
                    numEnvOld.Maximum = Convert.ToDecimal(envProp.MaxValue);
                    numEnvOld.Value = Convert.ToDecimal(envProp.OldValue);
                    splitEnvNumber.Visible = true;
                    break;
                case "Choice":
                    cboEnvDropNew.DataSource = envProp.Options;
                    cboEnvDropNew.Tag = envProp.Name;
                    cboEnvDropNew.DisplayMember = "Label";
                    cboEnvDropNew.ValueMember = "Value";
                    cboEnvDropNew.SelectedValue = envProp.NewValue ?? envProp.OldValue;
                    txtEnvDropOld.Text = envProp.OldValue;
                    splitEnvDropDown.Visible = true;
                    break;
            }
            grpEnvSetting.Visible = true;
        }
        #endregion
    }


    public static class JObjectExtensions
    {
        /// <summary>
        /// Replaces value based on path. New object tokens are created for missing parts of the given path.
        /// </summary>
        /// <param name="self">Instance to update</param>
        /// <param name="path">Dot delimited path of the new value. E.g. 'foo.bar'</param>
        /// <param name="value">Value to set.</param>
        public static void ReplaceNested(this JObject self, string path, JToken value)
        {
            if (self is null)
                throw new ArgumentNullException(nameof(self));

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Path cannot be null or empty", nameof(path));

            var pathParts = path.Split('.');
            JToken currentNode = self;

            for (int i = 0; i < pathParts.Length; i++)
            {
                var pathPart = pathParts[i];
                var isLast = i == pathParts.Length - 1;
                var partNode = currentNode.SelectToken(pathPart);

                if (partNode is null)
                {
                    var nodeToAdd = isLast ? value : new JObject();
                    ((JObject)currentNode).Add(pathPart, nodeToAdd);
                    currentNode = currentNode.SelectToken(pathPart);
                }
                else
                {
                    currentNode = partNode;

                    if (isLast)
                        currentNode.Replace(value);
                }
            }
        }
    }


}
