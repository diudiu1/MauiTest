using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.AccountServices
{
    public interface IAccountService
    {
        Task<List<AccountListItemResponseModel>> GetAccountListAsync(AccountListRequestModel request);
        Task<AccountListItemResponseModel> GetAccountAsync(string id);
        Task<AccountListItemResponseModel> GetAccountNextAsync(AccountNextRequestModel request);
    }
}
