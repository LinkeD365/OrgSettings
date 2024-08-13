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
    internal class EnvProp
    {
        public string Type { get; set; }
        public string Group { get; set; }
        public string Label { get; set; }

        [Browsable(false)]
        public string Path { get; set; }

        public string UpdatePath { get; set; }

        [Browsable(false)]
        public string Editable { get; set; }

        [Browsable(false)]
        public string OldValue { get; set; }

        [Browsable(false)]
        public string NewValue { get; set; }

        [DisplayName("New Value")]
        public string New { get; set; }

        [DisplayName("Old Value")]
        public string Old { get { return OldValue; } }


        [Browsable(false)]
        public List<EnvOption> Options { get; set; }
    }

    internal class EnvOption
    {
        public string Label { get; set; }
        public string Value { get; set; }
    }
}
