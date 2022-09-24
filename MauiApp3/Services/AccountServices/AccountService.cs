using MauiApp3.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MauiApp3.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly HttpClientService _httpClient;
        private AccountInfo currentAccount;
        public AccountInfo CurrentAccount { get => currentAccount; set => currentAccount=value; }

        public AccountService(HttpClientService  httpClientService)
        {
            _httpClient = httpClientService;
            
        }

        public async Task<LoginResponseModel> LoginAsync(LoginRequestModel request)
        {
            var resp= await _httpClient.PostAsync<LoginRequestModel,LoginResponseModel>(Appsettings.LoginUrl, request);
            

            return resp;
        }

        public async Task TestAsync()
        {
            var resp = await _httpClient.GetAsync<LoginResponseModel>(Appsettings.TestUrl);
        }

        public Task ClearAsync()
        {
            SecureStorage.Default.RemoveAll();
            IAccountService.CurrentAccount = null;
            return Task.CompletedTask;
        }

        public async Task<AccountInfoResponseModel> MyInfoAsync()
        {
            return await _httpClient.GetAsync<AccountInfoResponseModel>(Appsettings.MyInfoUrl);
        }

        public async Task<AccountInfoResponseModel> UpdateAvatarAsync(Stream file,string name)
        {
            return await _httpClient.PostFileAsync<AccountInfoResponseModel>(Appsettings.UpdateAvatar, file, name);
            
        }
    }
}
