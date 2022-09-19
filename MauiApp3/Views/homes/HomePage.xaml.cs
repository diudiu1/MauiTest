using MauiApp3.Views.homes;

namespace MauiApp3;

public partial class HomePage : ContentPage
{
    public HomePage(HomePageViewModel vm)
	{
		InitializeComponent();

        BindingContext= vm;
	}
    //async void OnCollectionViewRemainingItemsThresholdReached(object sender, EventArgs e)
    //{
    //    await viewModel.NextPageData();
    //}
}