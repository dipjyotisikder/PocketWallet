using Microsoft.AspNetCore.Mvc;
using PocketWallet.Bkash;

namespace PocketWallet.Controllers;

[ApiController]
[Route("api/wallets")]
/// <summary>
/// Exposes wallet-related operations for different payment providers.
/// </summary>
public class WalletsController : ControllerBase
{
    private readonly ILogger<WalletsController> _logger;
    private readonly IBkashPayment _bkashPayment;

    /// <summary>
    /// Initializes a new instance of the <see cref="WalletsController"/> class.
    /// </summary>
    /// <param name="logger">Application logger instance.</param>
    /// <param name="bkashPayment">Bkash payment service instance.</param>
    public WalletsController(
        ILogger<WalletsController> logger,
        IBkashPayment bkashPayment)
    {
        _logger = logger;
        _bkashPayment = bkashPayment;
    }

    [HttpPost("bkash/createPayment")]
    [ProducesResponseType(typeof(Result<CreatePaymentResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    /// <summary>
    /// Initiates a payment request against the Bkash gateway.
    /// </summary>
    /// <param name="request">Payment initiation payload.</param>
    /// <returns>Operation result from Bkash.</returns>
    public async Task<IActionResult> CreateBkashPayment([FromBody] CreatePaymentCommand request)
    {
        var result = await _bkashPayment.Create(request);
        return Ok(result);
    }

    [HttpPost("bkash/queryPayment")]
    [ProducesResponseType(typeof(Result<QueryPaymentResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    /// <summary>
    /// Retrieves payment details from Bkash for a given payment identifier.
    /// </summary>
    /// <param name="request">Payment query payload.</param>
    /// <returns>Operation result from Bkash.</returns>
    public async Task<IActionResult> QueryBkashPayment([FromBody] PaymentQuery request)
    {
        var result = await _bkashPayment.Query(request);
        return Ok(result);
    }

    [HttpGet("bkash/callback")]
    [ProducesResponseType(typeof(Result<ExecutePaymentResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    /// <summary>
    /// Handles callback notifications from Bkash to finalize payment execution.
    /// </summary>
    /// <param name="paymentID">Payment identifier returned by Bkash.</param>
    /// <param name="status">Reported payment status.</param>
    /// <returns>Operation result from Bkash when execution succeeds.</returns>
    public async Task<IActionResult> PaymentCallback(
        [FromQuery] string paymentID,
        [FromQuery] string status)
    {
        if (status == "success")
        {
            // Check if any other payment existed with this same paymentID
            // If not
            var result = await _bkashPayment.Execute(new ExecutePaymentCommand
            {
                PaymentId = paymentID
            });

            return Ok(result);
        }
        return Ok();
    }

    [HttpPost("bkash/refund")]
    [ProducesResponseType(typeof(Result<ExecutePaymentResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    /// <summary>
    /// Initiates a refund request against the Bkash gateway.
    /// </summary>
    /// <param name="command">Refund payload containing transaction identifiers.</param>
    /// <returns>Operation result from Bkash.</returns>
    public async Task<IActionResult> PaymentRefund([FromBody] RefundPaymentCommand command)
    {
        var result = await _bkashPayment.Refund(command);
        return Ok(result);
    }
}