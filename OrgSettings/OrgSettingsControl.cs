using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace LinkeD365.OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase, IGitHubPlugin
    {
        private readonly AppInsights _ai;
        public string RepositoryName => "OrgSettings";
        private const string AiEndpoint = "https://dc.services.visualstudio.com/v2/track";

        private const string AiKey = "cc383234-dfdb-429a-a970-d17847361df3";

        public string UserName => "LinkeD365";
        private List<OrgSetting> _primaryOrgSettings = new List<OrgSetting>();
        private List<OrgSetting> _secondaryOrgSettings = new List<OrgSetting>();
        private List<OrgSetting> _filteredList = new List<OrgSetting>();

        private List<OrgSetting> _fullList = new List<OrgSetting>();
        private Guid _orgGuid;
        private TabControl _hidden = new TabControl();

        private bool SecondarySetting
        {
            get => AdditionalConnectionDetails.Any();
        }

        public OrgSettingsControl()
        {
            InitializeComponent();
            _ai = new AppInsights(AiEndpoint, AiKey, Assembly.GetExecutingAssembly());
            _ai.WriteEvent("Control Loaded");
        }

        private void OrgSettingsControl_Load(object sender, EventArgs e)
        {
            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out apiConns))
            {
                apiConns = new APIConns();

                LogWarning("Settings not found => a new settings file has been created!");
            }

            ShowInfoNotification("NOTE: You should NOT change any setting without having a specific reason to do.\r\nPlease ensure you have a back up of the current settings prior to changing them, available in Manual tab", null);
            ExecuteMethod(LoadSingleConfig);
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if (tabGrpBase.SelectedTab == tabPageEnvironment)
            { ExecuteMethod(LoadEnvironmentConfig); }
            else
            {
                btnConnectSecondary.Enabled = false;
                // The ExecuteMethod method handles connecting
                // to an organization if XrmToolBox is not yet connected
                txtSearch.Text = string.Empty;
                if (AdditionalConnectionDetails.Any()) RemoveAdditionalOrganization(AdditionalConnectionDetails[0]);
                ExecuteMethod(LoadSingleConfig);
                if (ConnectionDetail != null) btnConnectSecondary.Enabled = true;
                // LoadSingleConfig();
            }
        }



        private void InitGridView()
        {
            gvSettings.Columns["secondaryCurrentSetting"].Visible = AdditionalConnectionDetails.Any();
            gvSettings.Columns["secondaryNewSetting"].Visible = AdditionalConnectionDetails.Any();

            gvSettings.Columns["currentSetting"].HeaderText =
                ConnectionDetail.ConnectionName + " Current";

            gvSettings.Columns["newSetting"].HeaderText =
                ConnectionDetail.ConnectionName + " New";
            if (AdditionalConnectionDetails.Any())
            {
                gvSettings.Columns["secondaryCurrentSetting"].HeaderText =
                    AdditionalConnectionDetails[0].ConnectionName + " Current";

                gvSettings.Columns["secondaryNewSetting"].HeaderText =
                    AdditionalConnectionDetails[0].ConnectionName + " New";
            }
        }

        /// <summary>
        /// This event occurs when the connection has been
        /// updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);
            if (ConnectionDetail != null)
            {
                btnConnectSecondary.Enabled = true;
                btnSave.Text = "Commit to " + ConnectionDetail.ConnectionName;
            }
        }

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs args)
        {
            if (args.Action == NotifyCollectionChangedAction.Add || args.Action == NotifyCollectionChangedAction.Replace)
            {
                if (AdditionalConnectionDetails[0].ConnectionName == ConnectionDetail.ConnectionName)
                {
                    MessageBox.Show("Please choose a different connection rather than the current primary one",
                        "Choose different connections", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RemoveAdditionalOrganization(AdditionalConnectionDetails[0]);
                    AddAdditionalOrganization();
                    return;
                }

                LoadConfig(AdditionalConnectionDetails[0], _secondaryOrgSettings, true);
                UpdateGroups();
                btnSecondaryCommit.Text = "Commit to " + AdditionalConnectionDetails[0].ConnectionName;
                btnSecondaryCommit.Enabled = true;
                btnClone.Enabled = true;
            }
            else
            {
                btnSecondaryCommit.Text = "Secondary Commit";
                btnSecondaryCommit.Enabled = false;
                btnClone.Enabled = false;
            }
        }

        private void UpdateGroups()
        {
            splitSet.Panel2Collapsed = !AdditionalConnectionDetails.Any();
        }

        /// <summary>
        /// Display configuration for selected Org Setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSettings_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            OrgSetting selectedOs = (OrgSetting)gvSettings.Rows[e.RowIndex].DataBoundItem;
            SetConfigLabels(selectedOs);

            switch (selectedOs.Type)
            {
                case "Boolean":
                    PopulateBool(selectedOs);

                    break;

                case "Number":
                    if (selectedOs.MinValue == "0" && selectedOs.MaxValue == "1") // Treat like a boolean
                        PopulateBool(selectedOs);
                    else
                    {
                        grpNumber.Visible = true;
                        // remove event to prevent pop of
                        // setting on entry
                        numberNew.ValueChanged -= numberNew_ValueChanged;

                        if (string.IsNullOrEmpty(selectedOs.MinValue)) numberNew.Minimum = int.MinValue;
                        else numberNew.Minimum = int.Parse(selectedOs.MinValue);

                        if (string.IsNullOrEmpty(selectedOs.MaxValue)) numberNew.Maximum = int.MaxValue;
                        else numberNew.Maximum = int.Parse(selectedOs.MaxValue);

                        if (string.IsNullOrEmpty(selectedOs.NewSetting)) numberNew.Value = numberNew.Minimum;
                        else numberNew.Value = decimal.Parse(selectedOs.NewSetting);

                        numberNew.ValueChanged += numberNew_ValueChanged;

                        lblMinNumber.Text = "Min: " + numberNew.Minimum;
                        lblMaxNumber.Text = "Max: " + numberNew.Maximum;

                        if (SecondarySetting)
                        {
                            grpSecNumber.Visible = true;
                            // remove event to prevent pop
                            // of setting on entry
                            numberSecNew.ValueChanged -= numberNew_ValueChanged;

                            if (string.IsNullOrEmpty(selectedOs.MinValue)) numberSecNew.Minimum = int.MinValue;
                            else numberSecNew.Minimum = int.Parse(selectedOs.MinValue);

                            if (string.IsNullOrEmpty(selectedOs.MaxValue)) numberSecNew.Maximum = int.MaxValue;
                            else numberSecNew.Maximum = int.Parse(selectedOs.MaxValue);

                            if (string.IsNullOrEmpty(selectedOs.SecondaryNewSetting)) numberSecNew.Value = numberSecNew.Minimum;
                            else numberSecNew.Value = decimal.Parse(selectedOs.SecondaryNewSetting);

                            numberSecNew.ValueChanged += numberNew_ValueChanged;

                            lblSecMinNumber.Text = "Min: " + numberNew.Minimum;
                            lblSecMaxNumber.Text = "Max: " + numberNew.Maximum;
                        }
                    }
                    break;

                case "Double":

                    grpDouble.Visible = true;

                    decimalNewValue.Value = !string.IsNullOrEmpty(selectedOs.NewSetting) ? decimal.Parse(selectedOs.NewSetting) : 0;
                    if (SecondarySetting)
                    {
                        grpSecDouble.Visible = true;

                        decimalSecNewValue.Value = !string.IsNullOrEmpty(selectedOs.SecondaryNewSetting) ? decimal.Parse(selectedOs.SecondaryNewSetting) : 0;
                    }
                    break;

                case "String":

                    grpString.Visible = true;
                    grpSecString.Visible = SecondarySetting;

                    break;
            }
        }

        /// <summary>
        /// Set all the labels etc that are static 07-07-20
        /// added LinkeD365 desscription
        /// </summary>
        /// <param name="selectedOs"></param>
        private void SetConfigLabels(OrgSetting selectedOs)
        {
            grpAttribute.Text = ConnectionDetail.ConnectionName + " " + selectedOs.Name;
            if (SecondarySetting)
                grpSecAttribute.Text = AdditionalConnectionDetails[0].ConnectionName + " " + selectedOs.Name;
            grpAttribute.Tag = selectedOs.Name;
            webDescription.DocumentText = @"<body style=""font-family:verdana"">" + selectedOs.Description + "</body>";

            if (string.IsNullOrEmpty(selectedOs.LinkeD365Description))
            {
                _ai.WriteEvent("No LinkeD365 for " + selectedOs.Name);
                if (tabLinkeD365.Parent != _hidden) _hidden.TabPages.Add(tabLinkeD365);
                linkLinkeD365.Visible = false;
                lblLinkedD365URL.Visible = false;
            }
            else
            {
                if (tabLinkeD365.Parent != tabWeb) tabWeb.TabPages.Add(tabLinkeD365);

                linkLinkeD365.Visible = true;
                lblLinkedD365URL.Visible = true;

                webLinkeD365.DocumentText = @"<body style=""font-family:verdana"">" + selectedOs.LinkeD365Description + "</body>";
                linkLinkeD365.Text = selectedOs.Name;
                linkLinkeD365.Tag = selectedOs.LinkeD365Url;
            }

            txtCurrentValue.Text = selectedOs.CurrentSetting;
            txtSecCurrentValue.Text = selectedOs.SecondaryCurrentSetting;

            linkURL.Text = selectedOs.UrlTitle;
            linkURL.Tag = selectedOs.Url;
            lblDefaultValue.Text = selectedOs.DefaultValue;

            lblMin.Text = selectedOs.MinVersion;
            lblMax.Text = selectedOs.MaxVersion;
            lblTypeValue.Text = selectedOs.Type;

            btnRemove.Visible = !string.IsNullOrEmpty(selectedOs.CurrentSetting);
            btnSecRemove.Visible = !string.IsNullOrEmpty(selectedOs.SecondaryCurrentSetting);

            grpBool.Visible = false;
            grpNumber.Visible = false;
            grpDouble.Visible = false;
            grpString.Visible = false;

            grpSecBool.Visible = false;
            grpSecNumber.Visible = false;
            grpSecDouble.Visible = false;
            grpSecString.Visible = false;
        }

        private void PopulateBool(OrgSetting selectedOs)
        {
            grpBool.Visible = true;

            if (string.IsNullOrEmpty(selectedOs.NewSetting))
            {
                radioFalse.Checked = false;
                radioTrue.Checked = false;
            }
            else if (selectedOs.NewSetting.ToLower() == "true" || selectedOs.NewSetting == "1")
            {
                radioFalse.Checked = false;
                radioTrue.Checked = true;
            }
            else if (selectedOs.NewSetting.ToLower() == "false" || selectedOs.NewSetting == "0")
            {
                radioFalse.Checked = true;
                radioTrue.Checked = false;
            }

            if (SecondarySetting)
            {
                grpSecBool.Visible = true;

                if (string.IsNullOrEmpty(selectedOs.SecondaryNewSetting))
                {
                    radioSecNo.Checked = false;
                    radioSecTrue.Checked = false;
                }
                else if (selectedOs.SecondaryNewSetting.ToLower() == "true" || selectedOs.SecondaryNewSetting == "1")
                {
                    radioSecNo.Checked = false;
                    radioSecTrue.Checked = true;
                }
                else if (selectedOs.SecondaryNewSetting.ToLower() == "false" || selectedOs.SecondaryNewSetting == "0")
                {
                    radioSecNo.Checked = true;
                    radioSecTrue.Checked = false;
                }
            }
        }

        /// <summary>
        /// Colour the grid depending on whether we value
        /// new or current values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            OrgSetting os = (OrgSetting)gvSettings.Rows[e.RowIndex].DataBoundItem;
            switch (e.ColumnIndex)
            {
                case 0:
                    if (string.IsNullOrEmpty(os.NewSetting) && string.IsNullOrEmpty(os.SecondaryNewSetting))
                    {
                        if (!string.IsNullOrEmpty(os.CurrentSetting) || !string.IsNullOrEmpty(os.SecondaryCurrentSetting))
                        {
                            e.CellStyle.BackColor = Color.LightGreen;
                        }
                    }
                    else
                    {
                        e.CellStyle.BackColor = Color.Red;
                    }

                    return;

                case 1:
                    if (!string.IsNullOrEmpty(os.CurrentSetting)) e.CellStyle.BackColor = Color.LightGreen;
                    return;

                case 2:
                    if (!string.IsNullOrEmpty(os.NewSetting)) e.CellStyle.BackColor = Color.Red;
                    return;

                case 3:
                    if (!string.IsNullOrEmpty(os.SecondaryCurrentSetting)) e.CellStyle.BackColor = Color.LightGreen;
                    return;

                case 4:
                    if (!string.IsNullOrEmpty(os.SecondaryNewSetting)) e.CellStyle.BackColor = Color.Red;
                    return;
            }
            if (string.IsNullOrEmpty(os.NewSetting))
            {
                if (!string.IsNullOrEmpty(os.CurrentSetting))
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            else
            {
                e.CellStyle.BackColor = Color.Red;
            }
            //if (!String.IsNullOrEmpty(gvSettings.Rows[e.RowIndex].Cells["currentSetting"].Value.ToString()))

            // e.CellStyle.BackColor = Color.Red;
        }

        /// <summary>
        /// Update orgSetting with value selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radio_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (!radioButton.Checked)
            {
                return;
            }

            OrgSetting curOs = _filteredList.Find(os => os.Name == grpAttribute.Tag.ToString());

            if (radioButton.Name == radioFalse.Name || radioButton.Name == radioTrue.Name)
            {
                if (curOs.Type == "Number")
                    curOs.NewSetting = radioButton.Text == "True" ? "1" : "0";
                else
                    curOs.NewSetting = radioButton.Text.ToLower();

                if (_primaryOrgSettings.FindIndex(os => curOs.Name == os.Name) == -1)
                    _primaryOrgSettings.Add(curOs);
                else
                    _primaryOrgSettings.Find(os => curOs.Name == os.Name).NewSetting = curOs.NewSetting;
            }
            else
            {
                if (curOs.Type == "Number")
                {
                    curOs.SecondaryNewSetting = radioButton.Text == "True" ? "1" : "0";
                }
                else
                {
                    curOs.SecondaryNewSetting = radioButton.Text.ToLower();
                }

                if (_secondaryOrgSettings.FindIndex(os => curOs.Name == os.Name) == -1)
                {
                    _secondaryOrgSettings.Add(new OrgSetting()
                    { Name = curOs.Name, NewSetting = curOs.SecondaryNewSetting });
                }
                else
                {
                    _secondaryOrgSettings.Find(os => curOs.Name == os.Name).NewSetting = curOs.SecondaryNewSetting;
                }
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        /// <summary>
        /// Sort the grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSettings_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //get the current column details
            string strColumnName = gvSettings.Columns[e.ColumnIndex].Name;
            SortOrder strSortOrder = GetSortOrder(e.ColumnIndex);

            _filteredList.Sort(new OrgSettngComparer(strColumnName, strSortOrder));
            gvSettings.DataSource = null;
            gvSettings.DataSource = _filteredList;
            // customizeDataGridView();
            gvSettings.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
            InitGridView();
        }

        /// <summary>
        /// Get the current sort order of the column and
        /// return it set the new SortOrder to the columns.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns>SortOrder of the current column</returns>
        private SortOrder GetSortOrder(int columnIndex)
        {
            if (gvSettings.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.None ||
                gvSettings.Columns[columnIndex].HeaderCell.SortGlyphDirection == SortOrder.Descending)
            {
                gvSettings.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Ascending;
                return SortOrder.Ascending;
            }
            else
            {
                gvSettings.Columns[columnIndex].HeaderCell.SortGlyphDirection = SortOrder.Descending;
                return SortOrder.Descending;
            }
        }

        /// <summary>
        /// Commit changes back to D365
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (tabGrpBase.SelectedTab == tabPageEnvironment)
            {
                UpdateEnvConfig();

            }
            else
            {

                if (_primaryOrgSettings == null || _primaryOrgSettings.FindIndex(os => !String.IsNullOrEmpty(os.NewSetting)) == -1) return;

                if (MessageBox.Show("Commit " +
                    _primaryOrgSettings.FindAll(os => !String.IsNullOrEmpty(os.NewSetting)).Count + " changes to your Org Settings?",
                    "Commit Changes?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                UpdateConfig(ConnectionDetail, _primaryOrgSettings);
            }
        }

        /// <summary>
        /// Copy current configuration to allow manaul update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopyCurrent_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtOverride.Text))
            {
                if (MessageBox.Show("Do you want to clear the configuration in Overriden Config?", "Clear Override?",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                {
                    return;
                }
            }

            txtOverride.Text = txtCurrentXml.Text;
        }

        /// <summary>
        /// Update hte config with a manual file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void btnUpdateManual_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOverride.Text))
            {
                return;
            }

            if (MessageBox.Show("Using a custom configuration is entirely unsupported. \r\n " +
                "Please ensure you have a copy of the current configuration before continuing. \r\n" +
                "Continue at own risk?", "Commit Override?",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            UpdateManualComplete(ConnectionDetail);
        }

        /// <summary>
        /// Navigate to the URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel ll = sender as LinkLabel;
            Process.Start(ll.Tag.ToString());
        }

        /// <summary>
        /// Update orgSetting with number selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberNew_ValueChanged(object sender, EventArgs e)
        {
            OrgSetting curOs = _filteredList.Find(os => os.Name == grpAttribute.Tag.ToString());

            var upDown = sender as NumericUpDown;

            if (upDown.Name == numberNew.Name)
            {
                curOs.NewSetting = upDown.Value.ToString();
                if (_primaryOrgSettings.FindIndex(os => curOs == os) == -1) _primaryOrgSettings.Add(curOs);
                else _primaryOrgSettings.Find(os => curOs == os).NewSetting = curOs.NewSetting;
            }
            else
            {
                curOs.SecondaryNewSetting = upDown.Value.ToString();
                if (_secondaryOrgSettings.FindIndex(os => curOs == os) == -1) _secondaryOrgSettings.Add(new OrgSetting { Name = curOs.Name, NewSetting = curOs.SecondaryNewSetting });
                else _secondaryOrgSettings.Find(os => curOs == os).NewSetting = curOs.SecondaryNewSetting;
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void txtStringValue_TextChanged(object sender, EventArgs e)
        {
            var txtBox = sender as TextBox;
            OrgSetting curOs = _filteredList.Find(os => os.Name == grpAttribute.Tag.ToString());

            if (txtBox.Name == txtStringValue.Name)
            {
                curOs.NewSetting = txtBox.Text;
                // curOS.newSetting = txtStringValue.Text;
                if (_primaryOrgSettings.FindIndex(os => curOs == os) == -1) _primaryOrgSettings.Add(curOs);
                else _primaryOrgSettings.Find(os => curOs == os).NewSetting = curOs.NewSetting;
            }
            else
            {
                curOs.SecondaryNewSetting = txtBox.Text;
                if (_secondaryOrgSettings.FindIndex(os => curOs == os) == -1)
                    _secondaryOrgSettings.Add(new OrgSetting { Name = curOs.Name, NewSetting = curOs.SecondaryNewSetting });
                else _secondaryOrgSettings.Find(os => curOs == os).NewSetting = curOs.SecondaryNewSetting;
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void decimalNewValue_ValueChanged(object sender, EventArgs e)
        {
            OrgSetting curOs = _filteredList.Find(os => os.Name == grpAttribute.Tag.ToString());
            var upDown = sender as NumericUpDown;
            if (((NumericUpDown)sender).Name == decimalNewValue.Name)
            {
                curOs.NewSetting = upDown.Value.ToString("#.00");
                // decimalNewValue.Text.ToString();
                if (_primaryOrgSettings.FindIndex(os => curOs == os) == -1)
                {
                    _primaryOrgSettings.Add(curOs);
                }
                else
                {
                    _primaryOrgSettings.Find(os => curOs == os).NewSetting = curOs.NewSetting;
                }
            }
            else
            {
                curOs.SecondaryNewSetting = upDown.Value.ToString("#.00");
                // decimalNewValue.Text.ToString();
                if (_secondaryOrgSettings.FindIndex(os => curOs == os) != -1)
                {
                    _secondaryOrgSettings.Find(os => curOs == os).SecondaryNewSetting = curOs.SecondaryNewSetting;
                }
                else
                {
                    _secondaryOrgSettings.Add(new OrgSetting { Name = curOs.Name, NewSetting = curOs.SecondaryNewSetting });
                }
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void decimalNewValue_Validated(object sender, EventArgs e)
        {
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            string environment = ((Button)sender).Name == "btnRemove" ? ConnectionDetail.ConnectionName : AdditionalConnectionDetails[0].ConnectionName;
            if (MessageBox.Show("Are you sure you want to remove " + grpAttribute.Text +
                                " from " + environment + " configuration?", "Remove Config?",
                MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            if (((Button)sender).Name == "btnRemove") RemoveConfig(ConnectionDetail, _primaryOrgSettings);
            else RemoveConfig(AdditionalConnectionDetails[0], _secondaryOrgSettings);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            gvSettings.DataSource = null;
            gvSettings.DataSource = _filteredList.Where(os => os.Name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();

            InitGridView();
            //gvSecondary.DataSource = null;
            //gvSecondary.DataSource = filteredList.Where(os => os.name.ToLower().Contains(txtSearch.Text.ToLower())).ToList();
        }

        private void btnConnectSecondary_Click(object sender, EventArgs e)
        {
            btnSecondaryCommit.Enabled = false;
            if (AdditionalConnectionDetails.Any()) RemoveAdditionalOrganization(AdditionalConnectionDetails[0]);
            AddAdditionalOrganization();
        }

        private void btnSecondaryCommit_Click(object sender, EventArgs e)
        {
            if (_secondaryOrgSettings == null || _secondaryOrgSettings.FindIndex(os => !String.IsNullOrEmpty(os.NewSetting)) == -1) return;

            if (MessageBox.Show("Commit " +
                                _secondaryOrgSettings.FindAll(os => !String.IsNullOrEmpty(os.NewSetting)).Count + " changes to your Org Settings for " + AdditionalConnectionDetails[0].ConnectionName + "?",
                "Commit Changes?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            UpdateConfig(AdditionalConnectionDetails[0], _secondaryOrgSettings);
        }

        private void btnClone_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(
                    "Copy org settings from " + ConnectionDetail.ConnectionName + " to " +
                    AdditionalConnectionDetails[0].ConnectionName + "?", "Clone Org Settings?", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question) !=
                DialogResult.Yes) return;
            //var removeList = secondaryOrgSettings.Where(os => !primaryOrgSettings.Contains(os));
            //foreach (var removeOs in removeList)
            //{
            //    secondaryOrgSettings.Remove(removeOs);
            //}

            foreach (OrgSetting removeOs in _secondaryOrgSettings.Where(os => !_primaryOrgSettings.Contains(os)))
            {
                _fullList.Find(os => os.Name == removeOs.Name).SecondaryNewSetting = string.Empty;
            }

            _secondaryOrgSettings.RemoveAll(os => !_primaryOrgSettings.Contains(os));
            foreach (var primOs in _primaryOrgSettings)
            {
                _fullList.Find(os => primOs.Name == os.Name).SecondaryNewSetting = string.IsNullOrEmpty(primOs.NewSetting) ? primOs.CurrentSetting : primOs.NewSetting;
                if (_secondaryOrgSettings.FindIndex(os => primOs == os) != -1)
                {
                    _secondaryOrgSettings.Find(os => primOs == os).NewSetting = string.IsNullOrEmpty(primOs.NewSetting) ? primOs.CurrentSetting : primOs.NewSetting;
                }
                else _secondaryOrgSettings.Add(new OrgSetting { Name = primOs.Name, NewSetting = string.IsNullOrEmpty(primOs.NewSetting) ? primOs.CurrentSetting : primOs.NewSetting });
            }

            gvSettings.DataSource = _fullList;
            InitGridView();
        }

        private void gridEnv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //if (gridEnv.Columns[e.ColumnIndex].Name == "colNewValue")
            //{
            //    var envProp = (EnvProp)gridEnv.Rows[e.RowIndex].DataBoundItem;
            //    switch (envProp.Type)
            //    {
            //        case "Toggle":
                    
            //    }
            //}
        }


    }
}