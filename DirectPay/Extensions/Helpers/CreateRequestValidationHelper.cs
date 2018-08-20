using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DirectPay.Infrastructure;
using DirectPay.Models.Request;
using DirectPay.Models.Shared;

namespace DirectPay.Extensions.Helpers
{
    //TODO: Try sending this on a diet
    public static class ModelValidationHelpers
    {
        public static IEnumerable<ValidationResult> Validate(this CreateTokenModel requestData)
        {
            var validationResults = new List<ValidationResult>();

            validationResults.AddRange(ClassValidator<CreateTokenModel>.Validate(requestData));
            validationResults.AddRange(ClassValidator<TransactionModel>.Validate(requestData.Transaction ?? new TransactionModel()));


            foreach (var service in requestData.Services ?? new List<ServiceModel>())
            {
                validationResults.AddRange(ClassValidator<ServiceModel>.Validate(service));
            }

            foreach (var allocation in requestData.Allocations ?? new List<AllocationModel>())
            {
                validationResults.AddRange(ClassValidator<AllocationModel>.Validate(allocation));
            }

            foreach (var traveler in requestData.Travelers ?? new List<TravelerModel>())
            {
                validationResults.AddRange(ClassValidator<TravelerModel>.Validate(traveler));
            }

            return validationResults;
        }
        
        public static IEnumerable<ValidationResult> Validate(this UpdateTokenModel requestData)
        {
            var validationResults = new List<ValidationResult>();

            validationResults.AddRange(ClassValidator<UpdateTokenModel>.Validate(requestData));
            validationResults.AddRange(ClassValidator<TransactionModel>.Validate(requestData.Transaction ?? new TransactionModel()));


            foreach (var service in requestData.Services ?? new List<ServiceModel>())
            {
                validationResults.AddRange(ClassValidator<ServiceModel>.Validate(service));
            }

            foreach (var allocation in requestData.Allocations ?? new List<AllocationModel>())
            {
                validationResults.AddRange(ClassValidator<AllocationModel>.Validate(allocation));
            }

            foreach (var traveler in requestData.Travelers ?? new List<TravelerModel>())
            {
                validationResults.AddRange(ClassValidator<TravelerModel>.Validate(traveler));
            }

            return validationResults;
        }
        
        public static IEnumerable<ValidationResult> Validate(this CreditCardModel requestData)
        {
            var validationResults = new List<ValidationResult>();

            validationResults.AddRange(ClassValidator<CreditCardModel>.Validate(requestData));

            return validationResults;
        }
    }
}