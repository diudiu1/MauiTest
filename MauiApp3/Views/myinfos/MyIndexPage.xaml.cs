using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class MyIndexPage : ContentPage
{
    private readonly IAccountService _accountService;
	public MyIndexPage(IAccountService accountService)
	{
		InitializeComponent();
        _accountService=accountService;

    }
    async void OnButtonClicked(object sender, EventArgs args)
    {
        await _accountService.ClearAsync();
        await Shell.Current.GoToAsync(nameof(LoginPage));
    }
    async void OnTestClicked(object sender, EventArgs args)
    {
        await _accountService.TestAsync();
    }
}