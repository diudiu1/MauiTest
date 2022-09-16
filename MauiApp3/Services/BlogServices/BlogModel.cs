using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.BlogServices
{
    public class BlogListItemResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public List<string> ImageUrls { get; set; }
        public string VideoUrl { get; set; }
    }
    public class BlogListRequestModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class BlogNextRequestModel
    {
        public string CurrentId { get; set; }
        /// <summary>
        /// 1 上一个  2 下一个
        /// </summary>
        public int Action { get; set; }
    }
}
