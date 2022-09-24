using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiApp3.Configs;
using MauiApp3.Services.BlogServices;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MauiApp3.ViewModels
{
    [QueryProperty("Id", "Id")]
    public partial class BlogDetailPageViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IBlogService _blogService;
        public BlogDetailPageViewModel(IBlogService blogService)
        {
            _blogService = blogService;
            isImage = true;
            IsVedio = false;
            imageList = new ObservableCollection<string>();
            //_blogService = new BlogService(null);
            //title = "0000000";
            //InitData();  不能在此处初始化，页面传值Query 还没有生效
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.ContainsKey("Id"))
            {
                var value = query["Id"].ToString();
                await InitData(value);
            }
        }
        public async Task InitData(string id)
        {
            this.id = id;
            blog = await _blogService.GetBlogAsync(this.id);
            if (blog!=null)
            {
                title = blog.Title;
                content=blog.Content;
                var list= blog.ImageUrls.Split(',').Select(a=> Appsettings.BaseAddress +a).ToList();
                foreach (var item in list)
                {
                    imageList.Add(item);
                }
                
                this.OnPropertyChanged("Title");
                this.OnPropertyChanged("Id");
                this.OnPropertyChanged("ImageList");
                this.OnPropertyChanged("Content");
            }
        }
        

        [ObservableProperty]
        public BlogListItemResponseModel blog;
        [ObservableProperty]
        public string id;
        [ObservableProperty]
        public string content;
        
        [ObservableProperty]
        public string title;
        public int Type;
        [ObservableProperty]
        public bool isVedio=false;
        [ObservableProperty]
        public bool isImage = false;
        [ObservableProperty]
        public int imageIndex;
        [ObservableProperty]
        public ObservableCollection<string> imageList;
        [RelayCommand]
        async Task GoBack(object par)
        {
            await Shell.Current.GoToAsync("..");
        }
        [RelayCommand]
        void NextImage()
        {
            imageIndex++;
            if (imageIndex>= ImageList.Count)
            {
                imageIndex = 0;
            }
            GetImage();
        }
        [RelayCommand]
        void PreviousImage()
        {
            imageIndex--;
            if (imageIndex <0)
            {
                imageIndex = ImageList.Count-1;
            }
            GetImage();
        }
        private void GetImage()
        {
            //imageUrl = ImageList[imageIndex];
            //this.SetProperty(ref imageUrl, ImageList[imageIndex], "ImageUrl");
        }
    }
}
