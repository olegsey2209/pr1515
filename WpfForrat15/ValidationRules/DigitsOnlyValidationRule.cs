using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfForrat15.ValidationRules
{
    public class DigitsOnlyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var text = value as string;

            if (string.IsNullOrWhiteSpace(text))
                return new ValidationResult(false, "Поле обязательно для заполнения");

            if (!Regex.IsMatch(text, @"^\d+$"))
                return new ValidationResult(false, "Допустимы только цифры");

            return ValidationResult.ValidResult;
        }
    }
}
