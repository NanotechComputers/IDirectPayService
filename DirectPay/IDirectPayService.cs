using DirectPay.Models.Request;
using DirectPay.Models.Response;
using DirectPay.Models.Shared;

namespace DirectPay
{
    public interface IDirectPayService
    {
        /// <summary>
        /// CreateToken request will create a transaction in the Direct Pay Online system, it is constructed from 5 levels:
        /// <remarks>1. Transaction level – Mandatory - Contains all the basic transaction information<br/>
        /// 2. Services level – Mandatory - Contains all the information regarding the services sold in the transaction – must contain at-least one service<br/>
        /// 3. Allocations level – Contains all the information regarding the allocation of money received from transaction to be paid to other providers in Direct Pay Online system. If this level is not sent, the system will allocate all the money from this transaction to the provider<br/>
        /// 4. Additional level – Contains an option to block specific payment options in the transaction (for example, on an application which needs fast payment, block off Direct Pay Online bank payment)<br/>
        /// 5. Travelers level – Contains information regarding travelers (passengers / guests) which will a process in Direct Pay Online system to verify that one of the payers name matches the name of one of the travelers</remarks>
        /// </summary>
        /// <param name="requestData"></param>
        /// <returns></returns>
        CreateTokenResponse CreateToken(CreateTokenModel requestData);

        //TODO: Add update token
        // <summary>
        // UpdateToken request will be used to modify existing transaction data
        // </summary>
        // <param name="transactionToken"></param>
        // <param name="requestData"></param>
        // <returns></returns>
        //UpdateTokenResponse UpdateToken(string transactionToken, UpdateTokenModel requestData);

        /// <summary>
        /// The CancelToken request will be used to cancel active transactions.
        /// </summary>
        /// <param name="transactionToken">TransactionToken as received from CreateToken</param>
        /// <returns></returns>
        CancelTokenResponse CancelToken(string transactionToken);

        /// <summary>
        /// RefundToken request will create a refund for the paid transaction
        /// </summary>
        /// <param name="transactionToken">TransactionToken as received from CreateToken</param>
        /// <param name="refundAmount">Requested refund amount</param>
        /// <param name="refundDetails">Requested refund description</param>
        /// <returns></returns>
        RefundTokenResponse RefundToken(string transactionToken, decimal refundAmount, string refundDetails);

        /// <summary>
        /// The ChargeToken request will charge a transaction created by createToken and which was authorized
        /// </summary>
        /// <param name="transactionToken">Transaction token as accepted from CreateToken</param>
        /// <param name="model"></param>
        /// <returns></returns>
        ChargeTokenResponse ChargeToken(string transactionToken, CreditCardModel model);

        /// <summary>
        /// The ChargeToken request will charge a transaction created by createToken and which was authorized
        /// </summary>
        /// <param name="transactionToken">Transaction token as accepted from CreateToken</param>
        /// <param name="phoneNumber">Phone number to charge</param>
        /// <param name="networkOperator">Mobile Network Operator - According to your terminal settings in the system.</param>
        /// <param name="networkCountry">Country name of Mobile Network Operator - According to your terminal settings in the system.</param>
        /// <returns></returns>
        ChargeTokenMobileResponse ChargeTokenMobile(string transactionToken, string phoneNumber, string networkOperator, string networkCountry);

        /// <summary>
        /// The ChargeTokenAuth request will charge a transaction created by createToken and which was authorized
        /// </summary>
        /// <param name="transactionToken">Transaction token as accepted from CreateToken</param>
        /// <returns></returns>
        ChargeTokenAuthResponse ChargeTokenAuth(string transactionToken);

        /// <summary>
        /// The verifyToken request can be initiated at any time, and it is mandatory to verify the token when the customer will return to the application<br/>
        /// Not verifying the token within 30 minutes of transaction completed of payment will generate an alert e-mail to the provider announcing that there was no verification process.
        /// </summary>
        /// <param name="transactionToken">Transaction token as accepted from CreateToken</param>
        /// <param name="companyRef"></param>
        /// <param name="verifyTransaction"></param>
        /// <returns></returns>
        VerifyTokenResponse VerifyToken(string transactionToken = "", string companyRef = "", bool verifyTransaction = true);

        /// <summary>
        /// The EmailToken request will be used send/resend email request to the customer.
        /// </summary>
        /// <param name="transactionToken">Transaction token as accepted from CreateToken</param>
        /// <returns></returns>
        EmailTokenResponse EmailToken(string transactionToken);
    }
}