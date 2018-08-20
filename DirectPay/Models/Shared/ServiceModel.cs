using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectPay.Models.Shared
{
    public class ServiceModel : IValidatableObject
    {
        [Required]
        public string ServiceDescription { get; set; }

        [Required]
        public int ServiceType { get; set; }

        [Required]
        public DateTime ServiceDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(ServiceDescription, new ValidationContext(this, null, null) {MemberName = "ServiceDescription"}, results);
            Validator.TryValidateProperty(ServiceType, new ValidationContext(this, null, null) {MemberName = "ServiceType"}, results);
            Validator.TryValidateProperty(ServiceDate, new ValidationContext(this, null, null) {MemberName = "ServiceDate"}, results);

            return results;
        }
    }
}