﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.Views.homes
{
    [QueryProperty("Id", "Id")]
    public partial class BlogDetailPageViewModel : ObservableObject, IQueryAttributable
    {
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Id"))
            {
                var value = query["Id"].ToString();
                await InitData(value);
            }
        }
        private readonly IBlogService _blogService;
        public BlogDetailPageViewModel()
        {
            //_blogService = blogService;
            _blogService = new BlogService(null);
            //title = "0000000";
            //InitData();  不能在此处初始化，页面传值Query 还没有生效
        }
        async Task InitData(string id)
        {
            blog = await _blogService.GetBlogAsync(id);
            if (blog!=null)
            {
                title = blog.Title;
                this.OnPropertyChanged("Title");
            }
        }
        

        [ObservableProperty]
        public BlogListItemResponseModel blog;
        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public string title;
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
