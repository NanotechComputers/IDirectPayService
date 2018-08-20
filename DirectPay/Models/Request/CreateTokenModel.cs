using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DirectPay.Models.Shared;

namespace DirectPay.Models.Request
{
    public class CreateTokenModel : IValidatableObject
    {
        //Transaction Level
        [Required]
        public TransactionModel Transaction { get; set; }

        public CustomerModel Customer { get; set; } = new CustomerModel();

        //Services Level
        [Required]
        public List<ServiceModel> Services { get; set; }

        //Allocation Level
        public List<AllocationModel> Allocations { get; set; } = new List<AllocationModel>();

        //Allocation Level
        public List<TravelerModel> Travelers { get; set; } = new List<TravelerModel>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(Transaction, new ValidationContext(this, null, null) {MemberName = "Transaction"}, results);
            Validator.TryValidateProperty(Services, new ValidationContext(this, null, null) {MemberName = "Services"}, results);

            return results;
        }
    }
}