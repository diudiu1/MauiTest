using MauiApp3.ViewModels;

namespace MauiApp3;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginPageViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }
}