using MauiApi.Domain.Base;

namespace MauiApi.Domain.Entities.Blogs
{
    public class BlogInfo: EntityBase
    {
        public string AccountId { get; set; } = null!;
        public string Title { get; set; } = null!;
        /// <summary>
        /// 封面图
        /// </summary>
        public string CoverImageUrl { get; set; } = null!;
        public string Content { get; set; } = null!;
        /// <summary>
        /// 1 图片 2 视频
        /// </summary>
        public int Type { get; set; }
        public string? ImageUrls { get; set; }
        public string? VideoUrl { get; set; }
    }
    public class BlogComment : EntityBase
    {
        public string BlogId { get; set; } = null!;
        public string AccountId { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string? ParentId { get; set; }
    }
}
