using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectPay.Models.Shared
{
    public class TransactionModel : IValidatableObject
    {
        public string BackUrl { get; set; }
        public string DeclinedUrl { get; set; }
        public string RedirectUrl { get; set; }

        [Required]
        [Range(0.01, 99999999.99)]
        public decimal PaymentAmount { get; set; }

        [Required]
        public Currencies Currency { get; set; }

        [Required]
        public string CompanyRef { get; set; }

        public string CompanyRefUnique { get; set; }
        public string TransactionChargeType { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(PaymentAmount, new ValidationContext(this, null, null) {MemberName = "PaymentAmount"}, results);
            Validator.TryValidateProperty(Currency, new ValidationContext(this, null, null) {MemberName = "Currency"}, results);
            Validator.TryValidateProperty(CompanyRef, new ValidationContext(this, null, null) {MemberName = "CompanyRef"}, results);

            return results;
        }
    }
}