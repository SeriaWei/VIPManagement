using Easy.HTML.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace Easy.WPF
{
    public class ValidationRuleProvide : ValidationRule
    {
        private ValidatorBase _validator;
        public ValidationRuleProvide(ValidatorBase validator)
        {
            _validator = validator;
        }
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            return new ValidationResult(_validator.Validate(value), _validator.ErrorMessage);
        }
    }
}
