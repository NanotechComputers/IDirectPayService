using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;
using DirectPay.Models.Shared;


namespace DirectPay.Infrastructure.RequestTemplates
{
    internal static class ChargeTokenRequestXml
    {
        internal static string Get(string companyToken, string transactionToken, CreditCardModel model)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"));

            var root = new XElement("API3G",
                new XElement("CompanyToken", companyToken),
                new XElement("Request", RequestTypes.ChargeToken),
                new XElement("TransactionToken", transactionToken)
            );

            if (model != null)
            {
                root.Add(new XElement("CreditCardNumber", model.CreditCardNumber));
                root.Add(new XElement("CreditCardExpiry", model.CreditCardExpiry));
                root.Add(new XElement("CreditCardCVV", model.CreditCardCvv));
                root.Add(new XElement("CardHolderName", model.CardHolderName));
            }

            xmlDocument.Add(root);
            using (var sw = new Utf8StringWriter())
            {
                xmlDocument.Save(sw, SaveOptions.None);
                return sw.ToString();
            }
        }
    }
}