﻿namespace MauiApp3;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(BlogDetailPage), typeof(BlogDetailPage));
    }
}
