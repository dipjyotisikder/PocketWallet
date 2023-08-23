using Microsoft.AspNetCore.Mvc;
using PocketWallet.Bkash;
using PocketWallet.Bkash.Models;

namespace PocketWallet.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly ILogger<WalletsController> _logger;
        private readonly IBkashPayment _bkashPayment;

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
        public async Task<IActionResult> CreateBkashPayment([FromBody] CreatePaymentCommand request)
        {
            var result = await _bkashPayment.Create(request);
            return Ok(result);
        }

        [HttpPost("bkash/queryPayment")]
        [ProducesResponseType(typeof(Result<QueryPaymentResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> QueryBkashPayment([FromBody] PaymentQuery request)
        {
            var result = await _bkashPayment.Query(request);
            return Ok(result);
        }

        [HttpGet("bkash/callback")]
        [ProducesResponseType(typeof(Result<ExecutePaymentResult>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        public async Task<IActionResult> PaymentRefund([FromBody] RefundPaymentCommand command)
        {
            var result = await _bkashPayment.Refund(command);
            return Ok(result);
        }
    }
}