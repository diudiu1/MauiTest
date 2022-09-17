using MauiApp3.Views.homes;

namespace MauiApp3;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();

		BindingContext = new HomePageViewModel();
	}
}