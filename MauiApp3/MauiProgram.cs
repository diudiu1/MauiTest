using MauiApp3.Services.BlogServices;
using MauiApp3.Views.homes;
using Microsoft.Extensions.DependencyInjection;

namespace MauiApp3;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});


        var services = builder.Services;

		services.AddSingleton<IBlogService, BlogService>();

        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<HomePage>();

        services.AddSingleton<BlogDetailPageViewModel>();
        services.AddSingleton<BlogDetailPage>();


        Routing.RegisterRoute(nameof(BlogDetailPage), typeof(BlogDetailPage));
        return builder.Build();
	}
}
