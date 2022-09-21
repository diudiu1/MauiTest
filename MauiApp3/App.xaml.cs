using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        var shell = MauiProgram.Services.GetService<AppShell>();
        MainPage = shell;
        Init();
    }
    async void Init()
    {
        string token = await SecureStorage.Default.GetAsync("Token");
        string expiredTimeStr = await SecureStorage.Default.GetAsync("ExpiredTime");
        if (expiredTimeStr != null && DateTime.TryParse(expiredTimeStr, out DateTime expiredTime))
        {
            if (expiredTime > DateTime.Now)
            {
                IAccountService.CurrentAccount = new AccountInfo()
                {
                    Token = token,
                    ExpiredTime = expiredTime
                };
            }
        }

    }
}
