using Microsoft.AspNetCore.Mvc;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;

        public BlogController(ILogger<BlogController> logger)
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