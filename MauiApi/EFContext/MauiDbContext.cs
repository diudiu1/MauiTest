using MauiApi.Domain.Entities.Accounts;
using MauiApi.Domain.Entities.Blogs;
using MauiApi.Domain.Entities.Messages;
using MauiApi.Domain.Entities.Orders;
using MauiApi.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;

namespace MauiApi.EFContext
{
    public class MauiDbContext: DbContext
    {
        public MauiDbContext(DbContextOptions<MauiDbContext> options) : base(options)
        {
        }
        public DbSet<AccountInfo> AccountInfo { get; set; }
        public DbSet<BlogInfo> BlogInfo { get; set; }
        public DbSet<BlogComment> BlogComment { get; set; }
        public DbSet<MessageInfo> MessageInfo { get; set; }
        public DbSet<OrderInfo> OrderInfo { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<StoreInfo> StoreInfo { get; set; }
        public DbSet<ProductInfo> ProductInfo { get; set; }
        public DbSet<ProductCategory> ProductCategory { get; set; }
        public DbSet<ProductBanner> ProductBanner { get; set; }
        public DbSet<ProductComment> ProductComment { get; set; }
    }
}
