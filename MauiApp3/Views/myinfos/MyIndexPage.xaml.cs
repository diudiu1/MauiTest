using MauiApp3.Configs;
using MauiApp3.Services.AccountServices;
using MauiApp3.ViewModels;

namespace MauiApp3;

public partial class MyIndexPage : ContentPage
{
    private MyIndexPageViewModel _vm;
    public MyIndexPage( MyIndexPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = _vm = vm;
        
    }
    protected override async void OnAppearing()
    {
        if (IAccountService.CurrentAccount == null)
        {
           await Shell.Current.GoToAsync(nameof(LoginPage));
        }
        else 
        {
            _vm.GetMyInfoCommand.Execute(null);
        }
        
    }
}