using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private List<BlogListItemResponseModel> BlogData = new List<BlogListItemResponseModel>();
        private readonly HttpClient httpClient;
        public BlogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            for (int i = 0; i < 100; i++)
            {
                var blog = new BlogListItemResponseModel()
                {
                    Id = "id"+i,
                    Body = "Body"+i,
                    CreateTime = DateTime.Now,
                    CoverImageUrl = "",
                    Type = 1,
                    ImageUrls = new List<string>() { "", "", "" },
                    Title = "Title"+i,
                };
                BlogData.Add(blog);
            }
        }
        public Task<List<BlogListItemResponseModel>> GetBlogListAsync(BlogListRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<BlogListItemResponseModel>>($"/api/blog");
            List<BlogListItemResponseModel> resp = BlogData.Skip(request.PageSize * ( request.PageIndex-1)).Take(request.PageSize).ToList();

            return Task.FromResult(resp);
        }

        public Task<BlogListItemResponseModel> GetBlogNextAsync(BlogNextRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<BlogListItemResponseModel>>($"/api/blog/next");
            BlogListItemResponseModel resp= new BlogListItemResponseModel();
            var current = BlogData.FirstOrDefault(a=>a.Id==request.CurrentId);
            if (current!=null)
            {
                if (request.Action == 1)
                {
                    resp = BlogData.Where(a => a.CreateTime > current.CreateTime).OrderBy(a => a.CreateTime).FirstOrDefault();
                    if (resp==null)
                    {
                        resp = BlogData.OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    }
                }
                else
                {
                    resp = BlogData.Where(a => a.CreateTime < current.CreateTime).OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    if (resp == null)
                    {
                        resp = BlogData.OrderBy(a => a.CreateTime).FirstOrDefault();
                    }
                }
                
            }
            


            return Task.FromResult(resp);
        }

        public Task<BlogListItemResponseModel> GetBlogAsync(string id)
        {
            var blog = BlogData.FirstOrDefault(a => a.Id == id);

            return Task.FromResult(blog);
        }
    }
}
