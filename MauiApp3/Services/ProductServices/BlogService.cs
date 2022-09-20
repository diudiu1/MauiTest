using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private List<ProductListItemResponseModel> ProductData = new List<ProductListItemResponseModel>();
        private readonly HttpClient httpClient;
        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            for (int i = 0; i < 100; i++)
            {
                var blog = new ProductListItemResponseModel()
                {
                    Id = "id"+i,
                    Body = "Body"+i,
                    CreateTime = DateTime.Now,
                    CoverImageUrl = "",
                    Type = 1,
                    ImageUrls = new List<string>() { "", "", "" },
                    Title = "Title"+i,
                };
                ProductData.Add(blog);
            }
        }
        public Task<List<ProductListItemResponseModel>> GetProductListAsync(ProductListRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<ProductListItemResponseModel>>($"/api/blog");
            List<ProductListItemResponseModel> resp = ProductData.Skip(request.PageSize * ( request.PageIndex-1)).Take(request.PageSize).ToList();

            return Task.FromResult(resp);
        }

        public Task<ProductListItemResponseModel> GetProductNextAsync(ProductNextRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<ProductListItemResponseModel>>($"/api/blog/next");
            ProductListItemResponseModel resp= new ProductListItemResponseModel();
            var current = ProductData.FirstOrDefault(a=>a.Id==request.CurrentId);
            if (current!=null)
            {
                if (request.Action == 1)
                {
                    resp = ProductData.Where(a => a.CreateTime > current.CreateTime).OrderBy(a => a.CreateTime).FirstOrDefault();
                    if (resp==null)
                    {
                        resp = ProductData.OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    }
                }
                else
                {
                    resp = ProductData.Where(a => a.CreateTime < current.CreateTime).OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    if (resp == null)
                    {
                        resp = ProductData.OrderBy(a => a.CreateTime).FirstOrDefault();
                    }
                }
                
            }
            


            return Task.FromResult(resp);
        }

        public Task<ProductListItemResponseModel> GetProductAsync(string id)
        {
            var blog = ProductData.FirstOrDefault(a => a.Id == id);

            return Task.FromResult(blog);
        }
    }
}
