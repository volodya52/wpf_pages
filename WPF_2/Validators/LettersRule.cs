using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_2.Validators
{
    public  class LettersRule: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value.ToString();
            if (!strValue.All(c => char.IsLetter(c) || c == ' '))
                return new ValidationResult(false, "Только буквы разрешены");

            return ValidationResult.ValidResult;
        }
    }
}
