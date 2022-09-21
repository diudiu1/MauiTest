using MauiApp3.Services.AccountServices;

namespace MauiApp3;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        
    }
	public void GotoHome()
	{
        Shell.Current.CurrentItem = homeTabItem;
    }
    protected override async void OnParentSet()
    {
        base.OnParentSet();
        
        
    }
}
