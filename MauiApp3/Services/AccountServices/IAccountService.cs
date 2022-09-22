using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.AccountServices
{
    public interface IAccountService
    {
        Task<LoginResponseModel> LoginAsync(LoginRequestModel request);
        Task<AccountInfoResponseModel> MyInfoAsync();
        Task<AccountInfoResponseModel> UpdateAvatarAsync(Stream file,string name);
        Task TestAsync();
        Task ClearAsync();
        public static AccountInfo CurrentAccount { get; set; }
    }
}
