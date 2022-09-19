using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.Views.homes
{
    public partial class HomePageViewModel : ObservableObject
    {
        
        private readonly IBlogService _blogService;
        public HomePageViewModel()
        {
            //_blogService = blogService;
            _blogService = new BlogService(null);
            blogList = new ObservableCollection<BlogListItemResponseModel>();
            InitData();
        }
        async void InitData()
        {
            var BlogList = await GetDataAsync();
            foreach (var item in BlogList)
            {
                blogList.Add(item);
            }
        }
        async Task<List<BlogListItemResponseModel>> GetDataAsync()
        {
            return await _blogService.GetBlogListAsync(new BlogListRequestModel() { PageIndex = this.pageIndex, PageSize = pageSize });  
        }

        [ObservableProperty]
        public ObservableCollection<BlogListItemResponseModel> blogList;
        [ObservableProperty]
        public int pageIndex = 1;
        [ObservableProperty]
        public int pageSize= 8;
        [RelayCommand]
        async Task NextPageData()
        {
            this.pageIndex++;

            var nextData =await GetDataAsync();
            foreach (var item in nextData)
            {
                blogList.Add(item);
            }
        }
        [RelayCommand]
        async Task ItemClick(BlogListItemResponseModel blog)
        {
            await Shell.Current.GoToAsync($"{nameof(BlogDetailPage)}?Id={blog.Id}");
        }
    }
}
