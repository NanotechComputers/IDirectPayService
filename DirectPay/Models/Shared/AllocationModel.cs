// ReSharper disable ClassNeverInstantiated.Global

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// ReSharper disable MemberCanBePrivate.Global

namespace DirectPay.Models.Shared
{
    public class AllocationModel : IValidatableObject
    {
        [Required]
        public string AllocationCode { get; set; }

        [Required]
        public decimal AllocationAmount { get; set; }

        [Required]
        public int AllocationServiceType { get; set; }

        public string AllocationServiceDescription { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(AllocationCode, new ValidationContext(this, null, null) {MemberName = "AllocationCode"}, results);
            Validator.TryValidateProperty(AllocationAmount, new ValidationContext(this, null, null) {MemberName = "AllocationAmount"}, results);
            Validator.TryValidateProperty(AllocationServiceType, new ValidationContext(this, null, null) {MemberName = "AllocationServiceType"}, results);

            return results;
        }
    }
}