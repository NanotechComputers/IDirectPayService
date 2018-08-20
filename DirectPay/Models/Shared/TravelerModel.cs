using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DirectPay.Models.Shared
{
    public class TravelerModel : IValidatableObject
    {
        [Required]
        public string TravelerFirstName { get; set; }

        [Required]
        public string TravelerLastName { get; set; }

        public string TravelerPhone { get; set; }
        public string TravelerPhonePrefix { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            Validator.TryValidateProperty(TravelerFirstName, new ValidationContext(this, null, null) {MemberName = "TravelerFirstName"}, results);
            Validator.TryValidateProperty(TravelerLastName, new ValidationContext(this, null, null) {MemberName = "TravelerLastName"}, results);

            return results;
        }
    }
}