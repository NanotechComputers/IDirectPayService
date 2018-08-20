using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectPay.Models.Shared
{
    public class CreditCardModel : IValidatableObject
    {
        [Required]
        [CreditCard]
        public string CreditCardNumber { get; set; }  
        
        [Required]
        [StringLength(4)]
        public string CreditCardExpiry { get; set; }   
        
        [Required]
        [MinLength(3)]
        [MaxLength(4)]
        public string CreditCardCvv { get; set; }   
        
        [Required]
        public string CardHolderName { get; set; }   
        
        //public string ChargeType { get; set; }    
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(CreditCardNumber, new ValidationContext(this, null, null) {MemberName = "CreditCardNumber"}, results);
            Validator.TryValidateProperty(CreditCardExpiry, new ValidationContext(this, null, null) {MemberName = "CreditCardExpiry"}, results);
            Validator.TryValidateProperty(CreditCardCvv, new ValidationContext(this, null, null) {MemberName = "CreditCardCvv"}, results);
            Validator.TryValidateProperty(CardHolderName, new ValidationContext(this, null, null) {MemberName = "CardHolderName"}, results);

            return results;
        }
    }
}