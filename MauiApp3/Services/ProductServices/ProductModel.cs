﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp3.Services.ProductServices
{
    public class ProductListItemResponseModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImageUrl { get; set; }
        public string Body { get; set; }
        /// <summary>
        /// 1 图片 2 视频
        /// </summary>
        public int Type { get; set; }
        public List<string> ImageUrls { get; set; }
        public string VideoUrl { get; set; }
        public DateTime CreateTime { get; set; }
    }
    public class ProductListRequestModel
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
    public class ProductNextRequestModel
    {
        public string CurrentId { get; set; }
        /// <summary>
        /// 1 上一个  2 下一个
        /// </summary>
        public int Action { get; set; }
    }
}
