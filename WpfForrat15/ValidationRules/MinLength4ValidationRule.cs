using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfForrat15.ValidationRules
{
    public class MinLength4ValidationRule : ValidationRule
    {


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int MinLength = 4;

            var str = value?.ToString() ?? string.Empty;

            return str.Length < MinLength
                ? new ValidationResult(false, "Минимальная длина 4 символа")
                : ValidationResult.ValidResult;
        }
    }
}
