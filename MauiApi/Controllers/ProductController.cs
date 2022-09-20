using Microsoft.AspNetCore.Mvc;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
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