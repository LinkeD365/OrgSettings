using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace LinkeD365.OrgSettings
{
    internal class OrgSettingsManager
    {
    }

    public class OrgSetting : IEquatable<OrgSetting>
    {
        [DisplayName(@"Setting Name")]
        public string Name { get; set; }

        public string CurrentSetting { get; set; }

        public string NewSetting { get; set; }

        public string SecondaryCurrentSetting { get; set; }

        public string SecondaryNewSetting { get; set; }

        [Browsable(false)]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Browsable(false)]
        public string MinVersion { get; set; }

        [Browsable(false)]
        public string MaxVersion { get; set; }

        [Browsable(false)]
        public bool OrgAttribute { get; set; }

        [Browsable(false)]
        public string MinValue { get; set; }

        [Browsable(false)]
        public string MaxValue { get; set; }

        [Browsable(false)]
        public string DefaultValue { get; set; }

        [Browsable(false)]
        public string Type { get; set; }

        [Browsable(false)]
        public string Url { get; set; }

        [Browsable(false)]
        public string UrlTitle { get; set; }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }

            OrgSetting os = (OrgSetting)obj;
            return (Name == os.Name);
        }

        public bool Equals(OrgSetting other)
        {
            return other != null &&
                   Name == other.Name;
        }

        public override int GetHashCode()
        {
            return 363513814 + EqualityComparer<string>.Default.GetHashCode(Name);
        }

        [Browsable(false)]
        public string LinkeD365Url { get; set; }

        [Browsable(false)]
        public string LinkeD365Description { get; set; }
    }

    internal class OrgSettngComparer : IComparer<OrgSetting>
    {
        private string _colName = string.Empty; // specifies the member name to be sorted
        private SortOrder _sortOrder = SortOrder.None; // Specifies the SortOrder.

        /// <summary>
        /// constructor to set the sort column and sort order.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="sortingOrder"></param>
        public OrgSettngComparer(string columnName, SortOrder sortingOrder)
        {
            _colName = columnName;
            _sortOrder = sortingOrder;
        }

        /// <summary>
        /// Compares two Students based on member name and sort order
        /// and return the result.
        /// </summary>
        /// <param name="Student1"></param>
        /// <param name="Student2"></param>
        /// <returns></returns>
        public int Compare(OrgSetting os1, OrgSetting os2)
        {
            string compare1 = string.Empty;
            string compare2 = string.Empty;
            switch (_colName)
            {
                case "CurrentSetting":
                    compare1 = os1?.CurrentSetting ?? string.Empty;
                    compare2 = os2?.CurrentSetting ?? string.Empty;
                    break;

                case "NewSetting":
                    compare1 = os1?.NewSetting ?? string.Empty;
                    compare2 = os2?.NewSetting ?? string.Empty;
                    break;
                case "SecondaryCurrentSetting":
                    compare1 = os1?.SecondaryCurrentSetting ?? string.Empty;
                    compare2 = os2?.SecondaryCurrentSetting ?? string.Empty;
                    break;

                case "SecondaryNewSetting":
                    compare1 = os1?.SecondaryNewSetting ?? string.Empty;
                    compare2 = os2?.SecondaryNewSetting ?? string.Empty;
                    break;

                default:
                    compare1 = os1?.Name ?? string.Empty;
                    compare2 = os2?.Name ?? string.Empty;
                    break;
            }
            if (_sortOrder == SortOrder.Ascending)
            {
                return compare1.CompareTo(compare2);
            }
            else
            {
                return compare2.CompareTo(compare1);
            }
        }
    }
}