using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.BlogServices
{
    public class BlogService : IBlogService
    {
        private List<BlogListItemResponseModel> BlogData = new List<BlogListItemResponseModel>() {
            new BlogListItemResponseModel(){
                Id="1",
                Body="1Body",
                CreateTime=DateTime.Now,
                CoverImageUrl="",
                Type=1,
                ImageUrls=new List<string>(){ "","","" },
                Title="1Title",
            },
            new BlogListItemResponseModel(){
                Id="2",
                Body="2Body",
                CreateTime=DateTime.Now,
                CoverImageUrl="",
                Type=1,
                ImageUrls=new List<string>(){ "","","" },
                Title="2Title",
            },
            new BlogListItemResponseModel(){
                Id="3",
                Body="3Body",
                CreateTime=DateTime.Now,
                CoverImageUrl="",
                Type=2,
                Title="3Title",
                VideoUrl="",
            },
            new BlogListItemResponseModel(){
                Id="4",
                Body="4Body",
                CreateTime=DateTime.Now,
                CoverImageUrl="",
                Type=2,
                Title="4Title",
                VideoUrl="",
            },
        };
        private readonly HttpClient httpClient;
        public BlogService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
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
    }
}
