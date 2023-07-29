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
        [ProducesResponseType(typeof(Result<CreatePaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBkashPayment([FromBody] CreatePaymentCommand request)
        {
            var result = await _bkashPayment.Create(request);
            return Ok(result);
        }

        [HttpPost("bkash/queryPayment")]
        [ProducesResponseType(typeof(Result<PaymentQueryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> QueryBkashPayment([FromBody] PaymentQuery request)
        {
            var result = await _bkashPayment.Query(request);
            return Ok(result);
        }

        [HttpGet("bkash/callback")]
        [ProducesResponseType(typeof(Result<ExecutePaymentResponse>), StatusCodes.Status200OK)]
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
                    PaymentID = paymentID
                });

                return Ok(result);
            }
            return Ok();
        }
    }
}