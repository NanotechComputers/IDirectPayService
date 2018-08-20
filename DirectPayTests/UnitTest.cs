using System;
using System.Collections.Generic;
using DirectPay;
using DirectPay.Extensions;
using DirectPay.Models.Request;
using DirectPay.Models.Shared;
using Xunit;
using Xunit.Abstractions;

namespace DirectPayTests
{
    public class UnitTest
    {
        private readonly ITestOutputHelper _debug;

        private const string _TransactionToken = "Transaction Token from CreateTokenTest for other tests";
        private const string _baseUrl = "https://secure1.sandbox.directpay.online/API/v6/";
        private const string _companyToken = "Company Token Here";
        static Random rnd = new Random();

        public UnitTest(ITestOutputHelper debug)
        {
            _debug = debug;
        }

        [Theory]
        [InlineData("Name", "Surname", "EmailAddress")]
        public void CreateTokenTest(string name, string surname, string email)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var data = new CreateTokenModel
            {
                Customer = new CustomerModel
                {
                    CustomerFirstName = name,
                    CustomerLastName = surname,
                    CustomerPhone = "",
                    CustomerEmail = email
                },
                Transaction = new TransactionModel
                {
                    CompanyRef = Guid.NewGuid().ToString("N"),
                    PaymentAmount = decimal.Parse($"{rnd.Next(11, 199)}.{rnd.Next(0, 99)}"),//TODO: Set Transaction Payment Amount here
                    Currency = Currencies.USD,
                    TransactionChargeType = "1"
                },
                Services = new List<ServiceModel>
                {
                    new ServiceModel
                    {
                        ServiceDescription = "Service Here", //TODO: Get your service from DPO
                        ServiceType = 0,//TODO: Get your service type from DPO
                        ServiceDate = DateTime.Now
                    }
                }
            };
            var response = service.CreateToken(data);
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0} - Amount: {1}", response.TransToken, data.Transaction.PaymentAmount);
            Assert.Equal(true, response.Result == "000");
        }

        [Theory]
        [InlineData(_TransactionToken, "000")]
        public void ChargeTokenTest(string transactionToken, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var data = new CreditCardModel
            {
                CardHolderName = "Card Holder",
                CreditCardCvv = "123",
                CreditCardExpiry = "0320",
                CreditCardNumber = "5424000000000015"
            };

            var response = service.ChargeToken(transactionToken.ToUpper(), data);
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);
            Assert.Equal(true, response.Result == "000");
        }

        [Theory]
        public void CancelTokenTest(string transactionToken, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var response = service.CancelToken(transactionToken.ToUpper());
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);
            Assert.Equal(true, response.Result == expectedResponse);
        }

        [Theory]
        public void RefundTokenTest(string transactionToken, decimal amount, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var response = service.RefundToken(transactionToken.ToUpper(), amount, "Testing refunds");
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);
            Assert.Equal(true, response.Result == expectedResponse);
        }

        [Theory]
        [InlineData(_TransactionToken, "000")]
        public void VerifyTokenTest(string transactionToken, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var response = service.VerifyToken(transactionToken.ToUpper(), "", false);
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);

            Assert.Equal(true, response.Result == expectedResponse);
        }

        [Theory]
        [InlineData(_TransactionToken, "Mobile Number Here", "mpesa", "kenya", "000")]
        public void ChargeTokenMobileTest(string transactionToken,string phoneNumber, string networkOperator, string networkCountry, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var response = service.ChargeTokenMobile(transactionToken.ToUpper(), phoneNumber, networkOperator, networkCountry);
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);

            Assert.Equal(true, response.Result == expectedResponse);
        }

        [Theory]
        [InlineData(_TransactionToken, "000")]
        public void ChargeTokenAuthTest(string transactionToken, string expectedResponse)
        {
            var service = new DirectPayService(_baseUrl, _companyToken);
            var response = service.ChargeTokenAuth(transactionToken.ToUpper());
            _debug.WriteLine("Result: {0}", response.Result);
            _debug.WriteLine("Result Explanation: {0}", response.ResultExplanation);
            _debug.WriteLine("Transaction Token: {0}", transactionToken);

            Assert.Equal(true, response.Result == expectedResponse);
        }
    }
}