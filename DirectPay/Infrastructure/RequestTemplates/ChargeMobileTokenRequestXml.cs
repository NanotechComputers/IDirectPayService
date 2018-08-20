using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;


namespace DirectPay.Infrastructure.RequestTemplates
{
    public static class ChargeTokenMobileRequestXml
    {
        public static string Get(string companyToken, string transactionToken, string phoneNumber, string mobileNumberOperator, string country)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"),
                new XElement("API3G",
                    new XElement("CompanyToken", companyToken),
                    new XElement("Request", RequestTypes.ChargeTokenMobile),
                    new XElement("TransactionToken", transactionToken),
                    new XElement("PhoneNumber", phoneNumber),
                    new XElement("MNO", mobileNumberOperator),
                    new XElement("MNOcountry", country)
                ));

            using (var sw = new Utf8StringWriter())
            {
                xmlDocument.Save(sw, SaveOptions.None);
                return sw.ToString();
            }
        }
    }
}