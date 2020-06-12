using McTools.Xrm.Connection;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using XrmToolBox.Extensibility;

namespace OrgSettings
{
    public partial class OrgSettingsControl : MultipleConnectionsPluginControlBase
    {
        private List<OrgSetting> curOrgSettings;
        private Settings mySettings;
        private List<OrgSetting> filteredList;
        private Guid orgGuid;

        public OrgSettingsControl()
        {
            InitializeComponent();
        }

        private void OrgSettingsControl_Load(object sender, EventArgs e)
        {
            ShowInfoNotification("NOTE: You should NOT change any setting without having a specific reason to do.\r\nPlease ensure you have a back up of the current settings prior to changing them, available in Manual tab", null);

            // Loads or creates the settings for the plugin
            if (!SettingsManager.Instance.TryLoad(GetType(), out mySettings))
            {
                mySettings = new Settings();

                LogWarning("Settings not found => a new settings file has been created!");
            }
            else
            {
                LogInfo("Settings found and loaded");
            }
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseTool();
        }

        private void tsbSample_Click(object sender, EventArgs e)
        {
            // The ExecuteMethod method handles connecting to an
            // organization if XrmToolBox is not yet connected
            ExecuteMethod(LoadConfig);
        }

        private void InitGridView()
        {
            // gvSettings.AutoResizeRowHeadersWidth = DataGridViewAutoSizeColumnMode.AllCells;
            //foreach(DataGridViewColumn gvCol in gvSettings.Columns)
            //{
            //    gvCol.SortMode = DataGridViewColumnSortMode.Automatic;
            //}
            //gvSettings.Columns["description"].Visible = false;
        }

        /// <summary>
        /// This event occurs when the plugin is closed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyPluginControl_OnCloseTool(object sender, EventArgs e)
        {
            // Before leaving, save the settings
            SettingsManager.Instance.Save(GetType(), mySettings);
        }

        /// <summary>
        /// This event occurs when the connection has been updated in XrmToolBox
        /// </summary>
        public override void UpdateConnection(IOrganizationService newService, ConnectionDetail detail, string actionName, object parameter)
        {
            base.UpdateConnection(newService, detail, actionName, parameter);

            if (mySettings != null && detail != null)
            {
                mySettings.LastUsedOrganizationWebappUrl = detail.WebApplicationUrl;
                LogInfo("Connection has changed to: {0}", detail.WebApplicationUrl);
            }

            ExecuteMethod(LoadConfig);
        }

        protected override void ConnectionDetailsUpdated(NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Display configuration for selected Org Setting
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSettings_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            OrgSetting selectedOS = (OrgSetting)gvSettings.Rows[e.RowIndex].DataBoundItem;
            SetConfigLabels(selectedOS);

            switch (selectedOS.type)
            {
                case "Boolean":
                    populateBool(selectedOS);

                    break;

                case "Number":
                    if (selectedOS.minValue == "0" && selectedOS.maxValue == "1") // Treat like a boolean
                        populateBool(selectedOS);
                    else
                    {
                        grpNumber.Visible = true;
                        // remove event to prevent pop of setting on entry
                        numberNew.ValueChanged -= numberNew_ValueChanged;

                        if (string.IsNullOrEmpty(selectedOS.minValue)) numberNew.Minimum = int.MinValue;
                        else numberNew.Minimum = int.Parse(selectedOS.minValue);

                        if (string.IsNullOrEmpty(selectedOS.maxValue)) numberNew.Maximum = int.MaxValue;
                        else numberNew.Maximum = int.Parse(selectedOS.maxValue);

                        if (string.IsNullOrEmpty(selectedOS.newSetting)) numberNew.Value = numberNew.Minimum;
                        else numberNew.Value = decimal.Parse(selectedOS.newSetting);

                        numberNew.ValueChanged += numberNew_ValueChanged;

                        lblMinNumber.Text = "Min: " + numberNew.Minimum.ToString();
                        lblMaxNumber.Text = "Max: " + numberNew.Maximum.ToString();
                    }
                    break;

                case "Double":

                    grpDouble.Visible = true;
                    if (!string.IsNullOrEmpty(selectedOS.newSetting))
                        decimalNewValue.Value = (decimal.Parse(selectedOS.newSetting));
                    else decimalNewValue.Value = 0;

                    break;

                case "String":

                    grpString.Visible = true;

                    break;

                default:
                    break;
            }
        }

        private void SetConfigLabels(OrgSetting selectedOS)
        {
            grpAttribute.Text = selectedOS.name;
            webDescription.DocumentText = @"<body style=""font-family:verdana"">" + selectedOS.description + "</body>";
            txtCurrentValue.Text = selectedOS.currentSetting;
            linkURL.Text = selectedOS.urlTitle;
            linkURL.Tag = selectedOS.url;
            lblDefaultValue.Text = selectedOS.defaultValue;

            lblMin.Text = selectedOS.minVersion;
            lblMax.Text = selectedOS.maxVersion;
            lblTypeValue.Text = selectedOS.type;

            btnRemove.Visible = !string.IsNullOrEmpty(selectedOS.currentSetting);

            grpBool.Visible = false;
            grpNumber.Visible = false;
            grpDouble.Visible = false;
            grpString.Visible = false;
        }

        private void populateBool(OrgSetting selectedOS)
        {
            grpBool.Visible = true;
            grpNumber.Visible = false;

            if (selectedOS.newSetting == "")
            {
                radioFalse.Checked = false;
                radioTrue.Checked = false;
            }
            else if (selectedOS.newSetting.ToLower() == "true" || selectedOS.newSetting == "1")
            {
                radioFalse.Checked = false;
                radioTrue.Checked = true;
            }
            else if (selectedOS.newSetting.ToLower() == "false" || selectedOS.newSetting == "0")
            {
                radioFalse.Checked = true;
                radioTrue.Checked = false;
            }
        }

        /// <summary>
        /// Colour the grid depending on whether we value new or current values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSettings_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            OrgSetting os = (OrgSetting)gvSettings.Rows[e.RowIndex].DataBoundItem;
            if (string.IsNullOrEmpty(os.newSetting))
            {
                if (!string.IsNullOrEmpty(os.currentSetting))
                {
                    e.CellStyle.BackColor = Color.LightGreen;
                }
            }
            else
            {
                e.CellStyle.BackColor = Color.Red;
            }
            //if (!String.IsNullOrEmpty(gvSettings.Rows[e.RowIndex].Cells["currentSetting"].Value.ToString()))

            //    e.CellStyle.BackColor = Color.Red;
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

            OrgSetting curOS = filteredList.Find(os => os.name == grpAttribute.Text);
            if (curOS.type == "Number")
            {
                curOS.newSetting = radioButton.Text == "True" ? "1" : "0";
            }
            else
            {
                curOS.newSetting = radioButton.Text.ToLower();
            }

            if (curOrgSettings.FindIndex(os => curOS == os) == -1)
            {
                curOrgSettings.Add(curOS);
            }
            else
            {
                curOrgSettings.Find(os => curOS == os).newSetting = curOS.newSetting;
            }

            filteredList.Find(os => curOS == os).newSetting = curOS.newSetting;
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
            SortOrder strSortOrder = getSortOrder(e.ColumnIndex);

            filteredList.Sort(new orgSettngComparer(strColumnName, strSortOrder));
            gvSettings.DataSource = null;
            gvSettings.DataSource = filteredList;
            // customizeDataGridView();
            gvSettings.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection = strSortOrder;
        }

        /// <summary>
        /// Get the current sort order of the column and return it
        /// set the new SortOrder to the columns.
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns>SortOrder of the current column</returns>
        private SortOrder getSortOrder(int columnIndex)
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
            if (curOrgSettings == null || curOrgSettings.FindIndex(os => !String.IsNullOrEmpty(os.newSetting)) == -1) return;

            if (MessageBox.Show("Commit " +
                curOrgSettings.FindAll(os => !String.IsNullOrEmpty(os.newSetting)).Count.ToString() + " changes to your Org Settings?",
                "Commit Changes?", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            ExecuteMethod(UpdateConfig);
        }

        /// <summary>
        /// Copy current configuration to allow manaul update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        ///
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

            UpdateManualComplete();
        }

        /// <summary>
        /// Navigate to the URL
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkURL_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(linkURL.Tag.ToString());
        }

        /// <summary>
        /// Update orgSetting with number selected
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void numberNew_ValueChanged(object sender, EventArgs e)
        {
            OrgSetting curOS = filteredList.Find(os => os.name == grpAttribute.Text);
            curOS.newSetting = numberNew.Value.ToString();
            if (curOrgSettings.FindIndex(os => curOS == os) == -1)
            {
                curOrgSettings.Add(curOS);
            }
            else
            {
                curOrgSettings.Find(os => curOS == os).newSetting = curOS.newSetting;
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void txtStringValue_TextChanged(object sender, EventArgs e)
        {
            OrgSetting curOS = filteredList.Find(os => os.name == grpAttribute.Text);
            curOS.newSetting = txtStringValue.Text.ToString();
            if (curOrgSettings.FindIndex(os => curOS == os) == -1)
            {
                curOrgSettings.Add(curOS);
            }
            else
            {
                curOrgSettings.Find(os => curOS == os).newSetting = curOS.newSetting;
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void decimalNewValue_ValueChanged(object sender, EventArgs e)
        {
            OrgSetting curOS = filteredList.Find(os => os.name == grpAttribute.Text);
            curOS.newSetting = decimalNewValue.Value.ToString("#.00");
            //    decimalNewValue.Text.ToString();
            if (curOrgSettings.FindIndex(os => curOS == os) == -1)
            {
                curOrgSettings.Add(curOS);
            }
            else
            {
                curOrgSettings.Find(os => curOS == os).newSetting = curOS.newSetting;
            }

            gvSettings.Update();
            gvSettings.Refresh();
        }

        private void decimalNewValue_Validated(object sender, EventArgs e)
        {
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to remove " + grpAttribute.Text +
                " from your conifguration?", "Remove Config?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                ExecuteMethod(RemoveConfig);
        }
    }
}