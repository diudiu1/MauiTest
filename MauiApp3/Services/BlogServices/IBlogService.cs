using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.BlogServices
{
    public interface IBlogService
    {
        Task<List<BlogListItemResponseModel>> GetBlogListAsync(BlogListRequestModel request);
        Task<BlogListItemResponseModel> GetBlogNextAsync(BlogNextRequestModel request);
    }
}
