using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;


namespace DirectPay.Infrastructure.RequestTemplates
{
    internal static class RefundTokenRequestXml
    {
        internal static string Get(string companyToken, string transactionToken, decimal refundAmount, string refundDetails)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"),
                new XElement("API3G",
                    new XElement("CompanyToken", companyToken),
                    new XElement("Request", RequestTypes.RefundToken),
                    new XElement("TransactionToken", transactionToken),
                    new XElement("refundAmount", refundAmount),
                    new XElement("refundDetails", refundDetails)
                ));

            using (var sw = new Utf8StringWriter())
            {
                xmlDocument.Save(sw, SaveOptions.None);
                return sw.ToString();
            }
        }
    }
}