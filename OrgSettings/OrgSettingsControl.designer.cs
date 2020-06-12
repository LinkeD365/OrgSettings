namespace OrgSettings
{
    partial class OrgSettingsControl
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStripMenu = new System.Windows.Forms.ToolStrip();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tssSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbSample = new System.Windows.Forms.ToolStripButton();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.gvSettings = new System.Windows.Forms.DataGridView();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitMainRight = new System.Windows.Forms.SplitContainer();
            this.tabGrpMain = new System.Windows.Forms.TabControl();
            this.tabSet = new System.Windows.Forms.TabPage();
            this.grpAttribute = new System.Windows.Forms.GroupBox();
            this.grpDouble = new System.Windows.Forms.GroupBox();
            this.decimalNewValue = new System.Windows.Forms.NumericUpDown();
            this.grpCurrent = new System.Windows.Forms.GroupBox();
            this.txtCurrentValue = new System.Windows.Forms.TextBox();
            this.grpString = new System.Windows.Forms.GroupBox();
            this.txtStringValue = new System.Windows.Forms.TextBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.grpNumber = new System.Windows.Forms.GroupBox();
            this.lblMinNumber = new System.Windows.Forms.Label();
            this.lblMaxNumber = new System.Windows.Forms.Label();
            this.numberNew = new System.Windows.Forms.NumericUpDown();
            this.grpBool = new System.Windows.Forms.GroupBox();
            this.radioFalse = new System.Windows.Forms.RadioButton();
            this.radioTrue = new System.Windows.Forms.RadioButton();
            this.tabManual = new System.Windows.Forms.TabPage();
            this.splitManual = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtCurrentXml = new System.Windows.Forms.TextBox();
            this.splitManualEditor = new System.Windows.Forms.SplitContainer();
            this.btnUpdateManual = new System.Windows.Forms.Button();
            this.btnCopyCurrent = new System.Windows.Forms.Button();
            this.txtOverride = new System.Windows.Forms.TextBox();
            this.splitMainLower = new System.Windows.Forms.SplitContainer();
            this.lblDefaultValue = new System.Windows.Forms.Label();
            this.lblDefault = new System.Windows.Forms.Label();
            this.lblUrl = new System.Windows.Forms.Label();
            this.linkURL = new System.Windows.Forms.LinkLabel();
            this.webDescription = new System.Windows.Forms.WebBrowser();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblMin = new System.Windows.Forms.Label();
            this.lblMinVersion = new System.Windows.Forms.Label();
            this.lblMax = new System.Windows.Forms.Label();
            this.lblMaxVersion = new System.Windows.Forms.Label();
            this.lblTypeValue = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.toolStripMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainRight)).BeginInit();
            this.splitMainRight.Panel1.SuspendLayout();
            this.splitMainRight.Panel2.SuspendLayout();
            this.splitMainRight.SuspendLayout();
            this.tabGrpMain.SuspendLayout();
            this.tabSet.SuspendLayout();
            this.grpAttribute.SuspendLayout();
            this.grpDouble.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.decimalNewValue)).BeginInit();
            this.grpCurrent.SuspendLayout();
            this.grpString.SuspendLayout();
            this.grpNumber.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberNew)).BeginInit();
            this.grpBool.SuspendLayout();
            this.tabManual.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitManual)).BeginInit();
            this.splitManual.Panel1.SuspendLayout();
            this.splitManual.Panel2.SuspendLayout();
            this.splitManual.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitManualEditor)).BeginInit();
            this.splitManualEditor.Panel1.SuspendLayout();
            this.splitManualEditor.Panel2.SuspendLayout();
            this.splitManualEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMainLower)).BeginInit();
            this.splitMainLower.Panel1.SuspendLayout();
            this.splitMainLower.Panel2.SuspendLayout();
            this.splitMainLower.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripMenu
            // 
            this.toolStripMenu.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.toolStripMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbClose,
            this.tssSeparator1,
            this.tsbSample,
            this.btnSave});
            this.toolStripMenu.Location = new System.Drawing.Point(0, 0);
            this.toolStripMenu.Name = "toolStripMenu";
            this.toolStripMenu.Size = new System.Drawing.Size(1130, 39);
            this.toolStripMenu.TabIndex = 4;
            this.toolStripMenu.Text = "toolStrip1";
            // 
            // tsbClose
            // 
            this.tsbClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(86, 36);
            this.tsbClose.Text = "Close this tool";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tssSeparator1
            // 
            this.tssSeparator1.Name = "tssSeparator1";
            this.tssSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // tsbSample
            // 
            this.tsbSample.Image = global::OrgSettings.Properties.Resources.iconfinder_Revert_131718;
            this.tsbSample.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbSample.Name = "tsbSample";
            this.tsbSample.Size = new System.Drawing.Size(82, 36);
            this.tsbSample.Text = "Refresh";
            this.tsbSample.Click += new System.EventHandler(this.tsbSample_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::OrgSettings.Properties.Resources.iconfinder_Save_131694;
            this.btnSave.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(87, 36);
            this.btnSave.Text = "Commit";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gvSettings
            // 
            this.gvSettings.AllowUserToAddRows = false;
            this.gvSettings.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gvSettings.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.gvSettings.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gvSettings.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.gvSettings.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.gvSettings.DefaultCellStyle = dataGridViewCellStyle6;
            this.gvSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gvSettings.Location = new System.Drawing.Point(0, 0);
            this.gvSettings.Name = "gvSettings";
            this.gvSettings.Size = new System.Drawing.Size(596, 706);
            this.gvSettings.TabIndex = 5;
            this.gvSettings.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.gvSettings_CellFormatting);
            this.gvSettings.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.gvSettings_ColumnHeaderMouseClick);
            this.gvSettings.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.gvSettings_RowEnter);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 39);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gvSettings);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.splitMainRight);
            this.splitMain.Size = new System.Drawing.Size(1130, 706);
            this.splitMain.SplitterDistance = 596;
            this.splitMain.TabIndex = 6;
            // 
            // splitMainRight
            // 
            this.splitMainRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainRight.Location = new System.Drawing.Point(0, 0);
            this.splitMainRight.Name = "splitMainRight";
            this.splitMainRight.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMainRight.Panel1
            // 
            this.splitMainRight.Panel1.Controls.Add(this.tabGrpMain);
            // 
            // splitMainRight.Panel2
            // 
            this.splitMainRight.Panel2.Controls.Add(this.splitMainLower);
            this.splitMainRight.Size = new System.Drawing.Size(530, 706);
            this.splitMainRight.SplitterDistance = 376;
            this.splitMainRight.TabIndex = 1;
            // 
            // tabGrpMain
            // 
            this.tabGrpMain.Controls.Add(this.tabSet);
            this.tabGrpMain.Controls.Add(this.tabManual);
            this.tabGrpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabGrpMain.Location = new System.Drawing.Point(0, 0);
            this.tabGrpMain.Name = "tabGrpMain";
            this.tabGrpMain.SelectedIndex = 0;
            this.tabGrpMain.Size = new System.Drawing.Size(530, 376);
            this.tabGrpMain.TabIndex = 7;
            // 
            // tabSet
            // 
            this.tabSet.Controls.Add(this.grpAttribute);
            this.tabSet.Location = new System.Drawing.Point(4, 22);
            this.tabSet.Name = "tabSet";
            this.tabSet.Padding = new System.Windows.Forms.Padding(3);
            this.tabSet.Size = new System.Drawing.Size(522, 350);
            this.tabSet.TabIndex = 0;
            this.tabSet.Text = "Set";
            this.tabSet.UseVisualStyleBackColor = true;
            // 
            // grpAttribute
            // 
            this.grpAttribute.Controls.Add(this.grpDouble);
            this.grpAttribute.Controls.Add(this.grpCurrent);
            this.grpAttribute.Controls.Add(this.grpString);
            this.grpAttribute.Controls.Add(this.btnRemove);
            this.grpAttribute.Controls.Add(this.grpNumber);
            this.grpAttribute.Controls.Add(this.grpBool);
            this.grpAttribute.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpAttribute.Location = new System.Drawing.Point(3, 3);
            this.grpAttribute.Name = "grpAttribute";
            this.grpAttribute.Size = new System.Drawing.Size(516, 344);
            this.grpAttribute.TabIndex = 2;
            this.grpAttribute.TabStop = false;
            this.grpAttribute.Text = "Select an Attribute";
            // 
            // grpDouble
            // 
            this.grpDouble.Controls.Add(this.decimalNewValue);
            this.grpDouble.Location = new System.Drawing.Point(203, 20);
            this.grpDouble.Name = "grpDouble";
            this.grpDouble.Size = new System.Drawing.Size(200, 100);
            this.grpDouble.TabIndex = 10;
            this.grpDouble.TabStop = false;
            this.grpDouble.Text = "New Value";
            // 
            // decimalNewValue
            // 
            this.decimalNewValue.DecimalPlaces = 2;
            this.decimalNewValue.Location = new System.Drawing.Point(6, 19);
            this.decimalNewValue.Maximum = new decimal(new int[] {
            1316134912,
            2328,
            0,
            0});
            this.decimalNewValue.Name = "decimalNewValue";
            this.decimalNewValue.Size = new System.Drawing.Size(181, 20);
            this.decimalNewValue.TabIndex = 0;
            this.decimalNewValue.ValueChanged += new System.EventHandler(this.decimalNewValue_ValueChanged);
            this.decimalNewValue.Validated += new System.EventHandler(this.decimalNewValue_Validated);
            // 
            // grpCurrent
            // 
            this.grpCurrent.Controls.Add(this.txtCurrentValue);
            this.grpCurrent.Location = new System.Drawing.Point(0, 20);
            this.grpCurrent.Name = "grpCurrent";
            this.grpCurrent.Size = new System.Drawing.Size(200, 100);
            this.grpCurrent.TabIndex = 9;
            this.grpCurrent.TabStop = false;
            this.grpCurrent.Text = "Current Value";
            // 
            // txtCurrentValue
            // 
            this.txtCurrentValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentValue.Location = new System.Drawing.Point(3, 16);
            this.txtCurrentValue.Multiline = true;
            this.txtCurrentValue.Name = "txtCurrentValue";
            this.txtCurrentValue.ReadOnly = true;
            this.txtCurrentValue.Size = new System.Drawing.Size(194, 81);
            this.txtCurrentValue.TabIndex = 5;
            // 
            // grpString
            // 
            this.grpString.Controls.Add(this.txtStringValue);
            this.grpString.Location = new System.Drawing.Point(203, 20);
            this.grpString.Name = "grpString";
            this.grpString.Size = new System.Drawing.Size(193, 100);
            this.grpString.TabIndex = 8;
            this.grpString.TabStop = false;
            this.grpString.Text = "New Value";
            // 
            // txtStringValue
            // 
            this.txtStringValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtStringValue.Location = new System.Drawing.Point(3, 16);
            this.txtStringValue.Multiline = true;
            this.txtStringValue.Name = "txtStringValue";
            this.txtStringValue.Size = new System.Drawing.Size(187, 81);
            this.txtStringValue.TabIndex = 7;
            this.txtStringValue.TextChanged += new System.EventHandler(this.txtStringValue_TextChanged);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(417, 19);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 49);
            this.btnRemove.TabIndex = 7;
            this.btnRemove.Text = "Remove Value";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // grpNumber
            // 
            this.grpNumber.Controls.Add(this.lblMinNumber);
            this.grpNumber.Controls.Add(this.lblMaxNumber);
            this.grpNumber.Controls.Add(this.numberNew);
            this.grpNumber.Location = new System.Drawing.Point(203, 19);
            this.grpNumber.Name = "grpNumber";
            this.grpNumber.Size = new System.Drawing.Size(200, 101);
            this.grpNumber.TabIndex = 6;
            this.grpNumber.TabStop = false;
            this.grpNumber.Text = "New Value";
            // 
            // lblMinNumber
            // 
            this.lblMinNumber.AutoSize = true;
            this.lblMinNumber.Location = new System.Drawing.Point(15, 47);
            this.lblMinNumber.Name = "lblMinNumber";
            this.lblMinNumber.Size = new System.Drawing.Size(27, 13);
            this.lblMinNumber.TabIndex = 7;
            this.lblMinNumber.Text = "Min:";
            // 
            // lblMaxNumber
            // 
            this.lblMaxNumber.AutoSize = true;
            this.lblMaxNumber.Location = new System.Drawing.Point(91, 47);
            this.lblMaxNumber.Name = "lblMaxNumber";
            this.lblMaxNumber.Size = new System.Drawing.Size(33, 13);
            this.lblMaxNumber.TabIndex = 6;
            this.lblMaxNumber.Text = "Max: ";
            // 
            // numberNew
            // 
            this.numberNew.Location = new System.Drawing.Point(6, 19);
            this.numberNew.Name = "numberNew";
            this.numberNew.Size = new System.Drawing.Size(181, 20);
            this.numberNew.TabIndex = 5;
            this.numberNew.ValueChanged += new System.EventHandler(this.numberNew_ValueChanged);
            // 
            // grpBool
            // 
            this.grpBool.Controls.Add(this.radioFalse);
            this.grpBool.Controls.Add(this.radioTrue);
            this.grpBool.Location = new System.Drawing.Point(203, 19);
            this.grpBool.Name = "grpBool";
            this.grpBool.Size = new System.Drawing.Size(200, 101);
            this.grpBool.TabIndex = 2;
            this.grpBool.TabStop = false;
            this.grpBool.Text = "New Value";
            // 
            // radioFalse
            // 
            this.radioFalse.AutoSize = true;
            this.radioFalse.Location = new System.Drawing.Point(6, 42);
            this.radioFalse.Name = "radioFalse";
            this.radioFalse.Size = new System.Drawing.Size(50, 17);
            this.radioFalse.TabIndex = 2;
            this.radioFalse.TabStop = true;
            this.radioFalse.Text = "False";
            this.radioFalse.UseVisualStyleBackColor = true;
            this.radioFalse.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // radioTrue
            // 
            this.radioTrue.AutoSize = true;
            this.radioTrue.Location = new System.Drawing.Point(6, 19);
            this.radioTrue.Name = "radioTrue";
            this.radioTrue.Size = new System.Drawing.Size(47, 17);
            this.radioTrue.TabIndex = 0;
            this.radioTrue.TabStop = true;
            this.radioTrue.Text = "True";
            this.radioTrue.UseVisualStyleBackColor = true;
            this.radioTrue.CheckedChanged += new System.EventHandler(this.radio_CheckedChanged);
            // 
            // tabManual
            // 
            this.tabManual.Controls.Add(this.splitManual);
            this.tabManual.Location = new System.Drawing.Point(4, 22);
            this.tabManual.Name = "tabManual";
            this.tabManual.Padding = new System.Windows.Forms.Padding(3);
            this.tabManual.Size = new System.Drawing.Size(522, 350);
            this.tabManual.TabIndex = 1;
            this.tabManual.Text = "Manual";
            this.tabManual.UseVisualStyleBackColor = true;
            // 
            // splitManual
            // 
            this.splitManual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitManual.Location = new System.Drawing.Point(3, 3);
            this.splitManual.Name = "splitManual";
            this.splitManual.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitManual.Panel1
            // 
            this.splitManual.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitManual.Panel2
            // 
            this.splitManual.Panel2.Controls.Add(this.splitManualEditor);
            this.splitManual.Size = new System.Drawing.Size(516, 344);
            this.splitManual.SplitterDistance = 168;
            this.splitManual.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtCurrentXml);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(516, 168);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Current Config";
            // 
            // txtCurrentXml
            // 
            this.txtCurrentXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCurrentXml.Location = new System.Drawing.Point(3, 16);
            this.txtCurrentXml.Multiline = true;
            this.txtCurrentXml.Name = "txtCurrentXml";
            this.txtCurrentXml.ReadOnly = true;
            this.txtCurrentXml.Size = new System.Drawing.Size(510, 149);
            this.txtCurrentXml.TabIndex = 0;
            // 
            // splitManualEditor
            // 
            this.splitManualEditor.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitManualEditor.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitManualEditor.IsSplitterFixed = true;
            this.splitManualEditor.Location = new System.Drawing.Point(0, 0);
            this.splitManualEditor.Name = "splitManualEditor";
            this.splitManualEditor.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitManualEditor.Panel1
            // 
            this.splitManualEditor.Panel1.Controls.Add(this.btnUpdateManual);
            this.splitManualEditor.Panel1.Controls.Add(this.btnCopyCurrent);
            // 
            // splitManualEditor.Panel2
            // 
            this.splitManualEditor.Panel2.Controls.Add(this.txtOverride);
            this.splitManualEditor.Size = new System.Drawing.Size(516, 172);
            this.splitManualEditor.SplitterDistance = 43;
            this.splitManualEditor.TabIndex = 2;
            // 
            // btnUpdateManual
            // 
            this.btnUpdateManual.Location = new System.Drawing.Point(271, 10);
            this.btnUpdateManual.Name = "btnUpdateManual";
            this.btnUpdateManual.Size = new System.Drawing.Size(92, 23);
            this.btnUpdateManual.TabIndex = 1;
            this.btnUpdateManual.Text = "Update Settings Manually";
            this.btnUpdateManual.UseVisualStyleBackColor = true;
            this.btnUpdateManual.Click += new System.EventHandler(this.btnUpdateManual_Click);
            // 
            // btnCopyCurrent
            // 
            this.btnCopyCurrent.Location = new System.Drawing.Point(129, 10);
            this.btnCopyCurrent.Name = "btnCopyCurrent";
            this.btnCopyCurrent.Size = new System.Drawing.Size(92, 23);
            this.btnCopyCurrent.TabIndex = 0;
            this.btnCopyCurrent.Text = "Copy Current";
            this.btnCopyCurrent.UseVisualStyleBackColor = true;
            this.btnCopyCurrent.Click += new System.EventHandler(this.btnCopyCurrent_Click);
            // 
            // txtOverride
            // 
            this.txtOverride.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtOverride.Location = new System.Drawing.Point(0, 0);
            this.txtOverride.Multiline = true;
            this.txtOverride.Name = "txtOverride";
            this.txtOverride.Size = new System.Drawing.Size(516, 125);
            this.txtOverride.TabIndex = 1;
            // 
            // splitMainLower
            // 
            this.splitMainLower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMainLower.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitMainLower.Location = new System.Drawing.Point(0, 0);
            this.splitMainLower.Name = "splitMainLower";
            this.splitMainLower.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMainLower.Panel1
            // 
            this.splitMainLower.Panel1.Controls.Add(this.lblTypeValue);
            this.splitMainLower.Panel1.Controls.Add(this.lblType);
            this.splitMainLower.Panel1.Controls.Add(this.lblMax);
            this.splitMainLower.Panel1.Controls.Add(this.lblMaxVersion);
            this.splitMainLower.Panel1.Controls.Add(this.lblMin);
            this.splitMainLower.Panel1.Controls.Add(this.lblMinVersion);
            this.splitMainLower.Panel1.Controls.Add(this.lblDefaultValue);
            this.splitMainLower.Panel1.Controls.Add(this.lblDefault);
            this.splitMainLower.Panel1.Controls.Add(this.lblUrl);
            this.splitMainLower.Panel1.Controls.Add(this.linkURL);
            // 
            // splitMainLower.Panel2
            // 
            this.splitMainLower.Panel2.Controls.Add(this.webDescription);
            this.splitMainLower.Size = new System.Drawing.Size(530, 326);
            this.splitMainLower.TabIndex = 1;
            // 
            // lblDefaultValue
            // 
            this.lblDefaultValue.AutoSize = true;
            this.lblDefaultValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefaultValue.Location = new System.Drawing.Point(63, 9);
            this.lblDefaultValue.Name = "lblDefaultValue";
            this.lblDefaultValue.Size = new System.Drawing.Size(56, 13);
            this.lblDefaultValue.TabIndex = 3;
            this.lblDefaultValue.Text = "Default: ";
            // 
            // lblDefault
            // 
            this.lblDefault.AutoSize = true;
            this.lblDefault.Location = new System.Drawing.Point(13, 9);
            this.lblDefault.Name = "lblDefault";
            this.lblDefault.Size = new System.Drawing.Size(47, 13);
            this.lblDefault.TabIndex = 2;
            this.lblDefault.Text = "Default: ";
            // 
            // lblUrl
            // 
            this.lblUrl.AutoSize = true;
            this.lblUrl.Location = new System.Drawing.Point(406, 22);
            this.lblUrl.Name = "lblUrl";
            this.lblUrl.Size = new System.Drawing.Size(35, 13);
            this.lblUrl.TabIndex = 1;
            this.lblUrl.Text = "URL: ";
            // 
            // linkURL
            // 
            this.linkURL.AutoSize = true;
            this.linkURL.Location = new System.Drawing.Point(444, 22);
            this.linkURL.Name = "linkURL";
            this.linkURL.Size = new System.Drawing.Size(55, 13);
            this.linkURL.TabIndex = 0;
            this.linkURL.TabStop = true;
            this.linkURL.Text = "linkLabel1";
            this.linkURL.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkURL_LinkClicked);
            // 
            // webDescription
            // 
            this.webDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webDescription.Location = new System.Drawing.Point(0, 0);
            this.webDescription.MinimumSize = new System.Drawing.Size(20, 20);
            this.webDescription.Name = "webDescription";
            this.webDescription.Size = new System.Drawing.Size(530, 272);
            this.webDescription.TabIndex = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblMin
            // 
            this.lblMin.AutoSize = true;
            this.lblMin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMin.Location = new System.Drawing.Point(220, 9);
            this.lblMin.Name = "lblMin";
            this.lblMin.Size = new System.Drawing.Size(56, 13);
            this.lblMin.TabIndex = 5;
            this.lblMin.Text = "Default: ";
            // 
            // lblMinVersion
            // 
            this.lblMinVersion.AutoSize = true;
            this.lblMinVersion.Location = new System.Drawing.Point(149, 9);
            this.lblMinVersion.Name = "lblMinVersion";
            this.lblMinVersion.Size = new System.Drawing.Size(71, 13);
            this.lblMinVersion.TabIndex = 4;
            this.lblMinVersion.Text = "Min Version:  ";
            // 
            // lblMax
            // 
            this.lblMax.AutoSize = true;
            this.lblMax.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMax.Location = new System.Drawing.Point(220, 27);
            this.lblMax.Name = "lblMax";
            this.lblMax.Size = new System.Drawing.Size(56, 13);
            this.lblMax.TabIndex = 7;
            this.lblMax.Text = "Default: ";
            // 
            // lblMaxVersion
            // 
            this.lblMaxVersion.AutoSize = true;
            this.lblMaxVersion.Location = new System.Drawing.Point(149, 27);
            this.lblMaxVersion.Name = "lblMaxVersion";
            this.lblMaxVersion.Size = new System.Drawing.Size(74, 13);
            this.lblMaxVersion.TabIndex = 6;
            this.lblMaxVersion.Text = "Max Version:  ";
            // 
            // lblTypeValue
            // 
            this.lblTypeValue.AutoSize = true;
            this.lblTypeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTypeValue.Location = new System.Drawing.Point(63, 28);
            this.lblTypeValue.Name = "lblTypeValue";
            this.lblTypeValue.Size = new System.Drawing.Size(56, 13);
            this.lblTypeValue.TabIndex = 9;
            this.lblTypeValue.Text = "Default: ";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(13, 28);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(37, 13);
            this.lblType.TabIndex = 8;
            this.lblType.Text = "Type: ";
            // 
            // OrgSettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.toolStripMenu);
            this.Name = "OrgSettingsControl";
            this.Size = new System.Drawing.Size(1130, 745);
            this.Load += new System.EventHandler(this.OrgSettingsControl_Load);
            this.toolStripMenu.ResumeLayout(false);
            this.toolStripMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSettings)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitMainRight.Panel1.ResumeLayout(false);
            this.splitMainRight.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainRight)).EndInit();
            this.splitMainRight.ResumeLayout(false);
            this.tabGrpMain.ResumeLayout(false);
            this.tabSet.ResumeLayout(false);
            this.grpAttribute.ResumeLayout(false);
            this.grpDouble.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.decimalNewValue)).EndInit();
            this.grpCurrent.ResumeLayout(false);
            this.grpCurrent.PerformLayout();
            this.grpString.ResumeLayout(false);
            this.grpString.PerformLayout();
            this.grpNumber.ResumeLayout(false);
            this.grpNumber.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numberNew)).EndInit();
            this.grpBool.ResumeLayout(false);
            this.grpBool.PerformLayout();
            this.tabManual.ResumeLayout(false);
            this.splitManual.Panel1.ResumeLayout(false);
            this.splitManual.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitManual)).EndInit();
            this.splitManual.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.splitManualEditor.Panel1.ResumeLayout(false);
            this.splitManualEditor.Panel2.ResumeLayout(false);
            this.splitManualEditor.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitManualEditor)).EndInit();
            this.splitManualEditor.ResumeLayout(false);
            this.splitMainLower.Panel1.ResumeLayout(false);
            this.splitMainLower.Panel1.PerformLayout();
            this.splitMainLower.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMainLower)).EndInit();
            this.splitMainLower.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStripMenu;
        private System.Windows.Forms.ToolStripButton tsbClose;
        private System.Windows.Forms.ToolStripButton tsbSample;
        private System.Windows.Forms.ToolStripSeparator tssSeparator1;
        private System.Windows.Forms.DataGridView gvSettings;
        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.WebBrowser webDescription;
        private System.Windows.Forms.SplitContainer splitMainRight;
        private System.Windows.Forms.GroupBox grpAttribute;
        private System.Windows.Forms.GroupBox grpBool;
        private System.Windows.Forms.RadioButton radioFalse;
        private System.Windows.Forms.RadioButton radioTrue;
        private System.Windows.Forms.NumericUpDown numberNew;
        private System.Windows.Forms.GroupBox grpNumber;
        private System.Windows.Forms.Label lblMinNumber;
        private System.Windows.Forms.Label lblMaxNumber;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.TabControl tabGrpMain;
        private System.Windows.Forms.TabPage tabSet;
        private System.Windows.Forms.TabPage tabManual;
        private System.Windows.Forms.SplitContainer splitManual;
        private System.Windows.Forms.TextBox txtOverride;
        private System.Windows.Forms.TextBox txtCurrentXml;
        private System.Windows.Forms.SplitContainer splitManualEditor;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnCopyCurrent;
        private System.Windows.Forms.Button btnUpdateManual;
        private System.Windows.Forms.SplitContainer splitMainLower;
        private System.Windows.Forms.Label lblDefault;
        private System.Windows.Forms.Label lblUrl;
        private System.Windows.Forms.LinkLabel linkURL;
        private System.Windows.Forms.Label lblDefaultValue;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.GroupBox grpCurrent;
        private System.Windows.Forms.TextBox txtCurrentValue;
        private System.Windows.Forms.GroupBox grpString;
        private System.Windows.Forms.TextBox txtStringValue;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.GroupBox grpDouble;
        private System.Windows.Forms.NumericUpDown decimalNewValue;
        private System.Windows.Forms.Label lblTypeValue;
        private System.Windows.Forms.Label lblType;
        private System.Windows.Forms.Label lblMax;
        private System.Windows.Forms.Label lblMaxVersion;
        private System.Windows.Forms.Label lblMin;
        private System.Windows.Forms.Label lblMinVersion;
    }
}
