using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Data_Binding
{
    static class ExtensionMethods
    {
        public static T To<T>(this string text)
        {
            return (T)Convert.ChangeType(text, typeof(T));
        }
    }
}
