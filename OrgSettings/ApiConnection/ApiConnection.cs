using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Services.Description;
using System.Windows.Forms;

namespace LinkeD365.OrgSettings
{
    public partial class ApiConnection : Form
    {
        public APIConns apiConns;
        public APIConn bapConn;

        private bool Graph;

        // Azure Active Directory registered app clientid
        // for Microsoft samples
        private const string clientId = "51f81489-12ee-4a9e-aaae-a2591f45987d";

        // Azure Active Directory registered app Redirect
        // URI for Microsoft samples
        private Uri redirectUri = new Uri("app://58145B91-0C36-4500-8554-080854F2AC97");

        private const string subscriptionText = "Azure Subscription Id";
        private const string appText = "Client Id for Configured App Registration";
        private const string tenantText = "Azure Tenant Id";
        private const string returnText = "Return URL for Configured App Regisration";
        private const string labelText = "Label for Connection";
        private const string environmentText = "Power Automate Environment Id";

        public ApiConnection(APIConn apiConn, bool graph)
        {
            InitializeComponent();
            //  if (graph) this.graphConn = (GraphConn)aPIConn;
            //else
            bapConn = apiConn;
            Graph = graph;
        }

        public ApiConnection(APIConns apiConnections, bool graph)
        {
            InitializeComponent();
            apiConns = apiConnections;
            Graph = graph;

            //if (!Graph)
            //{
            //    if (apiConns.FlowConns.Any())
            //    {
            //        cboFlowConns.Items.AddRange(apiConns.FlowConns.ToArray());
            //        cboFlowConns.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        EnableControls();
            //        cboFlowConns.Enabled = false;

            //        chkUseDevApp.CheckedChanged += ChkUseDevApp_CheckedChanged;
            //    }

            //    panelGraph.Visible = false;
            //}
            //else
            //{
            if (apiConns.bapConns.Any())
            {
                cboBAPConns.Items.AddRange(apiConns.bapConns.ToArray());
                cboBAPConns.SelectedIndex = 0;
            }
            else
            {
                EnableControls();
                cboBAPConns.Enabled = false;
                chkUseDevApp.CheckedChanged += ChkUseDevApp_CheckedChanged;
            }
            //}
            panelGraph.Visible = Graph;
            panelFlow.Visible = !Graph;
            Text = $"Connect to {(Graph ? "Graph" : "BAP")} API";
        }

        private void EnableControls()
        {
            panelFlow.Controls.OfType<TextBox>().ToList().ForEach(ctl => ctl.Enabled = !Graph);
            panelGraph.Controls.OfType<TextBox>().ToList().ForEach(ctl => ctl.Enabled = Graph);

            chkUseDevApp.Enabled = false;
            cboBAPConns.Enabled = false;
        }

        public HttpClient GetClient()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                //if (!Graph)
                //{
                //    flowConn = apiConns.FlowConns.FirstOrDefault(flw => flw.Id == (int)lblName.Tag);
                //    if (flowConn == null)
                //    {
                //        flowConn = new FlowConn();
                //    }

                //    flowConn.Id = (int)lblName.Tag;
                //    flowConn.Environment = txtEnvironment.Text;
                //    flowConn.TenantId = txtTenant.Text;
                //    flowConn.ReturnURL = txtReturnURL.Text;
                //    flowConn.AppId = txtAppId.Text;
                //    flowConn.UseDev = chkUseDevApp.Checked;
                //    flowConn.Name = txtName.Text;
                //    var flwIndex = apiConns.FlowConns.IndexOf(flowConn);
                //    if (flwIndex == -1)
                //    {
                //        apiConns.FlowConns.Add(flowConn);
                //    }
                //    else
                //    {
                //        apiConns.FlowConns[flwIndex] = flowConn;
                //    }
                //}
                //else
                //{
                bapConn = apiConns.bapConns.FirstOrDefault(grph => grph.Id == (int)lblName.Tag);
                if (bapConn == null)
                {
                    bapConn = new APIConn();
                }

                bapConn.Id = (int)lblName.Tag;
                bapConn.Environment = txtEnvironment.Text;
                bapConn.TenantId = txtTenant.Text;
                bapConn.ReturnURL = txtReturnURL.Text;
                bapConn.AppId = txtAppId.Text;
                bapConn.UseDev = chkUseDevApp.Checked;
                bapConn.Name = txtName.Text;
                var grpIndex = apiConns.bapConns.IndexOf(bapConn);
                if (grpIndex == -1)
                {
                    apiConns.bapConns.Add(bapConn);
                }
                else
                {
                    apiConns.bapConns[grpIndex] = bapConn;
                }
                //}
                return Connect();
            }

            return null;
        }

        public HttpClient Connect()
        {
            var token = GetInteractiveClientToken();

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            client.DefaultRequestHeaders.Add("accept", "application/json");
            return client;
        }

        private string GetInteractiveClientToken()
        {
            // AuthenticationContext ac = new AuthenticationContext($"https://login.microsoftonline.com/{(Graph ? graphConn.TenantId : flowConn.TenantId)}");
            AuthenticationContext ac = new AuthenticationContext($"https://login.microsoftonline.com/{bapConn.TenantId}");
            //string serviceURL = Graph ? "https://graph.microsoft.com" : "https://service.flow.microsoft.com/";
            string serviceURL = "https://api.bap.microsoft.com/";
            //string appId = Graph ? graphConn.AppId : flowConn.AppId;
            string appId = bapConn.AppId;

            try
            {
                return ac.AcquireTokenSilentAsync(serviceURL, appId).GetAwaiter().GetResult().AccessToken;
            }
            catch (AdalException adalException)
            {
                if (adalException.ErrorCode == AdalError.FailedToAcquireTokenSilently
                    || adalException.ErrorCode == AdalError.InteractionRequired)
                {
                    //return ac.AcquireTokenAsync(serviceURL, appId, new Uri(Graph ? graphConn.ReturnURL : flowConn.ReturnURL),
                    //    new PlatformParameters(PromptBehavior.SelectAccount)).GetAwaiter().GetResult().AccessToken;

                    return ac.AcquireTokenAsync(serviceURL, appId, new Uri(bapConn.ReturnURL),
                                               new PlatformParameters(PromptBehavior.SelectAccount)).GetAwaiter().GetResult().AccessToken;
                }
            }

            return null;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Graph && (txtAppId.Text == string.Empty || txtAppId.Text == appText
                || txtTenant.Text == string.Empty || txtTenant.Text == tenantText
                || txtEnvironment.Text == string.Empty || txtEnvironment.Text == environmentText
                || txtReturnURL.Text == string.Empty || txtReturnURL.Text == returnText
                || txtName.Text == string.Empty || txtName.Text == labelText))
            {
                MessageBox.Show("Please ensure all fields have a value", "Required properties missing", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                return;
            }
            else if (Graph && (txtGraphApp.Text == string.Empty || txtGraphApp.Text == appText
                           || txtGraphTenant.Text == string.Empty || txtGraphTenant.Text == tenantText
                           || txtGraphReturnURL.Text == string.Empty || txtGraphReturnURL.Text == returnText
                           || txtGraphName.Text == string.Empty || txtGraphName.Text == labelText))
            {
                MessageBox.Show("Please ensure all fields have a value", "Required properties missing", MessageBoxButtons.OK,
                                       MessageBoxIcon.Exclamation);
                DialogResult = DialogResult.None;
                return;
            }
        }

        private void ChkUseDevApp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkUseDevApp.Checked)
            {
                MessageBox.Show(
                    "For development and prototyping purposes, Microsoft has provided AppId and Redirect URI for use in OAuth situations. " +
                        Environment.NewLine +
                        "For production use, create an AppId that is specific to your tenant in the Azure Management Portal", "Use only for Dev", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAppId.Text = clientId;
                txtReturnURL.Text = redirectUri.ToString();
            }
            else
            {
                txtAppId.Text = "Client Id for Configured App Registration";
                txtReturnURL.Text = "Return URL for Configured App Regisration";

                txtAppId.ForeColor = Color.Silver;
                txtReturnURL.ForeColor = Color.Silver;
            }

            txtAppId.Enabled = !chkUseDevApp.Checked;
            txtReturnURL.Enabled = !chkUseDevApp.Checked;
        }

        private void ChkGraphDev_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGraphDev.Checked)
            {
                MessageBox.Show(
                    "For development and prototyping purposes, Microsoft has provided AppId and Redirect URI for use in OAuth situations. " +
                        Environment.NewLine +
                        "For production use, create an AppId that is specific to your tenant in the Azure Management Portal", "Use only for Dev", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGraphApp.Text = clientId;
                txtGraphReturnURL.Text = redirectUri.ToString();
            }
            else
            {
                txtGraphApp.Text = "Client Id for Configured App Registration";
                txtGraphReturnURL.Text = "Return URL for Configured App Regisration";
                txtGraphApp.ForeColor = Color.Silver;
                txtGraphReturnURL.ForeColor = Color.Silver;
            }
            txtGraphApp.Enabled = !chkGraphDev.Checked;
            txtGraphReturnURL.Enabled = !chkGraphDev.Checked;
        }

        private void cboGraphConns_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkGraphDev.CheckedChanged -= ChkGraphDev_CheckedChanged;
            var selectedConn = cboGraphConns.SelectedItem as APIConn;
            lblGraphName.Tag = selectedConn.Id;

            txtGraphTenant.Text = selectedConn.TenantId;
            txtGraphApp.Text = selectedConn.AppId;
            txtGraphReturnURL.Text = selectedConn.ReturnURL;
            chkGraphDev.Checked = selectedConn.UseDev;
            //txt.Text = selectedConn.Environment;
            txtGraphName.Text = selectedConn.Name;
            chkUseDevApp.CheckedChanged += ChkGraphDev_CheckedChanged;
            panelGraph.Controls.OfType<TextBox>().ToList().ForEach(txt => txt.ForeColor = Color.Black);

            txtGraphApp.Enabled = !chkUseDevApp.Checked;
            txtGraphReturnURL.Enabled = !chkUseDevApp.Checked;
        }

        private void cboFlowConns_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkUseDevApp.CheckedChanged -= ChkUseDevApp_CheckedChanged;
            var selectedConn = cboBAPConns.SelectedItem as APIConn;
            lblName.Tag = selectedConn.Id;

            txtTenant.Text = selectedConn.TenantId;
            txtAppId.Text = selectedConn.AppId;
            txtReturnURL.Text = selectedConn.ReturnURL;
            chkUseDevApp.Checked = selectedConn.UseDev;
            txtEnvironment.Text = selectedConn.Environment;
            txtName.Text = selectedConn.Name;
            chkUseDevApp.CheckedChanged += ChkUseDevApp_CheckedChanged;
            panelFlow.Controls.OfType<TextBox>().ToList().ForEach(txt => txt.ForeColor = Color.Black);

            txtAppId.Enabled = !chkUseDevApp.Checked;
            txtReturnURL.Enabled = !chkUseDevApp.Checked;
        }

        private void btnAddGraph_Click(object sender, EventArgs e)
        {
            txtSubscriptionId.Text = subscriptionText;
            txtGraphTenant.Text = tenantText;
            txtGraphApp.Text = appText;
            txtGraphReturnURL.Text = returnText;
            chkGraphDev.Checked = false;
            txtGraphName.Text = labelText;
            txtGraphApp.Enabled = true;
            txtGraphReturnURL.Enabled = true;
            txtGraphName.Enabled = true;
            txtGraphTenant.Enabled = true;
            txtGraphName.Enabled = true;
            txtSubscriptionId.Enabled = true;
            chkGraphDev.Enabled = true;
            lblGraphName.Tag = apiConns.bapConns.Any() ? apiConns.bapConns.Max(flw => flw.Id) + 1 : 0;
            //lblGraphName.Tag = apiConns.GraphConns.Any() ? apiConns.GraphConns.Max(flw => flw.Id) + 1 : 0;
        }

        private void btnFlowAdd_Click(object sender, EventArgs e)
        {
           // lblName.Tag = apiConns.FlowConns.Any() ? apiConns.FlowConns.Max(flw => flw.Id) + 1 : 0;
           lblName.Tag = apiConns.bapConns.Any() ? apiConns.bapConns.Max(flw => flw.Id) + 1 : 0;
            txtEnvironment.Text = environmentText;
            txtTenant.Text = tenantText;
            txtAppId.Text = appText;
            txtReturnURL.Text = returnText;
            chkUseDevApp.Checked = false;
            txtName.Text = labelText;
            txtAppId.Enabled = true;
            txtReturnURL.Enabled = true;
            txtName.Enabled = true;
            txtTenant.Enabled = true;
            txtName.Enabled = true;
            txtEnvironment.Enabled = true;
            chkUseDevApp.Enabled = true;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (cboBAPConns.SelectedItem == null)
            {
                return;
            }

            APIConn removeFlow = (APIConn)cboBAPConns.SelectedItem;
            if (MessageBox.Show("Remove the Power Automate Connection '" + removeFlow.Name + "' ?",
                    "Remove Flow Connection", MessageBoxButtons.YesNo) !=
                DialogResult.Yes)
            {
                return;
            }
            cboBAPConns.Items.Clear();

            apiConns.bapConns.Remove(removeFlow);
            if (apiConns.bapConns.Any())
            {
                cboBAPConns.Items.AddRange(apiConns.bapConns.ToArray());
                cboBAPConns.SelectedIndex = 0;
            }
            else
            {
                cboBAPConns.ResetText();
                txtEnvironment.Text = environmentText;
                txtTenant.Text = tenantText;
                txtAppId.Text = appText;
                txtReturnURL.Text = returnText;
                chkUseDevApp.Checked = false;
                txtName.Text = labelText;
                cboBAPConns.Enabled = false;
                panelFlow.Controls.OfType<TextBox>().ToList().ForEach(txt => txt.Enabled = false);
                chkUseDevApp.Enabled = false;
            }
        }

        private void btnRemoveLA_Click(object sender, EventArgs e)
        {
        }

        private void configValueEnter(object sender, EventArgs e)
        {
            TextBox inputTextBox = sender as TextBox;
            if (inputTextBox.Text == GetDefaultText(inputTextBox))
            {
                inputTextBox.Text = string.Empty;
                inputTextBox.ForeColor = Color.Black;
            }
        }

        private string GetDefaultText(TextBox inputBox)
        {
            switch (inputBox.Name)
            {
                case "txtLAName":
                case "txtName":
                    return labelText;

                case "txtSubscriptionId":
                    return subscriptionText;

                case "txtLAApp":
                case "txtAppId":
                    return appText;

                case "txtLAReturnURL":
                case "txtReturnURL":
                    return returnText;

                case "txtLATenant":
                case "txtTenant":
                    return tenantText;

                case "txtEnvironment":
                    return environmentText;

                default:
                    return string.Empty;
            }
        }

        private void configValueLeave(object sender, EventArgs e)
        {
            TextBox inputTextBox = sender as TextBox;

            if (inputTextBox.Text == string.Empty)
            {
                inputTextBox.Text = GetDefaultText(inputTextBox);
                inputTextBox.ForeColor = Color.Silver;
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string url = "https://linked365.blog/2020/10/14/flow-to-visio-xrmtoolbox-addon/#" + ("PowerAutomateAPI");
            Process.Start(url);
        }

        private void btnGraphSubHelp_Click(object sender, EventArgs e)
        {
            toolTipAPI.Show(toolTipAPI.GetToolTip(txtSubscriptionId), txtSubscriptionId);
            //var label = new RichTextLabel("This denotes the subsceription of your Azure tenant, available https://portal.azure.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2");
            //label.Show();
        }

        public class RichTextLabel : RichTextBox
        {
            public RichTextLabel(string text)
            {
                Text = text;
            }
        }
    }
}