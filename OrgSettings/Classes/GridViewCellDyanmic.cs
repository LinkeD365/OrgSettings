using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LinkeD365.OrgSettings
{
    // Define a column that will create an appropriate type of edit control as needed.
    public class OptionalDropdownColumn : DataGridViewColumn
    {
        public OptionalDropdownColumn()
            : base(new OptionalDropdownCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                // Ensure that the cell used for the template is a PropertyCell.
                if (value != null &&
                    !value.GetType().IsAssignableFrom(typeof(OptionalDropdownCell)))
                {
                    throw new InvalidCastException("Must be a PropertyCell");
                }
                base.CellTemplate = value;
            }
        }
    }

    // And the corresponding Cell type
    public class OptionalDropdownCell : DataGridViewTextBoxCell
    {

        public OptionalDropdownCell()
            : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object
            initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            // Set the value of the editing control to the current cell value.
            base.InitializeEditingControl(rowIndex, initialFormattedValue,
                dataGridViewCellStyle);

            EnvProp dataItem = (EnvProp)OwningRow.DataBoundItem;
            switch (dataItem.Type)
            {
                case "Choice":
                    DataGridViewComboBoxEditingControl ctl = (DataGridViewComboBoxEditingControl)DataGridView.EditingControl;
                    ctl.ValueMember = "Value";
                    ctl.DisplayMember = "Label";
                    ctl.DataSource = dataItem.Options;
                    ctl.DropDownStyle = ComboBoxStyle.DropDownList;
                    break;
                case "Toggle":
                    CheckBoxEditingControl ctlChk = (CheckBoxEditingControl)DataGridView.EditingControl;
                    ctlChk.Checked = (Value != null) && (bool)Value;
                    break;
                default:
                    DataGridViewTextBoxEditingControl ctl2 = (DataGridViewTextBoxEditingControl)DataGridView.EditingControl;
                    ctl2.Text = Value.ToString();
                    break;
            }

        }

        public override Type EditType
        {
            get
            {
                EnvProp dataItem = (EnvProp)OwningRow.DataBoundItem;
                switch (dataItem.Type)
                {
                    case "Choice":
                        return typeof(DataGridViewComboBoxEditingControl);
                    case "Toggle":
                        return typeof(CheckBoxEditingControl);
                    default:
                        return typeof(DataGridViewTextBoxEditingControl);
                }
                if (dataItem.Type == "Choice")
                {
                    return typeof(DataGridViewComboBoxEditingControl);
                }
                else
                {
                    return typeof(DataGridViewTextBoxEditingControl);
                }
            }
        }
    }

    class CheckBoxEditingControl : CheckBox, IDataGridViewEditingControl
    {
        DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;
        public CheckBoxEditingControl()
        {
            Checked = false;
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return Checked.ToString();
            }
            set
            {
                if (value is bool)
                {
                    Checked = (bool)value;
                }
            }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            Font = dataGridViewCellStyle.Font;
            ForeColor = dataGridViewCellStyle.ForeColor;
            BackColor = dataGridViewCellStyle.BackColor;
        }

        public int EditingControlRowIndex { get => rowIndex; set { rowIndex = value; } }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            switch (keyData & Keys.KeyCode)
            {
                case Keys.Space:
                    return true;
                default:
                    return !dataGridViewWantsInputKey;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return false;
            }
        }

        public DataGridView EditingControlDataGridView { get => dataGridView; set { dataGridView = value; } }

        public bool EditingControlValueChanged { get => valueChanged; set { valueChanged = value; } }

        public Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

        protected override void OnCheckedChanged(EventArgs eventargs)
        {
            // Notify the DataGridView that the contents of the cell
            // have changed.
            valueChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnCheckedChanged(eventargs);
        }

        protected override void OnCheckStateChanged(EventArgs e)
        {
            valueChanged = true;
            EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnCheckStateChanged(e);
        }
    }
}
