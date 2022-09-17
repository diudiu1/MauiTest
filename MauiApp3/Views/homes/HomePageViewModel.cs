using MauiApp3.Services.BlogServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Views.homes
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly IBlogService _blogService;
        public HomePageViewModel()
        {
            //_blogService = blogService;
            _blogService = new BlogService(null);

            Monkeys = new List<Monkey> {
                new Monkey(){ Name="111",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="222",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="333",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="444",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="555",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="666",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="777",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="888",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="999",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="100",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="101",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="102",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="103",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="104",ImageUrl="",Location="11111111111111111"},
                new Monkey(){ Name="105",ImageUrl="",Location="11111111111111111"},
            };
        //InitData();
    }
        async void InitData()
        {
            BlogList = await _blogService.GetBlogListAsync(new BlogListRequestModel() { PageIndex = pageIndex, PageSize = pageSize });

        }
        public List<BlogListItemResponseModel> BlogList;
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 2;
        public Command NextPageDataCommand { get; set; } = new Command(() =>
        {

            //_blogService.GetBlogListAsync(new BlogListRequestModel() { PageIndex = pageIndex, PageSize = pageSize });
        });
        public List<Monkey> Monkeys { get; set; }
        
    }
    public class Monkey
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Location { get; set; }
    }
}
