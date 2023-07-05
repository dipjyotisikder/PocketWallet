using Microsoft.AspNetCore.Mvc;
using PocketWallet.Bkash.Abstraction;

namespace PocketWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
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

        public async Task<IActionResult> CreateBkashPayment()
        {
            var result = await _bkashPayment.CreatePayment(new Bkash.Models.CreateBkashPayment
            {

            });
            return Ok(await Task.FromResult("Created"));
        }
    }
}