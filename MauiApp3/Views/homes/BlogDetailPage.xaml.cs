using MauiApp3.ViewModels;

namespace MauiApp3;

public partial class BlogDetailPage : ContentPage
{
    private BlogDetailPageViewModel blogDetailPageViewModel;
    public BlogDetailPage(BlogDetailPageViewModel vm)
	{
		InitializeComponent();
        BindingContext = blogDetailPageViewModel = vm;
	}
    async void OnSwiped(object sender, SwipedEventArgs e)
    {
        switch (e.Direction)
        {
            case SwipeDirection.Left:
                // Handle the swipe
                await blogDetailPageViewModel.InitData("id1");
                break;
            case SwipeDirection.Right:
                blogDetailPageViewModel.GoBackCommand.Execute(null);
                // Handle the swipe
                break;
            case SwipeDirection.Up:
                // Handle the swipe
                break;
            case SwipeDirection.Down:
                // Handle the swipe
                break;
        }
    }
}