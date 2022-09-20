using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private List<AccountListItemResponseModel> AccountData = new List<AccountListItemResponseModel>();
        private readonly HttpClient httpClient;
        public AccountService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            for (int i = 0; i < 100; i++)
            {
                var blog = new AccountListItemResponseModel()
                {
                    Id = "id"+i,
                    Body = "Body"+i,
                    CreateTime = DateTime.Now,
                    CoverImageUrl = "",
                    Type = 1,
                    ImageUrls = new List<string>() { "", "", "" },
                    Title = "Title"+i,
                };
                AccountData.Add(blog);
            }
        }
        public Task<List<AccountListItemResponseModel>> GetAccountListAsync(AccountListRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<AccountListItemResponseModel>>($"/api/blog");
            List<AccountListItemResponseModel> resp = AccountData.Skip(request.PageSize * ( request.PageIndex-1)).Take(request.PageSize).ToList();

            return Task.FromResult(resp);
        }

        public Task<AccountListItemResponseModel> GetAccountNextAsync(AccountNextRequestModel request)
        {
            //await httpClient.GetFromJsonAsync<List<AccountListItemResponseModel>>($"/api/blog/next");
            AccountListItemResponseModel resp= new AccountListItemResponseModel();
            var current = AccountData.FirstOrDefault(a=>a.Id==request.CurrentId);
            if (current!=null)
            {
                if (request.Action == 1)
                {
                    resp = AccountData.Where(a => a.CreateTime > current.CreateTime).OrderBy(a => a.CreateTime).FirstOrDefault();
                    if (resp==null)
                    {
                        resp = AccountData.OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    }
                }
                else
                {
                    resp = AccountData.Where(a => a.CreateTime < current.CreateTime).OrderByDescending(a => a.CreateTime).FirstOrDefault();
                    if (resp == null)
                    {
                        resp = AccountData.OrderBy(a => a.CreateTime).FirstOrDefault();
                    }
                }
                
            }
            


            return Task.FromResult(resp);
        }

        public Task<AccountListItemResponseModel> GetAccountAsync(string id)
        {
            var blog = AccountData.FirstOrDefault(a => a.Id == id);

            return Task.FromResult(blog);
        }
    }
}
