# PocketWallet.Bkash

A modern .NET Standard 2.0 library for seamless Bkash payment integration in your applications.

## Introduction

### What is PocketWallet.Bkash?

PocketWallet.Bkash is a comprehensive .NET Standard 2.0 library that simplifies integration with Bkash, Bangladesh's leading mobile financial service. This library streamlines payment processing for businesses and developers by providing an easy-to-use interface for Bkash transactions.

### Why Choose PocketWallet.Bkash?

- ✅ **Well-Designed API Wrapper** - Clean, intuitive API that follows .NET best practices
- ✅ **Simple Integration** - Get started with just a few lines of code
- ✅ **Automatic Token Management** - Network calls and authentication tokens are handled internally
- ✅ **Developer-Friendly Naming** - Bkash property names are translated to .NET conventions
- ✅ **Faster Development** - Significantly reduces Bkash integration time
- ✅ **Open Source** - Fully transparent and open for community contributions
- ✅ **Production Ready** - Supports both sandbox and production environments
- ✅ **Wide Compatibility** - Works with .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+, Xamarin, and more

### Compatibility

This library targets .NET Standard 2.0, making it compatible with:
- ✅ .NET Framework 4.6.1 and above
- ✅ .NET Core 2.0 and above
- ✅ .NET 5, 6, 7, 8, and future versions
- ✅ Xamarin.iOS, Xamarin.Android
- ✅ Universal Windows Platform (UWP)

For a complete compatibility matrix, see [.NET Standard compatibility](https://docs.microsoft.com/en-us/dotnet/standard/net-standard).

### Technologies Used

- C# / .NET Standard 2.0
- Bkash Payment Gateway API
- System.Text.Json for JSON serialization
- Dependency Injection for clean architecture

### Supported Features

- ✅ [Payment Creation](#payment-creation-process)
- ✅ [Payment Execution](#payment-execution-process)
- ✅ [Payment Status Inquiry](#check-payment-status)
- ✅ [Payment Refund](#payment-refund)

---

## Installation

Install the package via NuGet Package Manager:

### Package Manager Console
```powershell
Install-Package Dipjyoti.PocketWallet.Bkash
```

### .NET CLI
```bash
dotnet add package Dipjyoti.PocketWallet.Bkash
```

### Visual Studio
Search for `Dipjyoti.PocketWallet.Bkash` in the NuGet Package Manager UI.

---

## Configuration and Setup

### Step 1: Obtain Bkash Credentials

Contact Bkash support to obtain the following credentials:
- **MerchantUserName** - Your merchant username
- **MerchantPassword** - Your merchant password
- **AppKey** - Application key provided by Bkash
- **AppSecret** - Application secret provided by Bkash

> **Note:** You'll receive separate credentials for sandbox (testing) and production environments.

### Step 2: Configure appsettings.json

Add the following configuration block to your `appsettings.json` file:

```json
{
  "BkashOptions": {
    "MerchantUserName": "YourMerchantUserName",
    "MerchantPassword": "YourMerchantPassword",
    "AppKey": "YourAppKey",
    "AppSecret": "YourAppSecret",
    "ProductionMode": false
  }
}
```

> **Important:** 
> - Set `ProductionMode` to `false` for sandbox/testing environment
> - Set `ProductionMode` to `true` for production environment
> - Never commit production credentials to source control!

### Step 3: Create Configuration Class

Create a class to map the Bkash configuration:

```csharp
public class BkashOptions
{
    public string MerchantUserName { get; set; } = string.Empty;
    public string MerchantPassword { get; set; } = string.Empty;
    public string AppKey { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    public bool ProductionMode { get; set; }
}
```

### Step 4: Register Services

#### For .NET 6+ (Minimal APIs / Program.cs)

```csharp
using PocketWallet.Bkash.DependencyInjection;

// Bind configuration using the Options Pattern
var bkashOptions = new BkashOptions();
builder.Configuration.GetSection("BkashOptions").Bind(bkashOptions);

// Register Bkash services
builder.Services.AddBkash(option =>
{
    option.MerchantUserName = bkashOptions.MerchantUserName;
    option.MerchantPassword = bkashOptions.MerchantPassword;
    option.AppKey = bkashOptions.AppKey;
    option.AppSecret = bkashOptions.AppSecret;
    option.ProductionMode = bkashOptions.ProductionMode;
});
```

#### For .NET Framework / .NET Core 3.1 and earlier (Startup.cs)

```csharp
using PocketWallet.Bkash.DependencyInjection;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Bind configuration using the Options Pattern
        var bkashOptions = new BkashOptions();
     Configuration.GetSection("BkashOptions").Bind(bkashOptions);

        // Register Bkash services
        services.AddBkash(option =>
        {
            option.MerchantUserName = bkashOptions.MerchantUserName;
         option.MerchantPassword = bkashOptions.MerchantPassword;
        option.AppKey = bkashOptions.AppKey;
            option.AppSecret = bkashOptions.AppSecret;
            option.ProductionMode = bkashOptions.ProductionMode;
        });

        // ... other service registrations
    }
}
```

### Step 5: Inject IBkashPayment Interface

Inject the `IBkashPayment` interface into your controller, service, or component:

```csharp
using PocketWallet.Bkash;

public class PaymentController : ControllerBase
{
  private readonly IBkashPayment _bkashPayment;

    public PaymentController(IBkashPayment bkashPayment)
    {
        _bkashPayment = bkashPayment;
    }
    
    // Your payment methods here...
}
```

---

## Usage Guide

### Payment Creation Process

Payment creation is the **first step** in the complete payment flow. This initiates a payment request with Bkash.

#### Required Information

- **Amount** - Total amount to charge the customer (supports up to 2 decimal places)
- **PayerReference** - Customer's phone number (optional, pre-populates Bkash page)
- **CallbackURL** - Your server endpoint to handle Bkash callback
- **MerchantInvoiceNumber** - Unique invoice/order number from your system
- **Intent** - Payment intent (use "sale" for standard payments)
- **Currency** - Currency code (use "BDT" for Bangladesh Taka)

For more details, refer to the [official Bkash documentation](https://developer.bka.sh/docs/create-payment-2#section-request-parameters).

#### Example Request

```csharp
var command = new CreatePaymentCommand
{
    Amount = 100.50f,
    PayerReference = "01712345678", // Optional: Customer's phone number
    CallbackURL = "https://yourserver.com/api/payment/bkash/callback",
    MerchantInvoiceNumber = "INV-2024-001",
    Intent = "sale",
    Currency = "BDT"
};

Result<CreatePaymentResult> result = await _bkashPayment.Create(command);

if (result.IsSucceeded)
{
    // Store paymentId for tracking
    var paymentId = result.Data.PaymentId;
    
    // Redirect user to Bkash payment page
    var bkashUrl = result.Data.BkashURL;
    return Redirect(bkashUrl);
}
else
{
    // Handle error
    var errorMessage = result.Problem?.Message;
}
```

#### Example Response

```json
{
  "data": {
    "paymentId": "TR001179l3aVM1692860485496",
    "bkashURL": "https://sandbox.payment.bkash.com/redirect/tokenized/?paymentID=TR001179l3aVM1692860485496...",
    "callbackURL": "https://yourserver.com/api/payment/bkash/callback",
    "amount": 100.50,
    "intent": "sale",
    "currency": "BDT",
    "paymentCreateTime": "2024-01-15T10:30:00.000 GMT+0600",
    "transactionStatus": "Initiated",
    "merchantInvoiceNumber": "INV-2024-001"
  },
  "isSucceeded": true,
  "problem": null
}
```

> **Important:** Store the `paymentId` in your database to track this payment later. Redirect the user to `bkashURL` immediately after receiving this response.

---

### Payment Execution Process

Payment execution occurs after the customer successfully authenticates and approves the payment on the Bkash page.

#### Implementing the Callback Endpoint

After payment creation, Bkash redirects the user to your callback URL with payment status. Here's how to handle it:

```csharp
[HttpGet("bkash/callback")]
public async Task<IActionResult> PaymentCallback(
    [FromQuery] string paymentID,
    [FromQuery] string status)
{
    if (status == "success")
    {
        // Verify this paymentID doesn't already exist in your database
        // to prevent duplicate processing

        var result = await _bkashPayment.Execute(new ExecutePaymentCommand
        {
            PaymentId = paymentID
        });

        if (result.IsSucceeded)
        {
            // Payment successful - Update your database
            // Grant access to product/service
            // Send confirmation email, etc.

            var transactionId = result.Data.TransactionId;
            var amount = result.Data.Amount;

            return Ok(new { message = "Payment successful", transactionId });
        }
        else
        {
            // Payment execution failed
            return BadRequest(new { message = "Payment failed", error = result.Problem?.Message });
        }
    }
    else if (status == "cancel")
    {
        // User cancelled the payment
        return Ok(new { message = "Payment cancelled by user" });
    }
    else if (status == "failure")
    {
        // Payment failed
        return BadRequest(new { message = "Payment failed" });
    }

    return BadRequest(new { message = "Invalid status" });
}
```

> **Security Tip:** Always verify that the paymentID hasn't been processed before to prevent duplicate payments or replay attacks.

---

### Check Payment Status

Query the current status of any payment using its payment ID.

```csharp
var query = new PaymentQuery
{
    PaymentId = "TR001179l3aVM1692860485496"
};

Result<QueryPaymentResult> result = await _bkashPayment.Query(query);

if (result.IsSucceeded)
{
    var status = result.Data.TransactionStatus; // e.g., "Completed", "Initiated", "Failed"
    var amount = result.Data.Amount;
    var transactionId = result.Data.TransactionId;
}
```

#### Common Transaction Statuses
- **Initiated** - Payment created but not completed
- **Completed** - Payment successfully processed
- **Cancelled** - Payment cancelled by user
- **Failed** - Payment failed

---

### Payment Refund

Refund a completed payment to the customer.

#### Required Information

- **Amount** - Amount to refund (max 2 decimal places, e.g., 25.20)
- **PaymentId** - The original payment ID
- **Reason** - Reason for refund (e.g., "Defective product", "Service not delivered")
- **SKU** - Product identifier from your system
- **TransactionId** - Transaction ID from the completed payment

```csharp
var command = new RefundPaymentCommand
{
    Amount = 100.50f,
    PaymentId = "TR001179l3aVM1692860485496",
    Reason = "Product not delivered",
    SKU = "PROD-12345",
    TransactionId = "TRX8D9N7PH6"
};

Result<RefundPaymentResult> result = await _bkashPayment.Refund(command);

if (result.IsSucceeded)
{
    var refundTransactionId = result.Data.RefundTransactionId;
    // Refund processed successfully
}
```

> **Note:** You can only refund completed payments. The refund amount cannot exceed the original payment amount.

---

## Understanding the Bkash Callback Mechanism

### What is a Bkash Callback?

A callback is Bkash's way of notifying your application about the payment result after the customer interacts with the Bkash payment page.

### Payment Flow Diagram

1. **User Action** → Customer clicks "Pay with Bkash" button on your website
2. **Create Payment** → Your server calls `_bkashPayment.Create()` method
3. **Redirect User** → User is redirected to Bkash payment page using `bkashURL`
4. **User Authentication** → Customer enters Bkash PIN and confirms payment
5. **Callback** → Bkash redirects user back to your `CallbackURL` with status
   - ✅ Success: `status=success&paymentID=xxx`
   - ❌ Failure: `status=failure&paymentID=xxx`
   - ⚠️ Cancel: `status=cancel&paymentID=xxx`
6. **Execute Payment** → Your server calls `_bkashPayment.Execute()` to finalize
7. **Confirmation** → Show success/failure message to customer

### Callback URL Options

You can use either:
- **Backend URL** (Recommended): `https://yourserver.com/api/payment/callback`
  - Better security
  - Allows server-side validation
  - Prevents client-side manipulation

- **Frontend URL**: `https://yourwebsite.com/payment/success`
  - Better UX (user stays on your site)
  - Requires additional API call to execute payment
  - Less secure (client can manipulate URLs)

---

## Troubleshooting

### Common Issues and Solutions

#### Issue: "Unauthorized" Error
**Cause:** Invalid credentials or expired token  
**Solution:** 
- Verify your `AppKey`, `AppSecret`, `MerchantUserName`, and `MerchantPassword`
- Ensure you're using the correct credentials for your environment (sandbox vs production)
- Check that `ProductionMode` setting matches your credentials

#### Issue: Payment Creation Fails
**Cause:** Invalid parameters or network issues  
**Solution:**
- Validate all required fields are provided
- Ensure `Amount` has maximum 2 decimal places
- Check that `CallbackURL` is a valid, publicly accessible URL
- Verify `MerchantInvoiceNumber` is unique

#### Issue: Callback Not Received
**Cause:** CallbackURL is not accessible or incorrect  
**Solution:**
- Ensure your CallbackURL is publicly accessible (not localhost in production)
- Test your callback endpoint independently
- Check firewall and security settings
- Use ngrok for local testing: `https://your-ngrok-url.com/api/callback`

#### Issue: "Payment already executed" Error
**Cause:** Attempting to execute the same payment twice  
**Solution:**
- Check your database before executing
- Implement idempotency checks
- Store payment status to prevent duplicate processing

#### Issue: Refund Fails
**Cause:** Invalid transaction or insufficient time elapsed  
**Solution:**
- Verify the transaction is completed
- Ensure you're using the correct `TransactionId` (not `PaymentId`)
- Check refund amount doesn't exceed original payment
- Wait 24 hours after payment for refund eligibility (Bkash policy)

---

## Environment-Specific Notes

### Sandbox (Testing) Environment
- Use sandbox credentials provided by Bkash
- Set `ProductionMode = false`
- Use test phone numbers and PINs provided by Bkash
- Sandbox URL: `https://sandbox.payment.bkash.com`

### Production Environment
- Use production credentials from Bkash
- Set `ProductionMode = true`
- Real money transactions occur
- Production URL: `https://payment.bkash.com`
- **Always test thoroughly in sandbox before going live!**

---

## Version Information

This package targets .NET Standard 2.0 for maximum compatibility across the .NET ecosystem.

**Current Version:** 2.0.0

### Version History
- **2.x.x** - .NET Standard 2.0 (Wide compatibility: .NET Framework 4.6.1+, .NET Core 2.0+, .NET 5+)
- **6.x.x** - .NET 6 specific (Legacy version)

---

## Support and Contribution

### Getting Help
- 📚 [Bkash Official Documentation](https://developer.bka.sh/)
- 🐛 [Report Issues](https://github.com/dipjyotisikder/PocketWallet/issues)
- 💡 [Feature Requests](https://github.com/dipjyotisikder/PocketWallet/issues/new)
- ❓ [Ask Questions](https://github.com/dipjyotisikder/PocketWallet/issues)

### Contributing
Contributions are welcome! Please feel free to submit pull requests or open issues on GitHub.

### License
This project is licensed under the MIT License - see the [LICENSE](https://github.com/dipjyotisikder/PocketWallet/blob/master/LICENSE) file for details.

---

## Credits

**Author:** Dipjyoti Sikder  
**Repository:** [PocketWallet on GitHub](https://github.com/dipjyotisikder/PocketWallet)

---

*Made with ❤️ for the .NET community in Bangladesh*