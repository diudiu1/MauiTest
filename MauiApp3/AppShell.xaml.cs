using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class AppShell : Shell
{
    private readonly IAccountService _accountService;
	public AppShell()
	{
		InitializeComponent();

    }
	public void GotoHome()
	{
        Shell.Current.CurrentItem = homeTabItem;
    }
    protected override void OnNavigated(ShellNavigatedEventArgs args)
    {
        //if (_accountService?.CurrentAccount==null)
        //{
        //    Shell.Current.GoToAsync(nameof(LoginPage));
        //}
    }
}
