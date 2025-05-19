using PocketWallet.Bkash.DependencyInjection;
using PocketWallet.Options;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
