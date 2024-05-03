## What is PocketWallet.Bkash?
PocketWallet.Bkash is a library that is built with .NET 6, offering integration with Bkash, a popular Bangladeshi e-wallet system. 
The library application is designed to streamline the payment process for businesses and users by providing seamless support for Bkash transactions.

## What are the benefits PocketWallet.Bkash serves with?
- Well designed Bkash API support.
- Simple method call can do it all.
- Network call and token management is internally-handled.
- Unclear Bkash property names are interpreted in .NET way.

## Technologies Used
The library is developed using the following technologies:
- C#, .NET 6
- Bkash API

## Features
- [Payment Creation Process](#payment-creation-process)
- [Payment Execution Process](#payment-execution-process)
- [Check Payment Status](#check-payment-status)
- [Payment Refund](#payment-refund)

## Configuration and Dependency Resolution
### Add this appsettings.json configuration block and provide appropriate values.
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
### Create a class named "BkashOptions" to inject the configuration values as follows:
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
### Add Bkash Dependency:
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
### Add "IBkashPayment" interface in the constructor of any class as follows:
```
private readonly IBkashPayment _bkashPayment;
public WalletsController(IBkashPayment bkashPayment)
{
    _bkashPayment = bkashPayment;
}
```

## Payment Creation Process
1. Payment creation is part of the complete payment flow.
2. In this process, we create a payment instance against a unique invoice number.
3. We will get a bkashURL from the response.
4. We will send the user to that BkashURL immediately after getting the response.
5. User will provide his Bkash credentials (account number, pin, OTP), etc.
6. As soon as the user completes these tasks, he will be redirected to the callback URL(___"https://localhost:xxxx/api/wallets/bkash/callback"___) we provided below.
7. This ___CallBack___ URL is basically our Server or Client's URL ([It can be a get method](#payment-execution-process)), where we want to receive user payment creation status is OK or not, so that we can proceed to [Payment Execution](#payment-execution-process).
### Payment create method request:
```
var command = new CreatePaymentCommand
{
    Amount = "100",
    PayerReference = " ", // Use a phone number here or " ". Do not use null or "" or string.Empty.
    CallbackURL = "https://localhost:xxxx/api/wallets/bkash/callback", // Any callback URL you want to get a callback request from Bkash, to finally execute the payment.
    Currency = "BDT",
    Intent = "sale",
    MerchantInvoiceNumber = "UNIQUE_INVOICE_NUMBER",
};
Result<QueryPaymentResult> result = await _bkashPayment.Create(command);
```
### Payment create method response:
```
// Its the json formatted Result<QueryPaymentResult>
{
"data": {
"paymentId": "TR001179l3aVM1692860485496",
"bkashURL": "https://sandbox.payment.bkash.com/redirect/tokenized/?paymentID=TR001179l3aVM1692860485496&hash=CUCmcSv!OPMt)VB42QxS43Upu.kXOkXoHS0MZnXhZ(!1)3G0PLME(9-Vk4q1RhApNmeFHYfZSVhQbwgNS1692860485552&mode=0011&apiVersion=v1.2.0-beta",
"callbackURL": "https://localhost:xxxx/api/wallets/bkash/callback",
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
Please check the property summary for a better understanding of the responsibility of the property.

## Payment Execution Process
### Call the payment method as follows:
*Note:* A sample callback method is provided below for better clarification and understanding of the significance of ___status___ and ___paymentID___ param received from Bkash callback.
In previous [payment creation process](#payment-create-method-request) we have provided our server URL ___"https://localhost:XXXX/api/wallets/bkash/callback"___ as a callback URL so that Bkash can send a success or error status after the user completes his part.
We could have provided a front-end URL there too. To know more about how the callback mechanism works, please have a look at the [end of this documentation](#bkash-callback-mechanism).
```
[HttpGet("bkash/callback")]
public async Task<IActionResult> PaymentCallback([FromQuery] string paymentID,[FromQuery] string status)
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
### Call the query method as follows:
Note: PaymentId that is shown below can be received from the response of [payment create request](#payment-create-method-response).
```
var query = new PaymentQuery
{
    PaymentId = "UNIQUE_PAYMENT_ID" // Will be received after payment creation/execution.
};
Result<QueryPaymentResult> result = await _bkashPayment.Query(query);
```

## Payment refund
Payment can be refunded providing amount, reason such as ___"faulty product", "product not received"___ etc, Unique Payment ID, Product SKU and Transaction Id.
Call the query method as follows:
```
    var command = new RefundPaymentCommand
    {
        Amount = "100", // Amount to refund, Maximum two decimals after amount. Ex. 25.20
        PaymentId = "UNIQUE_PAYMENT_ID", // Will be received after payment creation/execution.
        Reason = "product not received", // example: "faulty product", "product not received"
        SKU = "PRODUCT_INFORMATION_OF_MERCHANT", // Tag of product information from merchant's website.
        TransactionId = "UNIQUE_TRANSACTION_ID" // Transaction Id will can be received after payment execution completed. That transaction id will be used here.
    };
    Result<RefundPaymentResult> result = await _bkashPayment.Refund(command);
```

## Bkash Callback Mechanism
### What is Bkash callback?
Bkash callback is a mechanism by which Bkash sends a status or result of user interaction after payment creation.

### How we can interpret Bkash Callback?
1. User clicks on Bkash Button for payment for his current order/invoice [Internally, we will create a [payment](#payment-creation-process)].
2. User should automatically be redirected to Bkash page. [Internally, we redirected him to "bkashURL" mentioned [here](#payment-create-method-response)].
3. User provides his credentials and after that, Bkash redirects that user to the Callback Url of our server/client app, [we have provided.](#payment-create-method-request) 
    - If credential is OK, in the [callback URL](#payment-execution-process) we get paymentID and status as "success".
    - If fails, similarly, we get the "error" status.
4. Based on the status we received, we will be going to [execute the payment](#payment-execution-process) to fully confirm the payment transaction.