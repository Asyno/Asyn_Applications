using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_DataBinding_Validation
{
    public class Person : IDataErrorInfo
    {
        public string Error { get { return null; } }

        public string this[string propertyName]
        {
            get
            {
                if(propertyName=="Alter")
                {
                    if (_Alter < 0)
                        return "Das Alter darf nicht negativ sein.";
                }
                return null;
            }
        }

        private string _Name;
        public string Name { set { _Name = value; } get { return _Name; } }

        private int? _Alter;
        public int? Alter { set { _Alter = value; } get { return _Alter; } }
    }
}
