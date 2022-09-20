using Microsoft.AspNetCore.Mvc;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return Enumerable.Range(1, 5).Select(index => "")
            .ToArray();
        }
    }
}