using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;


namespace DirectPay.Infrastructure.RequestTemplates
{
    internal static class EmailToTokenRequestXml
    {
        internal static string Get(string companyToken, string transactionToken)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"),
                new XElement("API3G",
                    new XElement("CompanyToken", companyToken),
                    new XElement("Request", RequestTypes.EmailToToken),
                    new XElement("TransactionToken", transactionToken)
                ));

            using (var sw = new Utf8StringWriter())
            {
                xmlDocument.Save(sw, SaveOptions.None);
                return sw.ToString();
            }
        }
    }
}