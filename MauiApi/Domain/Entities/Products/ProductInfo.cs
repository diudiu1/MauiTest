using MauiApi.Domain.Base;

namespace MauiApi.Domain.Entities.Products
{
    public class ProductInfo : EntityBase
    {
        public string StoreId { get; set; } = null!;
        public string StoreName { get; set; } = null!;
        public string CategoryId { get; set; } = null!;
        public string Name { get; set; } = null!;
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImageUrl { get; set; } = null!;
        public string Content { get; set; } = null!;

    }
    public class ProductBanner : EntityBase
    {
        public string ProductId { get; set; } = null!;
        public string FileUrl { get; set; } = null!;
        public string? Remark { get; set; }
        /// <summary>
        /// 1图片 2视频
        /// </summary>
        public int BannerType { get; set; }
    }
    public class ProductComment : EntityBase
    {
        public string ProductId { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ParentId { get; set; }
    }
    public class StoreInfo : EntityBase
    {
        public string AccountId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Content { get; set; } = null!;
    }
    public class ProductCategory : EntityBase
    {
        public string StoreId { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string? ParentId { get; set; }
        public string Name { get; set; } = null!;
        public string? Reamrk { get; set; }

    }
}
