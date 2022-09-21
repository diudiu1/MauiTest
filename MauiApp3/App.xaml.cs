using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        var shell = MauiProgram.Services.GetService<AppShell>();
        MainPage = shell;
        //Init();
    }
    async void Init()
    {
        bool needLogin = true;
        string token = await SecureStorage.Default.GetAsync("Token");
        string expiredTimeStr = await SecureStorage.Default.GetAsync("ExpiredTime");
        if (expiredTimeStr != null && DateTime.TryParse(expiredTimeStr, out DateTime expiredTime))
        {
            if (expiredTime > DateTime.Now)
            {
                needLogin = false;
                IAccountService.CurrentAccount = new AccountInfo()
                {
                    Token = token,
                    ExpiredTime = expiredTime
                };
            }
        }
        if (needLogin)
        {
            var login = MauiProgram.Services.GetService<LoginPage>();
            MainPage = login;
            //await Navigation.PushAsync(login);
#if WINDOWS
            //var login = MauiProgram.Services.GetService<LoginPage>();
            //await Navigation.PushAsync(login);
            //MainPage = login;
            //MainPage = new AppShell();
#else
            //await Shell.Current.GoToAsync(nameof(LoginPage));
#endif
        }

    }
}
