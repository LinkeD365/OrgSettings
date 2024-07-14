using System.Collections.Generic;

namespace LinkeD365.OrgSettings
{
    public class APIConns
    {
        // public List<LogicAppConn> LogicAppConns = new List<LogicAppConn>();
        public List<APIConn> bapConns = new List<APIConn>();

       // public List<GraphConn> GraphConns = new List<GraphConn>();

       // public Display Display = new Display();
    }

    public class APIConn
    {
        public int Id = 0;

        public string Name;
        public string TenantId = string.Empty;
        public string ReturnURL = string.Empty;
        public string Environment = string.Empty;
        public string AppId;
        public bool UseDev;

        public string Type;

        public override string ToString()
        {
            return Name;
        }
    }
}