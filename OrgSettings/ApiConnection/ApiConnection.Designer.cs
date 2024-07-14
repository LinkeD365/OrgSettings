namespace LinkeD365.OrgSettings
{
    partial class ApiConnection
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ApiConnection));
            this.lblClientId = new System.Windows.Forms.Label();
            this.txtAppId = new System.Windows.Forms.TextBox();
            this.txtEnvironment = new System.Windows.Forms.TextBox();
            this.lblEnvironment = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtTenant = new System.Windows.Forms.TextBox();
            this.lblTenant = new System.Windows.Forms.Label();
            this.txtReturnURL = new System.Windows.Forms.TextBox();
            this.lblReturn = new System.Windows.Forms.Label();
            this.chkUseDevApp = new System.Windows.Forms.CheckBox();
            this.flowMain = new System.Windows.Forms.FlowLayoutPanel();
            this.panelGraph = new System.Windows.Forms.Panel();
            this.btnRemoveGraph = new System.Windows.Forms.Button();
            this.lblGraphName = new System.Windows.Forms.Label();
            this.txtGraphName = new System.Windows.Forms.TextBox();
            this.btnAddGraph = new System.Windows.Forms.Button();
            this.cboGraphConns = new System.Windows.Forms.ComboBox();
            this.lblGraphSelect = new System.Windows.Forms.Label();
            this.chkGraphDev = new System.Windows.Forms.CheckBox();
            this.txtGraphReturnURL = new System.Windows.Forms.TextBox();
            this.lblGraphApp = new System.Windows.Forms.Label();
            this.txtGraphApp = new System.Windows.Forms.TextBox();
            this.lblGraphReturn = new System.Windows.Forms.Label();
            this.txtGraphTenant = new System.Windows.Forms.TextBox();
            this.lblGraphTenant = new System.Windows.Forms.Label();
            this.lblSubscription = new System.Windows.Forms.Label();
            this.txtSubscriptionId = new System.Windows.Forms.TextBox();
            this.panelFlow = new System.Windows.Forms.Panel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.lblName = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this.btnFlowAdd = new System.Windows.Forms.Button();
            this.cboFlowConns = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnHelp = new System.Windows.Forms.Button();
            this.toolTipAPI = new System.Windows.Forms.ToolTip(this.components);
            this.flowMain.SuspendLayout();
            this.panelGraph.SuspendLayout();
            this.panelFlow.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblClientId
            // 
            this.lblClientId.AutoSize = true;
            this.lblClientId.Location = new System.Drawing.Point(27, 72);
            this.lblClientId.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblClientId.Name = "lblClientId";
            this.lblClientId.Size = new System.Drawing.Size(92, 16);
            this.lblClientId.TabIndex = 0;
            this.lblClientId.Text = "Client / App Id:";
            this.toolTipAPI.SetToolTip(this.lblClientId, "App Id configured in Azure for the connection or use the Dev App Config");
            // 
            // txtAppId
            // 
            this.txtAppId.ForeColor = System.Drawing.Color.Silver;
            this.txtAppId.Location = new System.Drawing.Point(135, 69);
            this.txtAppId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtAppId.Name = "txtAppId";
            this.txtAppId.Size = new System.Drawing.Size(363, 22);
            this.txtAppId.TabIndex = 25;
            this.txtAppId.Tag = "0";
            this.txtAppId.Text = "Client Id for Configured App Registration";
            this.toolTipAPI.SetToolTip(this.txtAppId, "App Id configured in Azure for the connection or use the Dev App Config");
            this.txtAppId.Enter += new System.EventHandler(this.configValueEnter);
            this.txtAppId.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // txtEnvironment
            // 
            this.txtEnvironment.ForeColor = System.Drawing.Color.Silver;
            this.txtEnvironment.Location = new System.Drawing.Point(135, 132);
            this.txtEnvironment.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtEnvironment.Name = "txtEnvironment";
            this.txtEnvironment.Size = new System.Drawing.Size(363, 22);
            this.txtEnvironment.TabIndex = 35;
            this.txtEnvironment.Text = "Power Automate Environment Id";
            this.toolTipAPI.SetToolTip(this.txtEnvironment, "Reference to the Environment where your Power Automate Flows are, available under" +
        " Developer Resources within Make.PowerApps.com");
            this.txtEnvironment.Enter += new System.EventHandler(this.configValueEnter);
            this.txtEnvironment.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblEnvironment
            // 
            this.lblEnvironment.AutoSize = true;
            this.lblEnvironment.Location = new System.Drawing.Point(21, 135);
            this.lblEnvironment.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEnvironment.Name = "lblEnvironment";
            this.lblEnvironment.Size = new System.Drawing.Size(98, 16);
            this.lblEnvironment.TabIndex = 4;
            this.lblEnvironment.Text = "Environment Id:";
            this.toolTipAPI.SetToolTip(this.lblEnvironment, "Reference to the Environment where your Power Automate Flows are, available under" +
        " Developer Resources within Make.PowerApps.com");
            // 
            // btnOK
            // 
            this.btnOK.AccessibleDescription = "Confirm Connection";
            this.btnOK.AccessibleName = "OK";
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(325, 267);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 100;
            this.btnOK.Text = "Ok";
            this.toolTipAPI.SetToolTip(this.btnOK, "Confirm Connection");
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleDescription = "Cancel Connection";
            this.btnCancel.AccessibleName = "Cancel";
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(433, 267);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 105;
            this.btnCancel.Text = "Cancel";
            this.toolTipAPI.SetToolTip(this.btnCancel, "Cancel Connection");
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // txtTenant
            // 
            this.txtTenant.ForeColor = System.Drawing.Color.Silver;
            this.txtTenant.Location = new System.Drawing.Point(135, 101);
            this.txtTenant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtTenant.Name = "txtTenant";
            this.txtTenant.Size = new System.Drawing.Size(363, 22);
            this.txtTenant.TabIndex = 30;
            this.txtTenant.Text = "Azure Tenant Id";
            this.toolTipAPI.SetToolTip(this.txtTenant, "The Tenant ID for your Azure AD, available https://portal.azure.com/#view/Microso" +
        "ft_AAD_IAM/TenantProperties.ReactView");
            this.txtTenant.Enter += new System.EventHandler(this.configValueEnter);
            this.txtTenant.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblTenant
            // 
            this.lblTenant.AutoSize = true;
            this.lblTenant.Location = new System.Drawing.Point(54, 104);
            this.lblTenant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTenant.Name = "lblTenant";
            this.lblTenant.Size = new System.Drawing.Size(66, 16);
            this.lblTenant.TabIndex = 2;
            this.lblTenant.Text = "Tenant Id:";
            this.toolTipAPI.SetToolTip(this.lblTenant, "The Tenant ID for your Azure AD, available https://portal.azure.com/#view/Microso" +
        "ft_AAD_IAM/TenantProperties.ReactView");
            // 
            // txtReturnURL
            // 
            this.txtReturnURL.ForeColor = System.Drawing.Color.Silver;
            this.txtReturnURL.Location = new System.Drawing.Point(135, 164);
            this.txtReturnURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReturnURL.Name = "txtReturnURL";
            this.txtReturnURL.Size = new System.Drawing.Size(363, 22);
            this.txtReturnURL.TabIndex = 40;
            this.txtReturnURL.Text = "Return URL for Configured App Regisration";
            this.toolTipAPI.SetToolTip(this.txtReturnURL, "The return URL for the application configured with access, or use the Dev App Con" +
        "fig");
            this.txtReturnURL.Enter += new System.EventHandler(this.configValueEnter);
            this.txtReturnURL.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblReturn
            // 
            this.lblReturn.AutoSize = true;
            this.lblReturn.Location = new System.Drawing.Point(37, 167);
            this.lblReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblReturn.Name = "lblReturn";
            this.lblReturn.Size = new System.Drawing.Size(79, 16);
            this.lblReturn.TabIndex = 6;
            this.lblReturn.Text = "Return URL:";
            this.toolTipAPI.SetToolTip(this.lblReturn, "The return URL for the application configured with access, or use the Dev App Con" +
        "fig");
            // 
            // chkUseDevApp
            // 
            this.chkUseDevApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkUseDevApp.AutoSize = true;
            this.chkUseDevApp.Location = new System.Drawing.Point(337, 198);
            this.chkUseDevApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkUseDevApp.Name = "chkUseDevApp";
            this.chkUseDevApp.Size = new System.Drawing.Size(151, 20);
            this.chkUseDevApp.TabIndex = 45;
            this.chkUseDevApp.Text = "Use Dev App Config";
            this.toolTipAPI.SetToolTip(this.chkUseDevApp, "Use the Dev APP config for this Graph Connection");
            this.chkUseDevApp.UseVisualStyleBackColor = true;
            // 
            // flowMain
            // 
            this.flowMain.Controls.Add(this.panelGraph);
            this.flowMain.Controls.Add(this.panelFlow);
            this.flowMain.Location = new System.Drawing.Point(-1, 10);
            this.flowMain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowMain.Name = "flowMain";
            this.flowMain.Size = new System.Drawing.Size(535, 242);
            this.flowMain.TabIndex = 11;
            // 
            // panelGraph
            // 
            this.panelGraph.Controls.Add(this.btnRemoveGraph);
            this.panelGraph.Controls.Add(this.lblGraphName);
            this.panelGraph.Controls.Add(this.txtGraphName);
            this.panelGraph.Controls.Add(this.btnAddGraph);
            this.panelGraph.Controls.Add(this.cboGraphConns);
            this.panelGraph.Controls.Add(this.lblGraphSelect);
            this.panelGraph.Controls.Add(this.chkGraphDev);
            this.panelGraph.Controls.Add(this.txtGraphReturnURL);
            this.panelGraph.Controls.Add(this.lblGraphApp);
            this.panelGraph.Controls.Add(this.txtGraphApp);
            this.panelGraph.Controls.Add(this.lblGraphReturn);
            this.panelGraph.Controls.Add(this.txtGraphTenant);
            this.panelGraph.Controls.Add(this.lblGraphTenant);
            this.panelGraph.Controls.Add(this.lblSubscription);
            this.panelGraph.Controls.Add(this.txtSubscriptionId);
            this.panelGraph.Location = new System.Drawing.Point(4, 4);
            this.panelGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelGraph.Name = "panelGraph";
            this.panelGraph.Size = new System.Drawing.Size(516, 231);
            this.panelGraph.TabIndex = 12;
            // 
            // btnRemoveGraph
            // 
            this.btnRemoveGraph.Location = new System.Drawing.Point(485, 2);
            this.btnRemoveGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemoveGraph.Name = "btnRemoveGraph";
            this.btnRemoveGraph.Size = new System.Drawing.Size(23, 28);
            this.btnRemoveGraph.TabIndex = 10;
            this.btnRemoveGraph.Text = "-";
            this.toolTipAPI.SetToolTip(this.btnRemoveGraph, "Remove the current Graph API connection from the saved list");
            this.btnRemoveGraph.UseVisualStyleBackColor = true;
            this.btnRemoveGraph.Click += new System.EventHandler(this.btnRemoveLA_Click);
            // 
            // lblGraphName
            // 
            this.lblGraphName.AutoSize = true;
            this.lblGraphName.Location = new System.Drawing.Point(76, 41);
            this.lblGraphName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGraphName.Name = "lblGraphName";
            this.lblGraphName.Size = new System.Drawing.Size(44, 16);
            this.lblGraphName.TabIndex = 15;
            this.lblGraphName.Text = "Label:";
            this.toolTipAPI.SetToolTip(this.lblGraphName, "Label to identify Connection");
            // 
            // txtGraphName
            // 
            this.txtGraphName.ForeColor = System.Drawing.Color.Silver;
            this.txtGraphName.Location = new System.Drawing.Point(135, 37);
            this.txtGraphName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGraphName.Name = "txtGraphName";
            this.txtGraphName.Size = new System.Drawing.Size(363, 22);
            this.txtGraphName.TabIndex = 15;
            this.txtGraphName.Tag = "0";
            this.txtGraphName.Text = "Label for Connection";
            this.toolTipAPI.SetToolTip(this.txtGraphName, "Label to identify Connection");
            this.txtGraphName.Enter += new System.EventHandler(this.configValueEnter);
            this.txtGraphName.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // btnAddGraph
            // 
            this.btnAddGraph.Location = new System.Drawing.Point(459, 2);
            this.btnAddGraph.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddGraph.Name = "btnAddGraph";
            this.btnAddGraph.Size = new System.Drawing.Size(23, 28);
            this.btnAddGraph.TabIndex = 5;
            this.btnAddGraph.Text = "+";
            this.toolTipAPI.SetToolTip(this.btnAddGraph, "Add a new Graph API Connection");
            this.btnAddGraph.UseVisualStyleBackColor = true;
            this.btnAddGraph.Click += new System.EventHandler(this.btnAddGraph_Click);
            // 
            // cboGraphConns
            // 
            this.cboGraphConns.DisplayMember = "Name";
            this.cboGraphConns.FormattingEnabled = true;
            this.cboGraphConns.Location = new System.Drawing.Point(135, 4);
            this.cboGraphConns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboGraphConns.Name = "cboGraphConns";
            this.cboGraphConns.Size = new System.Drawing.Size(320, 24);
            this.cboGraphConns.TabIndex = 1;
            this.toolTipAPI.SetToolTip(this.cboGraphConns, "Select the saved Graph Connection");
            this.cboGraphConns.SelectedIndexChanged += new System.EventHandler(this.cboGraphConns_SelectedIndexChanged);
            // 
            // lblGraphSelect
            // 
            this.lblGraphSelect.AutoSize = true;
            this.lblGraphSelect.Location = new System.Drawing.Point(71, 9);
            this.lblGraphSelect.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGraphSelect.Name = "lblGraphSelect";
            this.lblGraphSelect.Size = new System.Drawing.Size(48, 16);
            this.lblGraphSelect.TabIndex = 12;
            this.lblGraphSelect.Text = "Select:";
            this.toolTipAPI.SetToolTip(this.lblGraphSelect, "Select the saved Graph Connection");
            // 
            // chkGraphDev
            // 
            this.chkGraphDev.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkGraphDev.AutoSize = true;
            this.chkGraphDev.Location = new System.Drawing.Point(347, 198);
            this.chkGraphDev.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkGraphDev.Name = "chkGraphDev";
            this.chkGraphDev.Size = new System.Drawing.Size(151, 20);
            this.chkGraphDev.TabIndex = 40;
            this.chkGraphDev.Text = "Use Dev App Config";
            this.toolTipAPI.SetToolTip(this.chkGraphDev, "Use the Dev APP config for this Graph Connection");
            this.chkGraphDev.UseVisualStyleBackColor = true;
            // 
            // txtGraphReturnURL
            // 
            this.txtGraphReturnURL.ForeColor = System.Drawing.Color.Silver;
            this.txtGraphReturnURL.Location = new System.Drawing.Point(135, 165);
            this.txtGraphReturnURL.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGraphReturnURL.Name = "txtGraphReturnURL";
            this.txtGraphReturnURL.Size = new System.Drawing.Size(363, 22);
            this.txtGraphReturnURL.TabIndex = 35;
            this.txtGraphReturnURL.Text = "Return URL for Configured App Regisration";
            this.toolTipAPI.SetToolTip(this.txtGraphReturnURL, "The return URL for the application configured with access, or use the Dev App Con" +
        "fig");
            this.txtGraphReturnURL.Enter += new System.EventHandler(this.configValueEnter);
            this.txtGraphReturnURL.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblGraphApp
            // 
            this.lblGraphApp.AutoSize = true;
            this.lblGraphApp.Location = new System.Drawing.Point(23, 105);
            this.lblGraphApp.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGraphApp.Name = "lblGraphApp";
            this.lblGraphApp.Size = new System.Drawing.Size(92, 16);
            this.lblGraphApp.TabIndex = 8;
            this.lblGraphApp.Text = "Client / App Id:";
            this.toolTipAPI.SetToolTip(this.lblGraphApp, "App Id configured in Azure for the connection or use the Dev App Config");
            // 
            // txtGraphApp
            // 
            this.txtGraphApp.ForeColor = System.Drawing.Color.Silver;
            this.txtGraphApp.Location = new System.Drawing.Point(135, 101);
            this.txtGraphApp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGraphApp.Name = "txtGraphApp";
            this.txtGraphApp.Size = new System.Drawing.Size(363, 22);
            this.txtGraphApp.TabIndex = 25;
            this.txtGraphApp.Tag = "0";
            this.txtGraphApp.Text = "Client Id for Configured App Registration";
            this.toolTipAPI.SetToolTip(this.txtGraphApp, "App Id configured in Azure for the connection or use the Dev App Config");
            this.txtGraphApp.Enter += new System.EventHandler(this.configValueEnter);
            this.txtGraphApp.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblGraphReturn
            // 
            this.lblGraphReturn.AutoSize = true;
            this.lblGraphReturn.Location = new System.Drawing.Point(37, 169);
            this.lblGraphReturn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGraphReturn.Name = "lblGraphReturn";
            this.lblGraphReturn.Size = new System.Drawing.Size(79, 16);
            this.lblGraphReturn.TabIndex = 10;
            this.lblGraphReturn.Text = "Return URL:";
            this.toolTipAPI.SetToolTip(this.lblGraphReturn, "The return URL for the application configured with access, or use the Dev App Con" +
        "fig");
            // 
            // txtGraphTenant
            // 
            this.txtGraphTenant.ForeColor = System.Drawing.Color.Silver;
            this.txtGraphTenant.Location = new System.Drawing.Point(135, 133);
            this.txtGraphTenant.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtGraphTenant.Name = "txtGraphTenant";
            this.txtGraphTenant.Size = new System.Drawing.Size(363, 22);
            this.txtGraphTenant.TabIndex = 30;
            this.txtGraphTenant.Text = "Azure Tenant Id";
            this.toolTipAPI.SetToolTip(this.txtGraphTenant, "The Tenant ID for your Azure AD, available https://portal.azure.com/#view/Microso" +
        "ft_AAD_IAM/TenantProperties.ReactView");
            this.txtGraphTenant.Enter += new System.EventHandler(this.configValueEnter);
            this.txtGraphTenant.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // lblGraphTenant
            // 
            this.lblGraphTenant.AutoSize = true;
            this.lblGraphTenant.Location = new System.Drawing.Point(49, 137);
            this.lblGraphTenant.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGraphTenant.Name = "lblGraphTenant";
            this.lblGraphTenant.Size = new System.Drawing.Size(66, 16);
            this.lblGraphTenant.TabIndex = 4;
            this.lblGraphTenant.Text = "Tenant Id:";
            this.toolTipAPI.SetToolTip(this.lblGraphTenant, "The Tenant ID for your Azure AD, available https://portal.azure.com/#view/Microso" +
        "ft_AAD_IAM/TenantProperties.ReactView");
            // 
            // lblSubscription
            // 
            this.lblSubscription.AutoSize = true;
            this.lblSubscription.Location = new System.Drawing.Point(17, 73);
            this.lblSubscription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSubscription.Name = "lblSubscription";
            this.lblSubscription.Size = new System.Drawing.Size(98, 16);
            this.lblSubscription.TabIndex = 2;
            this.lblSubscription.Text = "Subscription Id:";
            this.toolTipAPI.SetToolTip(this.lblSubscription, "This denotes the subsceription of your Azure tenant, available https://portal.azu" +
        "re.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2");
            // 
            // txtSubscriptionId
            // 
            this.txtSubscriptionId.ForeColor = System.Drawing.Color.Silver;
            this.txtSubscriptionId.Location = new System.Drawing.Point(135, 69);
            this.txtSubscriptionId.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtSubscriptionId.Name = "txtSubscriptionId";
            this.txtSubscriptionId.Size = new System.Drawing.Size(363, 22);
            this.txtSubscriptionId.TabIndex = 20;
            this.txtSubscriptionId.Tag = "0";
            this.txtSubscriptionId.Text = "Azure Subscription Id";
            this.toolTipAPI.SetToolTip(this.txtSubscriptionId, "This denotes the subsceription of your Azure tenant, available https://portal.azu" +
        "re.com/#view/Microsoft_Azure_Billing/SubscriptionsBladeV2");
            this.txtSubscriptionId.Enter += new System.EventHandler(this.configValueEnter);
            this.txtSubscriptionId.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // panelFlow
            // 
            this.panelFlow.Controls.Add(this.btnRemove);
            this.panelFlow.Controls.Add(this.lblName);
            this.panelFlow.Controls.Add(this.txtName);
            this.panelFlow.Controls.Add(this.btnFlowAdd);
            this.panelFlow.Controls.Add(this.cboFlowConns);
            this.panelFlow.Controls.Add(this.label1);
            this.panelFlow.Controls.Add(this.txtReturnURL);
            this.panelFlow.Controls.Add(this.lblClientId);
            this.panelFlow.Controls.Add(this.chkUseDevApp);
            this.panelFlow.Controls.Add(this.txtAppId);
            this.panelFlow.Controls.Add(this.lblEnvironment);
            this.panelFlow.Controls.Add(this.lblReturn);
            this.panelFlow.Controls.Add(this.txtEnvironment);
            this.panelFlow.Controls.Add(this.txtTenant);
            this.panelFlow.Controls.Add(this.lblTenant);
            this.panelFlow.Location = new System.Drawing.Point(4, 243);
            this.panelFlow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panelFlow.Name = "panelFlow";
            this.panelFlow.Size = new System.Drawing.Size(516, 230);
            this.panelFlow.TabIndex = 12;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(485, 1);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(23, 28);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "-";
            this.toolTipAPI.SetToolTip(this.btnRemove, "Remove the current Power Automate API connection from the saved list");
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(76, 41);
            this.lblName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(44, 16);
            this.lblName.TabIndex = 18;
            this.lblName.Text = "Label:";
            this.toolTipAPI.SetToolTip(this.lblName, "Label to identify Connection");
            // 
            // txtName
            // 
            this.txtName.ForeColor = System.Drawing.Color.Silver;
            this.txtName.Location = new System.Drawing.Point(135, 37);
            this.txtName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(363, 22);
            this.txtName.TabIndex = 20;
            this.txtName.Tag = "0";
            this.txtName.Text = "Label for Connection";
            this.toolTipAPI.SetToolTip(this.txtName, "Label to identify Connection");
            this.txtName.Enter += new System.EventHandler(this.configValueEnter);
            this.txtName.Leave += new System.EventHandler(this.configValueLeave);
            // 
            // btnFlowAdd
            // 
            this.btnFlowAdd.Location = new System.Drawing.Point(459, 1);
            this.btnFlowAdd.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnFlowAdd.Name = "btnFlowAdd";
            this.btnFlowAdd.Size = new System.Drawing.Size(23, 28);
            this.btnFlowAdd.TabIndex = 10;
            this.btnFlowAdd.Text = "+";
            this.toolTipAPI.SetToolTip(this.btnFlowAdd, "Add a new Power Automate API Connection");
            this.btnFlowAdd.UseVisualStyleBackColor = true;
            this.btnFlowAdd.Click += new System.EventHandler(this.btnFlowAdd_Click);
            // 
            // cboFlowConns
            // 
            this.cboFlowConns.DisplayMember = "Name";
            this.cboFlowConns.FormattingEnabled = true;
            this.cboFlowConns.Location = new System.Drawing.Point(135, 4);
            this.cboFlowConns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboFlowConns.Name = "cboFlowConns";
            this.cboFlowConns.Size = new System.Drawing.Size(320, 24);
            this.cboFlowConns.TabIndex = 5;
            this.toolTipAPI.SetToolTip(this.cboFlowConns, "Select the saved Power Automate API Connection");
            this.cboFlowConns.SelectedIndexChanged += new System.EventHandler(this.cboFlowConns_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(71, 9);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 16);
            this.label1.TabIndex = 15;
            this.label1.Text = "Select:";
            this.toolTipAPI.SetToolTip(this.label1, "Select the saved Power Automate API Connection");
            // 
            // btnHelp
            // 
            this.btnHelp.AccessibleDescription = "Shows help page on how to connect to the API to retrieve Flows";
            this.btnHelp.AccessibleName = "Help Button";
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(16, 267);
            this.btnHelp.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(100, 28);
            this.btnHelp.TabIndex = 110;
            this.btnHelp.Text = "Help";
            this.toolTipAPI.SetToolTip(this.btnHelp, "See the configuration page on LinkeD365.blog");
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // toolTipAPI
            // 
            this.toolTipAPI.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ApiConnection
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(551, 306);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.flowMain);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ApiConnection";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Configure API Connection";
            this.flowMain.ResumeLayout(false);
            this.panelGraph.ResumeLayout(false);
            this.panelGraph.PerformLayout();
            this.panelFlow.ResumeLayout(false);
            this.panelFlow.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblClientId;
        private System.Windows.Forms.TextBox txtAppId;
        private System.Windows.Forms.TextBox txtEnvironment;
        private System.Windows.Forms.Label lblEnvironment;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtTenant;
        private System.Windows.Forms.Label lblTenant;
        private System.Windows.Forms.TextBox txtReturnURL;
        private System.Windows.Forms.Label lblReturn;
        private System.Windows.Forms.CheckBox chkUseDevApp;
        private System.Windows.Forms.FlowLayoutPanel flowMain;
        private System.Windows.Forms.Panel panelFlow;
        private System.Windows.Forms.Panel panelGraph;
        private System.Windows.Forms.Label lblSubscription;
        private System.Windows.Forms.TextBox txtSubscriptionId;
        private System.Windows.Forms.TextBox txtGraphTenant;
        private System.Windows.Forms.Label lblGraphTenant;
        private System.Windows.Forms.TextBox txtGraphReturnURL;
        private System.Windows.Forms.Label lblGraphApp;
        private System.Windows.Forms.TextBox txtGraphApp;
        private System.Windows.Forms.Label lblGraphReturn;
        private System.Windows.Forms.CheckBox chkGraphDev;
        private System.Windows.Forms.Label lblGraphName;
        private System.Windows.Forms.TextBox txtGraphName;
        private System.Windows.Forms.Button btnAddGraph;
        private System.Windows.Forms.ComboBox cboGraphConns;
        private System.Windows.Forms.Label lblGraphSelect;
        private System.Windows.Forms.Button btnFlowAdd;
        private System.Windows.Forms.ComboBox cboFlowConns;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Button btnRemoveGraph;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.ToolTip toolTipAPI;
    }
}