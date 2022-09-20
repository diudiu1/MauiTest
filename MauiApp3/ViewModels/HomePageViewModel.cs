using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.ViewModels
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
        [ObservableProperty]
        public bool isRefreshing=false;
        /// <summary>
        /// 下一页
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// 下拉刷新
        /// </summary>
        /// <returns></returns>
        [RelayCommand]
        async Task Refresh()
        {
            //isRefreshing = true;
            this.pageIndex=Random.Shared.Next(10);//随机一个分页数
            //清空数据
            blogList.Clear();
            var nextData = await GetDataAsync();
            foreach (var item in nextData)
            {
                blogList.Add(item);
            }
            isRefreshing = false;

            this.OnPropertyChanged("IsRefreshing");
        }
    }
}
