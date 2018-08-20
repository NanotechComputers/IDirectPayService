using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectPay.Infrastructure
{
    public static class ClassValidator<T>
    {
        public static List<ValidationResult> Validate(T data, bool validateAllProperties = true)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(data, new ValidationContext(data, null, null), results, validateAllProperties);
            return results;
        }
    }
}