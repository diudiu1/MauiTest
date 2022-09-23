using MauiApi.EFContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly ILogger<BlogController> _logger;
        private MauiDbContext _context;
        public BlogController(ILogger<BlogController> logger, MauiDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<List<BlogListItemResponseModel>> Get([FromQuery]BlogListRequestModel request)
        {
            var query = from a in _context.BlogInfo
                        join b in _context.AccountInfo on a.AccountId equals b.Id
                        select new BlogListItemResponseModel {
                            Id = a.Id,
                            Content = a.Content,
                            CoverImageUrl = a.CoverImageUrl,
                            CreateTime = a.CreateTime,
                            Title = a.Title,
                            Type = a.Type,
                            VideoUrl = a.VideoUrl,
                            ImageUrls = a.ImageUrls,
                            AccountAvatarUrl=b.AvatarUrl,
                            AccountName=b.Name,
                            Follow=0
                        };


            var resp = await query.Skip(request.PageSize * (request.PageIndex - 1)).Take(request.PageSize).ToListAsync();
            return resp;
        }
        [HttpGet("{id}")]
        public async Task<BlogListItemResponseModel> Get(string id)
        {
            var query = from a in _context.BlogInfo
                        join b in _context.AccountInfo on a.AccountId equals b.Id
                        where a.Id==id
                        select new BlogListItemResponseModel
                        {
                            Id = a.Id,
                            Content = a.Content,
                            CoverImageUrl = a.CoverImageUrl,
                            CreateTime = a.CreateTime,
                            Title = a.Title,
                            Type = a.Type,
                            VideoUrl = a.VideoUrl,
                            ImageUrls = a.ImageUrls,
                            AccountAvatarUrl = b.AvatarUrl,
                            AccountName = b.Name,
                            Follow = 0
                        };
            var resp = await query.FirstOrDefaultAsync();
            return resp;
        }
        
    }
    public class BlogListRequestModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class BlogListItemResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// ∑‚√ÊÕº
        /// </summary>
        public string CoverImageUrl { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// 1 Õº∆¨ 2  ”∆µ
        /// </summary>
        public int Type { get; set; }
        public string? ImageUrls { get; set; }
        public string? VideoUrl { get; set; }
        public DateTime CreateTime { get; set; }
        public int Follow { get; set; }
        public string AccountName { get; set; }
        public string AccountAvatarUrl { get; set; }
    }
}