using MauiApi.Domain.Entities.Accounts;
using MauiApi.Domain.Entities.Blogs;
using MauiApi.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace MauiApi.EFContext
{
    public class MauiDbInitData
    {
        private readonly MauiDbContext _context;
        public MauiDbInitData(MauiDbContext context)
        {
            _context = context;
        }
        public async Task InitDataAsync()
        {
            await InitAccount();
            await InitBlog();

            await InitProduct();

            await _context.SaveChangesAsync();
        }
        private List<AccountInfo> Accounts=new List<AccountInfo>();
        private async Task InitAccount()
        {
            if (await _context.AccountInfo.AnyAsync())
            {
                return;
            }
            var accountList = new List<AccountInfoAddDto>();
            for (int i = 0; i < 10; i++)
            {
                accountList.Add(new AccountInfoAddDto()
                {
                    Name = "张" + i.ToString(),
                    UserName = "user" + i.ToString(),
                    Password = "123456"
                });
            }
            foreach (var item in accountList)
            {
                var account = new AccountInfo()
                {
                    Name = item.Name,
                    UserName = item.UserName,
                    AvatarUrl= "/images/avatar1.jpg"
                };
                account.CreatePassword(item.Password);
                Accounts.Add(account);
                await _context.AccountInfo.AddAsync(account);
            }
           
        }
        private async Task InitBlog()
        {
            if (await _context.BlogInfo.AnyAsync())
            {
                return;
            }
            for (int i = 0; i < 100; i++)
            {
                var account = Accounts[i % 10];
                var blog = new BlogInfo()
                {
                    AccountId = account.Id,
                    Content = "BlogContent" + i,
                    CoverImageUrl = "/images/blog1.png",
                    ImageUrls = "/images/blog1.png,/images/blog2.png",
                    Title = "BlogTitle" + i,
                    Type = 1,
                    VideoUrl = "/images/blog1.mp4",
                };
                var blogComment1 = new BlogComment()
                {
                    AccountId = Accounts[0].Id,
                    BlogId = blog.Id,
                    Content= "blogComment" + blog.Title + i,
                };
                var blogComment2 = new BlogComment()
                {
                    AccountId = Accounts[1].Id,
                    BlogId = blog.Id,
                    Content = "blogComment" + blog.Title + i,

                };
                await _context.BlogInfo.AddAsync(blog);
                await _context.BlogComment.AddAsync(blogComment1);
                await _context.BlogComment.AddAsync(blogComment2);
            } 
        }
        private async Task InitProduct()
        {
            if (await _context.StoreInfo.AnyAsync())
            {
                return;
            }
            var store1 = new StoreInfo()
            {
                AccountId = Accounts[0].Id,
                Name = Accounts[0].Name + "的百年老店",
                Content = Accounts[0].Name + "的百年老店介绍",
            };
            var store2 = new StoreInfo()
            {
                AccountId = Accounts[1].Id,
                Name = Accounts[1].Name + "的百年花店",
                Content = Accounts[1].Name + "的百年花店介绍",
            };
            await _context.StoreInfo.AddAsync(store1);
            await _context.StoreInfo.AddAsync(store2);

            var productCategoryList = new List<ProductCategory>();
            for (int i = 0; i < 4; i++)
            {

                var productCategory1 = new ProductCategory()
                {
                    AccountId = store1.AccountId,
                    Name = "分类" + i,
                    StoreId = store1.AccountId,
                    
                };
                var productCategory2 = new ProductCategory()
                {
                    AccountId = store2.AccountId,
                    Name = "分类" + i,
                    StoreId = store2.AccountId,
                };
                productCategoryList.Add(productCategory1);
                productCategoryList.Add(productCategory2);
                await _context.ProductCategory.AddAsync(productCategory1);
                await _context.ProductCategory.AddAsync(productCategory2);
            }
            for (int i = 0; i < 80; i++)
            {
                var productCategory = productCategoryList[i%8];
                var store = productCategory.StoreId == store1.Id ? store1 : store2;
                var product = new ProductInfo()
                {
                    CategoryId= productCategory.Id,
                    Content = "BlogContent" + i,
                    CoverImageUrl = "/images/product1.png",
                    Name= productCategory.Name+"_product"+i,
                    StoreId= store.Id,
                    StoreName= store1.Name,
                };
                await _context.ProductInfo.AddAsync(product);

                for (int j = 0; j < 4; j++)
                {
                    var productBanner = new ProductBanner()
                    {
                        ProductId = product.Id,
                        BannerType = 1,
                        FileUrl = "/images/product1.png",
                    };
                    var productComment = new ProductComment()
                    {
                        AccountId = Accounts[i%2].Id,
                        Content = product.Name + "_comment"+i,
                        ProductId = product.Id,

                    };
                    await _context.ProductBanner.AddAsync(productBanner);
                    await _context.ProductComment.AddAsync(productComment);
                }
            }
        }
    }
    public class AccountInfoAddDto
    { 
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
