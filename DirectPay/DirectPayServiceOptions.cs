namespace DirectPay
{
    public class DirectPayServiceOptions
    {
        public string Url { get; set; }
        public string CompanyToken { get; set; }
        public bool BypassCdataMod { get; set; } //Is this a great option name?
    }
}