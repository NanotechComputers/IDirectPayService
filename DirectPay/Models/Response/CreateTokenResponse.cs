using System;
using System.Runtime.Serialization;

namespace DirectPay.Models.Response
{
    /*[DataContract(Name = "API3G", Namespace = "")]
    [KnownType(typeof(CreateTokenResponse))]
    [KnownType(typeof(UpdateTokenResponse))]
    public class BaseResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }*/

    [DataContract(Name = "API3G", Namespace = "")]
    public class CreateTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }

        [DataMember]
        public Guid TransToken { get; set; }

        [DataMember]
        public string TransRef { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class UpdateTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class RefundTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class CancelTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class ChargeTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class ChargeTokenMobileResponse
    {
        [DataMember(Name = "StatusCode")] //Why The F will everything be result and this one is statuscode? 
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }

        [DataMember(Name = "instructions")]
        public string Instructions { get; set; }

        [DataMember]
        public bool RedirectOption { get; set; }
    }

    [DataContract(Name = "API3G", Namespace = "")]
    public class EmailTokenResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }
    
    [DataContract(Name = "API3G", Namespace = "")]
    public class ChargeTokenAuthResponse
    {
        [DataMember]
        public string Result { get; set; }

        [DataMember]
        public string ResultExplanation { get; set; }
    }
}