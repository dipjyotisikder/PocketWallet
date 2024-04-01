## What is PocketWallet.Bkash?
PocketWallet.Bkash is a library that is built with .NET 6, offering integration with Bkash, a popular Bangladeshi e-wallet system. The application is designed to streamline the payment process for businesses and users by providing seamless support for Bkash transactions.


## Technologies Used
The library is developed using the following technologies:
- .NET 6
- C# language
- Bkash API


## Features
- [Payment Creation Process](#payment-creation-process)
- [Payment Execution Process](#payment-execution-process)
- [Check Payment Status](#check-payment-status)
- [Payment Refund](#payment-refund)


## Configuration and Dependency Resolution
###### Add this appsettings.json configuration block and provide appropriate values.
```
"BkashOptions": {
    "MerchantUserName": "",
    "MerchantPassword": "",
    "AppKey": "",
    "AppSecret": "",
    "ProductionMode": false
}
```
 Please collect ***MerchantUserName***, ***MerchantPassword***, ***AppKey***, ***AppSecret*** from Bkash support.
######  Create a class named "BkashOptions" to inject the configuration values as follows:
```
public class BkashOptions
{
    public string MerchantUserName { get; set; }
    public string MerchantPassword { get; set; }
    public string AppKey { get; set; }
    public string AppSecret { get; set; }
    public bool ProductionMode { get; set; }
    public PaymentModes PaymentMode { get; set; }
}
```
###### Add Bkash Dependency:
```
var bkashOptions = new BkashOptions();
builder.Configuration.GetSection("BkashOptions").Bind(bkashOptions);
builder.Services.AddBkash(option =>
{
    option.MerchantUserName = bkashOptions.MerchantUserName;
    option.MerchantPassword = bkashOptions.MerchantPassword;
    option.AppKey = bkashOptions.AppKey;
    option.AppSecret = bkashOptions.AppSecret;
    option.ProductionMode = bkashOptions.ProductionMode;
});
```
###### Add "IBkashPayment" interface in the constructor of any class as follows:
```
private readonly IBkashPayment _bkashPayment;
public WalletsController(IBkashPayment bkashPayment)
{
    _bkashPayment = bkashPayment;
}
```


## Payment Creation Process
1. Payment creation is a part of complete payment flow.
2. In this process, we create a payment instance against an unique invoice number.
3. We will get a bkashURL from response
4. We will send user to that BkashURL immediately after getting the response. So, user will then interact with BKash where user will provide his Bkash account number, Pin Otp etc. 
5. As soon as user completes these tasks, he will be redirected to the callback URL we provided (example is mentioned below). #payment-point
###### Payment create method request:
```
var command = new CreatePaymentCommand
{
    Amount = "100",
    PayerReference = " ", // Use a phone number here or " ". Do not use null or "" or string.Empty.
    CallbackURL = "https://localhost:5000/api/wallets/bkash/callback", // Any callback URL you want to get a callback request from Bkash, to finally execute the payment.
    Currency = "BDT",
    Intent = "sale",
    MerchantInvoiceNumber = "UNIQUE_INVOICE_NUMBER",
};
Result<QueryPaymentResult> result = await _bkashPayment.Create(command);
```
###### Payment create method response:
```
// Its the json formatted Result<QueryPaymentResult>
{
	"data": {
		"paymentId": "TR001179l3aVM1692860485496",
		"bkashURL": "https://sandbox.payment.bkash.com/redirect/tokenized/?paymentID=TR001179l3aVM1692860485496&hash=CUCmcSv!OPMt)VB42QxS43Upu.kXOkXoHS0MZnXhZ(!1)3G0PLME(9-Vk4q1RhApNmeFHYfZSVhQbwgNS1692860485552&mode=0011&apiVersion=v1.2.0-beta",
		"callbackURL": "https://localhost:5000/api/wallets/bkash/callback",
		"amount": "10",
		"intent": "sale",
		"currency": "BDT",
		"paymentCreateTime": "2023-08-24T13:01:25:552 GMT+0600",
		"transactionStatus": "Initiated",
		"merchantInvoiceNumber": "XXXX-PAY-000"
	},
	"isSucceeded": true,
	"problem": null
}
```
Please check property-summary for better understanding about the responsibility of the property.


## Payment Execution Process
###### Call the payment method as follows:
*Note:* A sample callback method design is provided for better clarification and understanding of the significance of status and paymentID param received from Bkash callback. In previous payment creation process we have provided "https://localhost:5000/api/wallets/bkash/callback" as a callback URL. We could have provided a frontend URL there too. To know more about how callback mechanism works please have a look at the [end of this documentation](#bkash-callback-mechanism).
```
public async Task<IActionResult> PaymentCallback(
    [FromQuery] string paymentID,
    [FromQuery] string status)
{
    if (status == "success")
    {
        // Check if any other payment existed with this same paymentID in database/persistent storage.
        // If not
        var result = await _bkashPayment.Execute(new ExecutePaymentCommand
        {
            PaymentId = paymentID
        });
        return Ok(result);
    }
    return Ok();
}
```


## Check Payment Status
###### Call the query method as follows:
```
var query = new PaymentQuery
{
    PaymentId = "UNIQUE_PAYMENT_ID"  // Will be received after payment creation/execution.
};
Result<QueryPaymentResult> result = await _bkashPayment.Query(query);
```


## Payment refund
###### Call the query method as follows:
```
       var command = new RefundPaymentCommand
       {
           Amount = "100",
           PaymentId = "UNIQUE_PAYMENT_ID", // Will be received after payment creation/execution.
           Reason = "product not received", // example: "faulty product", "product not received"
           SKU = "PRODUCT_INFORMATION_MERCHANT",
           TransactionId = "UNIQUE_TRANSACTION_ID" // Transaction Id will can be received after payment execution completed. That transaction id will be used here.
       };
       Result<RefundPaymentResult> result = await _bkashPayment.Refund(command);
```


## Bkash Callback Mechanism
#### What is Bkash callback?
Bkash callback is a mechanism of Bkash with what they sends a status/result of user interaction between user and Bkash, to merchant after payment creation.
#### Complete Flow:
The resolution will be, What user clicks/interacts and what we do in background.
1. User clicks on Bkash Button for payment for his current order/invoice, we do [create the payment](#payment-creation-process).
2. Immediately after that user sees, he is automatically being redirected to Bkash page, but we actually redirected him to "bkashURL" of [create payment response](#payment-create-method-response).
3. Ok. So, now we don't have any control over the user. User interacts with bkash page now. After he provide his all the credentials, Bkash redirects that user to our Callback Url we have provided in [create payment request](#payment-create-method-request). This Url is basically our frontend URL or Backend Url, where we would like to get notified by Bkash after user is successfully provided his credentials. We will also be notified if the user fails.
4. When we will get that call in our callback URL, we will take the decision if we are going to [execute the payment](#payment-execution-process) to fully confirm the payment transaction or display an error to user.