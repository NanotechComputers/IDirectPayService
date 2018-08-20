namespace DirectPay.Infrastructure.RequestTemplates.Constants
{
    internal class RequestTypes
    {
        internal const string CreateToken = "createToken";
        internal const string UpdateToken = "updateToken";
        internal const string CancelToken = "cancelToken";
        internal const string VerifyToken = "verifyToken";
        internal const string ChargeToken = "chargeTokenCreditCard";
        internal const string ChargeTokenMobile = "ChargeTokenMobile";
        internal const string EmailToToken = "emailToToken";
        internal const string RefundToken = "refundToken";
        internal const string ChargeTokenAuth = "chargeTokenAuth";
    }
}