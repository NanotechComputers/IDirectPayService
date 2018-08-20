using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using DirectPay.Models.Shared;

namespace DirectPay.Models.Response
{
    [DataContract(Name = "API3G", Namespace = "")]
    public class VerifyTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }

        [DataMember]
        public string CustomerName { get; set; }

        //[DataMember]
        //public int CustomerCredit { get; set; } = 0;

        //[DataMember]
        //public int TransactionApproval { get; set; } = 0;

        [XmlIgnore]
        // ReSharper disable once MemberCanBePrivate.Global
        public Currencies TransactionCurrency { get; set; }

        [DataMember(Name = "TransactionCurrency")]
        internal string TransactionCurrencyString
        {
            set
            {
                var currencies = Currencies.All();
                var currency = currencies.First(x => x.Name == value);
                TransactionCurrency = currency;
            }
            get => TransactionCurrency.Name;
        }

        [DataMember]
        public string TransactionAmount { get; set; }

        [DataMember]
        public string FraudAlert { get; set; }

        [DataMember]
        public string FraudExplnation { get; set; }

        [DataMember]
        public decimal TransactionNetAmount { get; set; }

        [DataMember]
        public string TransactionSettlementDate { get; set; }

        [DataMember]
        public string TransactionRollingReserveAmount { get; set; }

        [DataMember]
        public string TransactionRollingReserveDate { get; set; }

        [DataMember]
        public string CustomerPhone { get; set; }

        [DataMember]
        public string CustomerCountry { get; set; }

        [DataMember]
        public string CustomerAddress { get; set; }

        [DataMember]
        public string CustomerCity { get; set; }

        [DataMember]
        public string CustomerZip { get; set; }

        [DataMember]
        public string MobilePaymentRequest { get; set; }

        [DataMember]
        public string AccRef { get; set; }
    }
}