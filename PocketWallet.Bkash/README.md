## Introduction
### What is PocketWallet.Bkash?
PocketWallet.Bkash is a library that is built with .NET 6, offering integration with Bkash, a popular Bangladeshi e-wallet system. 
The library application is designed to streamline the payment process for businesses and users by providing seamless support for Bkash transactions.

### What are the benefits of using PocketWallet.Bkash?
- Well designed Bkash API support.
- Simple method call can do it all.
- Network call and token management is internally-handled.
- Unclear Bkash property names are interpreted in .NET way.
- It reduces the Bkash integration time. 
- It is completely open sourcea and transparent. Anyone can contribute to the codebase.

### Technologies Used
The library is developed using the following technologies:
- C#, .NET 6
- Bkash API

### Features
- [Payment Creation Process](#payment-creation-process)
- [Payment Execution Process](#payment-execution-process)
- [Check Payment Status](#check-payment-status)
- [Payment Refund](#payment-refund)

## Configuration and Dependency
1. Collect ***MerchantUserName***, ***MerchantPassword***, ***AppKey***, ***AppSecret*** from Bkash support.
2. Add this configuration block into ___appsettings.json___ file and provide appropriate values. You have to collect all the required values and informations from Bkash officials. Such as, for production, collect and set production information and set "ProductionMode" value as true. And for sandbox/testing mode, similarly set sandbox/testing information and set "ProductionMode" to false as follows:
    ````
    "BkashOptions": {
        "MerchantUserName": "ExampleUserName",
        "MerchantPassword": "ExamplePassword",
        "AppKey": "ExampleAppKey",
        "AppSecret": "ExampleAppSecret",
        "ProductionMode": false
    }
    ````
3. To retrive BkashOptions from ___appsettings.json___ file you can create a class as follows:
    ````
    public class BkashOptions
    {
        public string MerchantUserName { get; set; }
        public string MerchantPassword { get; set; }
        public string AppKey { get; set; }
        public string AppSecret { get; set; }
        public bool ProductionMode { get; set; }
    }
    ````
4. Go to your ___program.cs___ file and add dependency by calling __AddBkash__ extension method under the namespace __PocketWallet.Bkash.DependencyInjection__ as follows:
    - Firstly, Bind AppSettings configuration information from "BkashOptions". This approach is commonly known as "Options Pattern" in .NET Core.
        ````
        var bkashOptions = new BkashOptions();
        builder.Configuration.GetSection("BkashOptions").Bind(bkashOptions);
        ````
    - Then, simply call AddBkash to add bkash functionality into your application IOC.
        ````
        builder.Services.AddBkash(option =>
        {
            option.MerchantUserName = bkashOptions.MerchantUserName;
            option.MerchantPassword = bkashOptions.MerchantPassword;
            option.AppKey = bkashOptions.AppKey;
            option.AppSecret = bkashOptions.AppSecret;
            option.ProductionMode = bkashOptions.ProductionMode;
        });
        ````
5. Lastly, Inject "IBkashPayment" object into the constructor of any component (i.e Controller, service etc.) as follows:
    ````
    private readonly IBkashPayment _bkashPayment;
    public WalletsController(IBkashPayment bkashPayment)
    {
        _bkashPayment = bkashPayment;
    }
    ````

## Actions
### Payment Creation Process: <a name="payment-creation-process"></a>
Payment Creation is the __First Step__ to the complete payment flow. 
In this step, you as merchant, need to provide some information such as,
- Total __Amount__ of money you want to charge from you customer need to be provided in the field __Amount__.
- Customers __Phone Number__ need to be provided in the field __PayerReference__, if you want to pre-populate this number inside Bkash page. This field is optional.
- You need to provide a server address to handle callback from Bkash in the field __CallbackURL__.
- Unique __Invoice Number__ need to be provided in the field __MerchantInvoiceNumber__, for which you are going to charge your customer.
- __Intent__ and __Currency__ field are provided below based on Bkash guideline as per this [Bkash documentation](https://developer.bka.sh/docs/create-payment-2#section-request-parameters).

*An example Request & Response is provided below*:

- Payment **Request**:
    ````
    var command = new CreatePaymentCommand
    {
        Amount = 100.50,
        PayerReference = "EXAMPLE_PHONE_NUMBER",
        CallbackURL = "https://EXAMPLE_SERVER.com/api/wallets/bkash/callback",
        MerchantInvoiceNumber = "EXAMPLE_INVOICE_NUMBER",
        Intent = "sale",
        Currency = "BDT"
    };
    Result<QueryPaymentResult> result = await _bkashPayment.Create(command);
    ````
- Payment **Response**:
````
// Its the json formatted Result<QueryPaymentResult>
{
    "data": {
        "paymentId": "TR001179l3aVM1692860485496",
        "bkashURL": "https://sandbox.payment.bkash.com/redirect/tokenized/?paymentID=TR001179l3aVM1692860485496&hash=CUCmcSv!OPMt)VB42QxS43Upu.kXOkXoHS0MZnXhZ(!1)3G0PLME(9-Vk4q1RhApNmeFHYfZSVhQbwgNS1692860485552&mode=0011&apiVersion=v1.2.0-beta",
        "callbackURL": "https://EXAMPLE_SERVER.com/api/wallets/bkash/callback",
        "amount": 100.50,
        "intent": "sale",
        "currency": "BDT",
        "paymentCreateTime": "0000-00-00T00:00:00:000 GMT+0000",
        "transactionStatus": "Initiated",
        "merchantInvoiceNumber": "XXXX-PAY-000"
    },
    "isSucceeded": true,
    "problem": null
}
````

From the response, you need to store **paymentId**, **Invoice** etc. so that you can track this payment later.
As you see, there's a property named **callbackURL**, is the URL where you have to redirect user immediately after you get this response, so that user can provide there credentions to Bkash and pay the amount successfully.
Based on the Customers input, 

### Payment Execution Process <a name="payment-execution-process"></a>
Payment execution process will happen when callback will be successful, that means if customer's credentials are valid after redirection to BkashURL.

#### Call the payment method as follows:
*Note:* A sample callback method is provided below for better clarification and understanding of the significance of ___status___ and ___paymentID___ param received from Bkash callback.
In previous [payment creation process](#payment-create-method-request) we have provided our server URL ___"https://EXAMPLE_SERVER.com/api/wallets/bkash/callback"___ as a callback URL so that Bkash can send a success or error status after the user completes his part.
We could have provided a front-end URL there too. To know more about how the callback mechanism works, please have a look at the [end of this documentation](#bkash-callback-mechanism).
````
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
````

### Check Payment Status <a name="check-payment-status"></a>
#### Call the query method as follows:
Note: PaymentId that is shown below can be received from the response of [payment create request](#payment-create-method-response).
````
var query = new PaymentQuery
{
    PaymentId = "UNIQUE_PAYMENT_ID" // Will be received after payment creation/execution.
};
Result<QueryPaymentResult> result = await _bkashPayment.Query(query);
````

### Payment refund <a name="payment-refund"></a>
Payment can be refunded providing amount, reason such as ___"faulty product", "product not received"___ etc, Unique Payment ID, Product SKU and Transaction Id.
Call the query method as follows:
````
var command = new RefundPaymentCommand
{
    Amount = 100, // Amount to refund, Maximum two decimals after amount. Ex. 25.20
    PaymentId = "UNIQUE_PAYMENT_ID", // Will be received after payment creation/execution.
    Reason = "product not received", // example: "faulty product", "product not received"
    SKU = "PRODUCT_INFORMATION_OF_MERCHANT", // Tag of product information from merchant's website.
    TransactionId = "UNIQUE_TRANSACTION_ID" // Transaction Id will can be received after payment execution completed. That transaction id will be used here.
};
Result<RefundPaymentResult> result = await _bkashPayment.Refund(command);
````

### Bkash Callback Mechanism <a name="bkash-callback-mechanism"></a>
#### What is Bkash callback?
Bkash callback is a mechanism by which Bkash sends a status or result of user interaction after payment creation.

#### How we can interpret Bkash Callback?
1. User clicks on Bkash Button for payment for his current order/invoice [Internally, we will create a [payment](#payment-creation-process)].
2. User should automatically be redirected to Bkash page. [Internally, we redirected him to "bkashURL" mentioned [here](#payment-create-method-response)].
3. User provides his credentials and after that, Bkash redirects that user to the Callback Url of our server/client app, [we have provided.](#payment-create-method-request) 
    - If credential is OK, in the [callback URL](#payment-execution-process) we get paymentID and status as "success".
    - If fails, similarly, we get the "error" status.
4. Based on the status we received, we will be going to [execute the payment](#payment-execution-process) to fully confirm the payment transaction.