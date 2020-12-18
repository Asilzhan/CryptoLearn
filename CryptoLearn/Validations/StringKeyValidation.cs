using System;
using System.Globalization;
using System.Linq;
using System.Windows.Controls;
using CryptoLearn.Models;

namespace CryptoLearn.Validations
{
    public class StringKeyValidation : ValidationRule
    {
        public string Alphabet { get; set; }
        
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string key = value?.ToString() ?? "";
            foreach (var t in key)
            {
                if (Alphabet.IndexOf(t, StringComparison.OrdinalIgnoreCase) == -1)
                    return new ValidationResult(false, "Алфавит тілі дұрыс емес!");
            }
            return ValidationResult.ValidResult;
        }
    }
}