using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();
        if (IAccountService.CurrentAccount == null)
        {
            var login = MauiProgram.Services.GetService<LoginPage>();
            //await Navigation.PushAsync(login);
            MainPage = login;
            //MainPage = new AppShell();
            //await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else
        {
            MainPage = new AppShell();
        }
        
	}
}
