using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WPF_2.Validators
{
    public  class NumericRule:ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string strValue = value.ToString();
            if (!long.TryParse(strValue, out long result))
                return new ValidationResult(false, "Только цифры");
           
            return ValidationResult.ValidResult;
        }
    }
}
