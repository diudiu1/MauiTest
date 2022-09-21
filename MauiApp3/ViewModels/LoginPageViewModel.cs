using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Services.AccountServices;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.ViewModels
{
    public partial class LoginPageViewModel : ObservableObject
    {
        private readonly IAccountService _accountService;
        public LoginPageViewModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ObservableProperty]
        public string userName="user0";
        [ObservableProperty]
        public string password= "123456";
        [RelayCommand]
        async Task Login()
        {
            var request = new LoginRequestModel()
            {
                UserName = userName,
                Password = password
            };
            var resp = await _accountService.LoginAsync(request);
            if (resp!=null)
            {
                IAccountService.CurrentAccount = new AccountInfo()
                {
                    Token = resp.Token,
                    ExpiredTime = DateTime.Now.AddMinutes(resp.ExpiredTime),
                };
                await SecureStorage.Default.SetAsync("Token", IAccountService.CurrentAccount.Token);
                await SecureStorage.Default.SetAsync("ExpiredTime", IAccountService.CurrentAccount.ExpiredTime.ToString("yyyy-MM-dd HH:mm:ss"));

                //var shell = MauiProgram.Services.GetService<AppShell>();
                //Application.Current.MainPage = shell;
                
                //Application.Current.MainPage = new AppShell();
                //((AppShell)Shell.Current).GotoHome();

                await Shell.Current.GoToAsync("..");
            }
        }
    }
}
