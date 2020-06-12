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
        public string name { get; set; }

        public string currentSetting { get; set; }

        public string newSetting { get; set; }

        [Browsable(false)]
        [DisplayName("Description")]
        public string description { get; set; }

        [Browsable(false)]
        public string minVersion { get; set; }

        [Browsable(false)]
        public string maxVersion { get; set; }

        [Browsable(false)]
        public bool orgAttribute { get; set; }

        [Browsable(false)]
        public string minValue { get; set; }

        [Browsable(false)]
        public string maxValue { get; set; }

        [Browsable(false)]
        public string defaultValue { get; set; }

        [Browsable(false)]
        public string type { get; set; }

        [Browsable(false)]
        public string url { get; set; }

        [Browsable(false)]
        public string urlTitle { get; set; }

        public override bool Equals(object obj)
        {
            //Check for null and compare run-time types.
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                OrgSetting os = (OrgSetting)obj;
                return (name == os.name);
            }
        }

        public bool Equals(OrgSetting other)
        {
            return other != null &&
                   name == other.name;
        }

        public override int GetHashCode()
        {
            return 363513814 + EqualityComparer<string>.Default.GetHashCode(name);
        }
    }

    internal class orgSettngComparer : IComparer<OrgSetting>
    {
        private string colName = string.Empty; // specifies the member name to be sorted
        private SortOrder sortOrder = SortOrder.None; // Specifies the SortOrder.

        /// <summary>
        /// constructor to set the sort column and sort order.
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="sortingOrder"></param>
        public orgSettngComparer(string columnName, SortOrder sortingOrder)
        {
            colName = columnName;
            sortOrder = sortingOrder;
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
            int returnValue = 1;
            switch (colName)
            {
                case "name":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = os1.name.CompareTo(os2.name);
                    }
                    else
                    {
                        returnValue = os2.name.CompareTo(os1.name);
                    }

                    break;

                case "currentSetting":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = os1.currentSetting.CompareTo(os2.currentSetting);
                    }
                    else
                    {
                        returnValue = os2.currentSetting.CompareTo(os1.currentSetting);
                    }
                    break;

                case "newSetting":
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = os1.newSetting.CompareTo(os2.newSetting);
                    }
                    else
                    {
                        returnValue = os2.newSetting.CompareTo(os1.newSetting);
                    }
                    break;

                default:
                    if (sortOrder == SortOrder.Ascending)
                    {
                        returnValue = os1.name.CompareTo(os2.name);
                    }
                    else
                    {
                        returnValue = os2.name.CompareTo(os1.name);
                    }
                    break;
            }
            return returnValue;
        }
    }
}