using System.Linq;
using System.Xml.Linq;
using DirectPay.Extensions.Helpers;
using DirectPay.Infrastructure.RequestTemplates.Constants;
using DirectPay.Models.Request;


namespace DirectPay.Infrastructure.RequestTemplates
{
    public static class CreateTokenRequestXml
    {
        public static string Get(string companyToken, CreateTokenModel model)
        {
            var xmlDocument = new XDocument(new XDeclaration("1.0", "utf-8", "no"));
            var root = new XElement("API3G",
                new XElement("CompanyToken", companyToken),
                new XElement("Request", RequestTypes.CreateToken)
            );

            var transaction = new XElement("Transaction",
                new XElement("PaymentAmount", model.Transaction.PaymentAmount),
                new XElement("PaymentCurrency", model.Transaction.Currency),
                new XElement("CompanyRef", model.Transaction.CompanyRef),
                //new XElement("CompanyRefUnique", model.Transaction.CompanyRefUnique),
                new XElement("TransactionChargeType", model.Transaction.TransactionChargeType)
            );

            if (!string.IsNullOrWhiteSpace(model.Transaction.RedirectUrl))
            {
                transaction.Add(new XElement("RedirectURL", model.Transaction.RedirectUrl));
            }

            if (!string.IsNullOrWhiteSpace(model.Transaction.BackUrl))
            {
                transaction.Add(new XElement("BackURL", model.Transaction.BackUrl));
            }

            if (!string.IsNullOrWhiteSpace(model.Transaction.DeclinedUrl))
            {
                transaction.Add(new XElement("DeclinedURL", model.Transaction.DeclinedUrl));
            }

            if(!string.IsNullOrWhiteSpace(model.Customer?.CustomerEmail ?? ""))
            {
                transaction.Add( new XElement("customerEmail", model.Customer?.CustomerEmail));
            }
            
            root.Add(transaction);


            var services = new XElement("Services");
            foreach (var service in model.Services)
            {
                services.Add(new XElement("Service",
                    new XElement("ServiceType", service.ServiceType),
                    new XElement("ServiceDescription", service.ServiceDescription),
                    new XElement("ServiceDate", service.ServiceDate.ToString("yyyy/MM/dd HH:MM"))
                ));
            }

            root.Add(services);


            if (model.Allocations.Any())
            {
                var allocations = new XElement("Allocations");
                root.Add(allocations);
            }

            if (model.Travelers.Any())
            {
                var travelers = new XElement("Travelers");
                root.Add(travelers);
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