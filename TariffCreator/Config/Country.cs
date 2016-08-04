using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TariffCreator.Config
{
    public class Country : IDataErrorInfo
    {
        public string Description { get; set; }
        public string Prefix { get; set; }
        public float? PriceMin { get; set; }
        public float? PriceCall { get; set; }

        public Country() { }
        public Country(string desc, string prefix)
        {
            Description = desc;
            Prefix = prefix;
        }

        /// <summary>
        /// Not implemented
        /// </summary>
        public string Error
        {
            get { return null; }
        }

        /// <summary>
        /// Validation for the Prefix and the Description field.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get
            {
                if (columnName == "Description")
                {
                    if (string.IsNullOrWhiteSpace(Description))
                        return "This Value couldn't be empty.";
                }
                else if (columnName == "Prefix")
                {
                    if (string.IsNullOrWhiteSpace(Prefix))
                        return "This Value couldn't be empty.";
                }
                return null;
            }
        }
    }
}
