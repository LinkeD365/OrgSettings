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
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
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
            int returnValue;
            switch (_colName)
            {
                case "name":
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        return  os1.Name.CompareTo(os2.Name);
                    }
                    else
                    {
                        return  os2.Name.CompareTo(os1.Name);
                    }

                    break;
                case "currentSetting":
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        return  String.Compare(os1.CurrentSetting, os2.CurrentSetting, StringComparison.Ordinal);
                    }
                    else
                    {
                        if (os2?.CurrentSetting != null) return os2.CurrentSetting.CompareTo(os1.CurrentSetting);
                        if (os1?.CurrentSetting == null) return 0;

                    }
                    break;

                case "newSetting":
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        if (os1?.NewSetting != null) return  os1.NewSetting.CompareTo(os2.NewSetting);
                        if (os2?.NewSetting == null) return 0;
                    }
                    else
                    {
                        if (os2?.NewSetting != null) return  os2.NewSetting.CompareTo(os1.NewSetting);
                        if (os1?.NewSetting == null) return 0;
                    }
                    break;
                case "secondaryCurrentSetting":
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        if (os1?.SecondaryCurrentSetting != null)
                            return os1.SecondaryCurrentSetting.CompareTo(os2.SecondaryCurrentSetting);
                        if (os2?.NewSetting == null) return 0;

                    }
                    else
                    {
                        if (os2?.SecondaryCurrentSetting != null)
                            return os2.SecondaryCurrentSetting.CompareTo(os1.SecondaryCurrentSetting);
                        if ( os1?.NewSetting == null) return 0;

                    }
                    break;
                case "secondaryNewSetting":
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        if (os1?.SecondaryNewSetting != null)
                            return os1.SecondaryNewSetting.CompareTo(os2.SecondaryNewSetting);
                        if (os2 != null && os2.NewSetting == null) return 0;

                    }
                    else
                    {
                        if (os2?.SecondaryNewSetting != null)
                            return os2.SecondaryNewSetting.CompareTo(os1.SecondaryNewSetting);
                        if (os1?.NewSetting == null) return 0;

                    }
                    break;

                default:
                    if (_sortOrder == SortOrder.Ascending)
                    {
                        return os1.Name.CompareTo(os2.Name);
                    }
                    else
                    {
                        return os2.Name.CompareTo(os1.Name);
                    }
                    break;
            }
            return 1;
        }
    }
}