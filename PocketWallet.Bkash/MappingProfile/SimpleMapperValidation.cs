namespace PocketWallet.Bkash.MappingProfile;

/// <summary>
/// Demonstrates SimpleMapper usage and validates all configured mappings.
/// This class can be used for testing or as reference for developers.
/// </summary>
public static class SimpleMapperValidation
{
    /// <summary>
    /// Validates all SimpleMapper configurations by creating test objects and mapping them.
    /// Returns true if all mappings work correctly, false otherwise.
    /// </summary>
    public static bool ValidateAllMappings()
    {
        try
        {
            // Test 1: CreatePaymentCommand -> CreatePaymentRequest
            var createCommand = new CreatePaymentCommand
            {
                PayerReference = "01234567890",
                MerchantInvoiceNumber = "INV123",
                CallbackURL = "https://example.com/callback",
                Amount = 100.50f,
                Intent = "sale",
                Currency = "BDT"
            };
            var createRequest = SimpleMapper.Map<CreatePaymentCommand, CreatePaymentRequest>(createCommand);
            if (createRequest == null || createRequest.Amount != "100.5")
            {
                return false;
            }

            // Test 2: CreatePaymentResponse -> CreatePaymentResult
            var createResponse = new CreatePaymentResponse
            {
                PaymentId = "PAY123",
                Amount = "150.75",
                BkashURL = "https://bkash.com/pay",
                CallbackURL = "https://example.com/callback",
                Intent = "sale",
                Currency = "BDT",
                PaymentCreateTime = "2024-01-01T12:00:00 GMTZ",
                TransactionStatus = "Initiated",
                MerchantInvoiceNumber = "INV123"
            };
            var createResult = SimpleMapper.Map<CreatePaymentResponse, CreatePaymentResult>(createResponse);
            if (createResult == null || createResult.Amount != 150.75f)
            {
                return false;
            }

            // Test 3: ExecutePaymentCommand -> ExecutePaymentRequest
            var executeCommand = new ExecutePaymentCommand { PaymentId = "PAY123" };
            var executeRequest = SimpleMapper.Map<ExecutePaymentCommand, ExecutePaymentRequest>(executeCommand);
            if (executeRequest == null || executeRequest.PaymentId != "PAY123")
            {
                return false;
            }

            // Test 4: ExecutePaymentResponse -> ExecutePaymentResult
            var executeResponse = new ExecutePaymentResponse
            {
                PaymentId = "PAY123",
                Amount = "200.00",
                TransactionId = "TRX123",
                TransactionStatus = "Completed",
                CustomerMSISDN = "01234567890",
                Currency = "BDT",
                Intent = "sale",
                MerchantInvoiceNumber = "INV123",
                PaymentExecuteTime = "2024-01-01T12:05:00 GMTZ"
            };
            var executeResult = SimpleMapper.Map<ExecutePaymentResponse, ExecutePaymentResult>(executeResponse);
            if (executeResult == null || executeResult.Amount != 200.00f)
            {
                return false;
            }

            // Test 5: PaymentQuery -> QueryPaymentRequest
            var paymentQuery = new PaymentQuery { PaymentId = "PAY123" };
            var queryRequest = SimpleMapper.Map<PaymentQuery, QueryPaymentRequest>(paymentQuery);
            if (queryRequest == null || queryRequest.PaymentId != "PAY123")
            {
                return false;
            }

            // Test 6: QueryPaymentResponse -> QueryPaymentResult
            var queryResponse = new QueryPaymentResponse
            {
                PaymentId = "PAY123",
                Amount = "300.50",
                TransactionId = "TRX123",
                TransactionStatus = "Completed",
                Currency = "BDT",
                Intent = "sale",
                MerchantInvoice = "INV123"
            };
            var queryResult = SimpleMapper.Map<QueryPaymentResponse, QueryPaymentResult>(queryResponse);
            if (queryResult == null || queryResult.Amount != 300.50f)
            {
                return false;
            }

            // Test 7: RefundPaymentCommand -> RefundPaymentRequest
            var refundCommand = new RefundPaymentCommand
            {
                PaymentId = "PAY123",
                TransactionId = "TRX123",
                Amount = 50.25f,
                Reason = "Customer request",
                SKU = "SKU123"
            };
            var refundRequest = SimpleMapper.Map<RefundPaymentCommand, RefundPaymentRequest>(refundCommand);
            if (refundRequest == null || refundRequest.Amount != "50.25")
            {
                return false;
            }

            // Test 8: RefundPaymentResponse -> RefundPaymentResult
            var refundResponse = new RefundPaymentResponse
            {
                Amount = "50.25",
                TransactionStatus = "Completed",
                CompletedTime = "2024-01-01T12:10:00 GMTZ",
                OriginalTransactionId = "TRX123",
                RefundTransactionId = "REFUND123",
                Currency = "BDT"
            };
            var refundResult = SimpleMapper.Map<RefundPaymentResponse, RefundPaymentResult>(refundResponse);
            if (refundResult == null || refundResult.Amount != 50.25f)
            {
                return false;
            }

            // Test 9: Verify default values and transformations
            var commandWithDefaults = new CreatePaymentCommand
            {
                PayerReference = "", // Should become " "
                MerchantInvoiceNumber = "INV456",
                CallbackURL = "https://example.com/callback",
                Amount = 75.0f,
                Intent = "", // Should become "sale"
                Currency = "" // Should become "BDT"
            };
            var requestWithDefaults = SimpleMapper.Map<CreatePaymentCommand, CreatePaymentRequest>(commandWithDefaults);
            if (requestWithDefaults.PayerReference != " ")
            {
                return false;
            }

            if (requestWithDefaults.Intent != CONSTANTS.SALE)
            {
                return false;
            }

            if (requestWithDefaults.Currency != CONSTANTS.BDT)
            {
                return false;
            }

            if (requestWithDefaults.Mode != CONSTANTS.WITHOUT_AGREEMENT_CODE)
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets a detailed validation report of all mappings.
    /// </summary>
    public static string GetValidationReport()
    {
        var report = "SimpleMapper Validation Report\n";
        report += "================================\n\n";

        try
        {
            // Test each mapping individually
            report += TestMapping<CreatePaymentCommand, CreatePaymentRequest>("CreatePaymentCommand", "CreatePaymentRequest");
            report += TestMapping<CreatePaymentResponse, CreatePaymentResult>("CreatePaymentResponse", "CreatePaymentResult");
            report += TestMapping<ExecutePaymentCommand, ExecutePaymentRequest>("ExecutePaymentCommand", "ExecutePaymentRequest");
            report += TestMapping<ExecutePaymentResponse, ExecutePaymentResult>("ExecutePaymentResponse", "ExecutePaymentResult");
            report += TestMapping<PaymentQuery, QueryPaymentRequest>("PaymentQuery", "QueryPaymentRequest");
            report += TestMapping<QueryPaymentResponse, QueryPaymentResult>("QueryPaymentResponse", "QueryPaymentResult");
            report += TestMapping<RefundPaymentCommand, RefundPaymentRequest>("RefundPaymentCommand", "RefundPaymentRequest");
            report += TestMapping<RefundPaymentResponse, RefundPaymentResult>("RefundPaymentResponse", "RefundPaymentResult");

            report += "\n? All mappings validated successfully!\n";
        }
        catch (Exception ex)
        {
            report += $"\n? Validation failed: {ex.Message}\n";
        }

        return report;
    }

    private static string TestMapping<TSource, TDestination>(string sourceName, string destName) where TDestination : new()
    {
        try
        {
            var source = Activator.CreateInstance<TSource>();
            var dest = SimpleMapper.Map<TSource, TDestination>(source);
            return $"? {sourceName} -> {destName}: OK\n";
        }
        catch (Exception ex)
        {
            return $"? {sourceName} -> {destName}: FAILED - {ex.Message}\n";
        }
    }
}
