using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfForrat15.ValidationRules
{
    public class MinLengthValidationRule : ValidationRule
    {

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int MinLength = 2;
            var str = value?.ToString() ?? string.Empty;

            return str.Length < MinLength
                ? new ValidationResult(false, $"Минимальная длина 2 символа")
                : ValidationResult.ValidResult;
        }
    }
}
