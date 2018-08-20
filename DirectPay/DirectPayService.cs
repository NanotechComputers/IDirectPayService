using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using DirectPay.Extensions;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates;
using DirectPay.Models.Request;
using DirectPay.Models.Response;
using DirectPay.Models.Shared;
using Microsoft.Extensions.Options;
using ServiceStack;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Local
// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace DirectPay
{
    public class DirectPayService : IDirectPayService
    {
        private bool _bypassCdataMod;
        private string _url { get; set; }

        private string _companyToken { get; set; }

        public DirectPayService(IOptions<DirectPayServiceOptions> options)
        {
            _url = options.Value.Url;
            _companyToken = options.Value.CompanyToken;
            _bypassCdataMod = options.Value.BypassCdataMod;
        }

        public DirectPayService(string url, string companyToken, bool bypassCdataMod = false)
        {
            _url = url;
            _companyToken = companyToken;
            _bypassCdataMod = bypassCdataMod;
        }

        /// <inheritdoc />
        public CreateTokenResponse CreateToken(CreateTokenModel requestData)
        {
            var validationResults = requestData.Validate().ToList();
            if (validationResults.Any())
            {
                return new CreateTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = validationResults.Select(x => x.ErrorMessage).Aggregate((i, j) => i + "," + j)
                };
            }

            var xml = CreateTokenRequestXml.Get(_companyToken, requestData);

            //Get the response
            var responseXml = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod);
            var response = responseXml.FromXml<CreateTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }

        // <inheritdoc />
        /*public UpdateTokenResponse UpdateToken(string transactionToken, UpdateTokenModel requestData)
        {
            var validationResults = requestData.Validate().ToList();
            if (validationResults.Any())
            {
                return new UpdateTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = validationResults.Select(x => x.ErrorMessage).Aggregate((i, j) => i + "," + j)
                };
            }

            var xml = UpdateTokenRequestXml.Get(_companyToken, transactionToken, requestData);

            //Get the response
            var responseXml = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod);
            var response = responseXml.FromXml<UpdateTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }*/

        /// <inheritdoc />
        public CancelTokenResponse CancelToken(string transactionToken)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new CancelTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            var xml = CancelTokenRequestXml.Get(_companyToken, transactionToken);

            //Get the response
            var response = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod).FromXml<CancelTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public RefundTokenResponse RefundToken(string transactionToken, decimal refundAmount, string refundDetails)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new RefundTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            if (refundAmount <= 0)
            {
                return new RefundTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "refundAmount needs to be a value greater than 0"
                };
            }

            if (string.IsNullOrWhiteSpace(refundDetails))
            {
                return new RefundTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "refundDetails field is required"
                };
            }

            var xml = RefundTokenRequestXml.Get(_companyToken, transactionToken, refundAmount, refundDetails);

            //Get the response
            var response = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod).FromXml<RefundTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public ChargeTokenResponse ChargeToken(string transactionToken, CreditCardModel model)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new ChargeTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            var validationResults = model.Validate().ToList();
            if (validationResults.Any())
            {
                return new ChargeTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = validationResults.Select(x => x.ErrorMessage).Aggregate((i, j) => i + "," + j)
                };
            }

            var xml = ChargeTokenRequestXml.Get(_companyToken, transactionToken, model);

            //Get the response
            var responseXml = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod);
            var response = responseXml.FromXml<ChargeTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public ChargeTokenMobileResponse ChargeTokenMobile(string transactionToken, string phoneNumber, string networkOperator, string networkCountry)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new ChargeTokenMobileResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            var xml = ChargeTokenMobileRequestXml.Get(_companyToken, transactionToken, phoneNumber, networkOperator, networkCountry);

            //Get the response
            var responsexml = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod).AddInstructionsCdata(_bypassCdataMod);

            var response = responsexml.FromXml<ChargeTokenMobileResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public ChargeTokenAuthResponse ChargeTokenAuth(string transactionToken)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new ChargeTokenAuthResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            var xml = ChargeTokenAuthRequestXml.Get(_companyToken, transactionToken);

            //Get the response
            var response = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod).FromXml<ChargeTokenAuthResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public VerifyTokenResponse VerifyToken(string transactionToken = "", string companyRef = "", bool verifyTransaction = true)
        {
            var xml = VerifyTokenRequestXml.Get(_companyToken, transactionToken, companyRef, verifyTransaction);

            //Get the response
            var xmlResult = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod);
            var response = xmlResult.FromXml<VerifyTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }

        /// <inheritdoc />
        public EmailTokenResponse EmailToken(string transactionToken)
        {
            if (string.IsNullOrWhiteSpace(transactionToken))
            {
                return new EmailTokenResponse
                {
                    Result = "-99",
                    ResultExplanation = "a TransactionToken is required"
                };
            }

            var xml = EmailToTokenRequestXml.Get(_companyToken, transactionToken);

            //Get the response
            var response = _url.PostXmlToUrl(xml).AddResultExplanationCdata(_bypassCdataMod).FromXml<EmailTokenResponse>();

            Debug.WriteLine(response);
            return response;
        }
    }
}