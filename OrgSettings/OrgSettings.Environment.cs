using XrmToolBox.Extensibility;

namespace LinkeD365.OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase
    {
        private APIConn bapConn;
        private ApiConnection apiConnection;
        private void LoadEnvironmentConfig()
        {
            if (bapConn == null) Connect();
            if (bapClient != null) return;
            WorkAsync(new WorkAsyncInfo
            {
                Message = "Getting Environment Settings",
                Work = (worker, args) =>
                {

                }
            }

            );
        }
               
        private void Connect()
        {
            if (bapConn == null)
            {
                apiConnection = new ApiConnection(bapConn, false);
                apiConnection.Connect();
            }
        }
    }
}
