using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;


namespace DirectPay.Infrastructure.RequestTemplates
{
    public static class VerifyTokenRequestXml
    {
        public static string Get(string companyToken, string transactionToken = "", string companyRef = "", bool verifyTransaction = true)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"));


            var root = new XElement("API3G",
                new XElement("CompanyToken", companyToken),
                new XElement("Request", RequestTypes.VerifyToken));


            if (!string.IsNullOrWhiteSpace(transactionToken))
            {
                root.Add(new XElement("TransactionToken", transactionToken));
            }

            if (!string.IsNullOrWhiteSpace(companyRef))
            {
                root.Add(new XElement("CompanyRef", companyRef));
            }

            if (!verifyTransaction)
            {
                root.Add(new XElement("VeirfyTransaction", "0")); //TODO: ASK DPO if they really mean for us to send VeirfyTransaction
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