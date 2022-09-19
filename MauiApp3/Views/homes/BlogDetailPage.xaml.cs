using MauiApp3.Views.homes;

namespace MauiApp3;

public partial class BlogDetailPage : ContentPage
{
    public BlogDetailPage()
	{
		InitializeComponent();

        BindingContext= new BlogDetailPageViewModel();
	}
}