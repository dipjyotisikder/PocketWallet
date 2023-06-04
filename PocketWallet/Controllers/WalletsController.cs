using Microsoft.AspNetCore.Mvc;

namespace PocketWallet.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WalletsController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WalletsController> _logger;

        public WalletsController(ILogger<WalletsController> logger)
        {
            _logger = logger;
        }

    }
}