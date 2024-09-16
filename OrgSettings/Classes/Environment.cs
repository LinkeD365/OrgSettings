using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkeD365.OrgSettings
{
    internal class EnvProps
    {
        public string EnvId { get; set; }
        public List<EnvProp> Properties { get; set; }
    }
    public class EnvProp
    {
        /// <summary>
        /// Name of Property
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Data type of displayed attribute
        /// </summary>
        public string Type { get; set; }
        public string Group { get; set; }
        public string Label { get; set; }

        [Browsable(false)]
        public string Path { get; set; }

        public string UpdatePath { get; set; }

        [Browsable(false)]
        public bool Editable { get; set; }

        [Browsable(false)]
        public string EditableLink { get; set; }

      //  [Browsable(false)]
        public bool Enabled { get; set; }
        /// <summary>
        /// Value that is present from the API call
        /// </summary>
        [Browsable(false)]
        public string OldValue { get; set; }

        /// <summary>
        /// Valkue to be set to the API
        /// </summary>
        [Browsable(false)]
        public string NewValue { get; set; }

        /// <summary>
        /// Displayed new value for user
        /// </summary>
        [DisplayName("New Value")]
        public string New
        {
            get
            {
                if (Type == "Toggle")
                {
                    return NewValue == null ? string.Empty : Options.FirstOrDefault(opt => opt.Value == NewValue).Label;
                }
                return NewValue;
            }
        }

        /// <summary>
        /// Displayed old value for user
        /// </summary>
        [DisplayName("Old Value")]
        public string Old
        {
            get
            {
                if (Type == "Toggle" && !string.IsNullOrEmpty( OldValue ))
                {
                    return Options.FirstOrDefault(opt => opt.Value == OldValue).Label;
                }
                return OldValue;
            }
        }
        public string Default { get; set; }

        public int MaxValue { get; set; }
        public int MinValue { get; set; }

        [Browsable(false)]
        public List<EnvOption> Options { get; set; }
    }

    public class EnvOption
    {
        public string Label { get; set; }
        public string Value { get; set; }

        public int IntValue { get; set; }
    }
}
