using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private readonly HttpClient httpClient;
        public BlogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<List<BlogListItemResponseModel>> GetBlogListAsync(BlogListRequestModel request)
        {
            //httpClient.GetFromJsonAsync<List<BlogListItemResponseModel>>($"/api/blog");
            List<BlogListItemResponseModel> resp = new List<BlogListItemResponseModel>();

            return Task.FromResult(resp);
        }

        public Task<BlogListItemResponseModel> GetBlogNextAsync(BlogNextRequestModel request)
        {
            //httpClient.GetFromJsonAsync<List<BlogListItemResponseModel>>($"/api/blog/next");
            BlogListItemResponseModel resp = new BlogListItemResponseModel();

            return Task.FromResult(resp);
        }
    }
}
