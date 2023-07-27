# PocketWallet.Bkash Tutorial - Step by Step process.

## Basic Information

### What is PocketWallet.Bkash?

PocketWallet.Bkash is a library that is built with .NET 6, offering integration with Bkash, a popular Bangladeshi e-wallet system. The application is designed to streamline the payment process for businesses and users by providing seamless support for Bkash transactions.

### Features

- Smooth integration process.
- Complete Support for Bkash transactions.

### Technologies Used

The library is developed using the following technologies:

- .NET 6
- C# language
- Bkash API

## Usage

### Add Configuration
Add this appsettings.json configuration block and provide appropriate values. Note: These informations need to be collected found from Bkash support.
```
"BkashOptions": {
    "MerchantUserName": "",
    "MerchantPassword": "",
    "AppKey": "",
    "AppSecret": "",
    "ProductionMode": false
}
```

### Dependency Injection
1. Create a "BkashOptions" class matching with configuration files "BkashOptions" section.
```
public class BkashOptions
{
    public string MerchantUserName { get; set; } = string.Empty;
    public string MerchantPassword { get; set; } = string.Empty;
    public string AppKey { get; set; } = string.Empty;
    public string AppSecret { get; set; } = string.Empty;
    public bool ProductionMode { get; set; }
}
```
2. Add Bkash Dependency
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

### Payment Process

To Create Payment:
- Add "IBkashPayment" interface in the constructor of any class as follows,
```
private readonly IBkashPayment _bkashPayment;
public WalletsController(IBkashPayment bkashPayment)
{
    _bkashPayment = bkashPayment;
}
```
- Call the payment method.
```
var result = await _bkashPayment.CreatePayment(new CreateBkashPayment{});

```
Please check property summary for better understanding about the responsibility of the property.

Thus, the other methods will work similarly.