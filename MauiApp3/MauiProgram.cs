using MauiApp3.Services.AccountServices;
using MauiApp3.Services.BlogServices;
using MauiApp3.Services.MessageServices;
using MauiApp3.Services.ProductServices;
using MauiApp3.ViewModels;
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
        services.AddSingleton<IProductService, ProductService>();
        services.AddSingleton<IMessageService, MessageService>();
        services.AddSingleton<IAccountService, AccountService>();



        services.AddSingleton<HomePageViewModel>();
        services.AddSingleton<BlogDetailPageViewModel>();


        services.AddSingleton<HomePage>();
        services.AddSingleton<BlogDetailPage>();

        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(MessagePage), typeof(MessagePage));
        Routing.RegisterRoute(nameof(BlogDetailPage), typeof(BlogDetailPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        return builder.Build();
	}
}
