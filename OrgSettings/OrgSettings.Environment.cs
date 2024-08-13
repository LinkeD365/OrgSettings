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
            var propNewValue = new DataGridViewTextBoxColumn { DataPropertyName = "New", Name = "New Value" };
            gridEnv.Columns.Add(propNewValue);

            var dynCol = new OptionalDropdownColumn();
            gridEnv.Columns.Add(dynCol);
            gridEnv.Columns.Add("colNewValue", "New Value");
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


                    if (envProps.Properties.Any(prop => string.IsNullOrEmpty(prop.Type) && !string.IsNullOrEmpty(prop.New)))
                    {
                        JObject jobj = new JObject();
                        string url = $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/scopes/admin/environments/{envProps.EnvId}?api-version=2021-04-01";
                        foreach (EnvProp envProp in envProps.Properties.Where(prop => string.IsNullOrEmpty(                           prop.Type) && !string.IsNullOrEmpty(prop.New)))
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

                    if (envProps.Properties.Any((prop => prop.Type == "Managed" && !string.IsNullOrEmpty(prop.New))))
                    {
                        JObject jobj = new JObject();
                        string url = $"https://api.bap.microsoft.com/providers/Microsoft.BusinessAppPlatform/environments{envProps.EnvId}/governanceConfiguration?api-version=2021-04-01";
                        foreach (EnvProp envProp in envProps.Properties.Where(prop => prop.Type == "Managed" && !string.IsNullOrEmpty(prop.New)))
                        {
                            jobj.ReplaceNested(envProp.UpdatePath, envProp.New);
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


                    args.Result = true;


                },
                PostWorkCallBack = (args) =>
                {
                }
            });
        }

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
