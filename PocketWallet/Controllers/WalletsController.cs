using Microsoft.AspNetCore.Mvc;
using PocketWallet.Bkash;
using PocketWallet.Bkash.Abstraction;
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
        [ProducesResponseType(typeof(Result<CreateBkashPaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBkashPayment([FromBody] CreateBkashPayment request)
        {
            var result = await _bkashPayment.CreatePayment(request);
            return Ok(result);
        }

        [HttpPost("bkash/executePayment")]
        [ProducesResponseType(typeof(Result<ExecuteBkashPaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ExecuteBkashPayment([FromBody] ExecuteBkashPayment request)
        {
            var result = await _bkashPayment.ExecutePayment(request);
            return Ok(result);
        }

        [HttpPost("bkash/queryPayment")]
        [ProducesResponseType(typeof(Result<QueryBkashPaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> QueryBkashPayment([FromQuery] QueryBkashPayment request)
        {
            var result = await _bkashPayment.QueryPayment(request);
            return Ok(result);
        }

        [HttpGet("bkash/callback")]
        [ProducesResponseType(typeof(Result<CreateBkashPaymentResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateBkashPaymentCallback(
            [FromQuery] string paymentID,
            [FromQuery] string status)
        {
            if (status == "success")
            {
                // Check if any other payment existed with this same paymentID
                // If not
                var result = await _bkashPayment.ExecutePayment(new ExecuteBkashPayment
                {
                    PaymentID = paymentID,
                });

                return Ok(result);
            }
            return Ok();
        }
    }
}