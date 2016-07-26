
using System.Globalization;
using System.Windows.Controls;

namespace WPF_Data_Binding
{
    public class TextValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo culturInfo)
        {
            if (value == null || value as string == "")
                return new ValidationResult(false, "Das Feld darf nicht leer sein.");
            return new ValidationResult(true, null);
        }
    }
}
