using MauiApi.EFContext;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace MauiApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            AddSwaggerGen(builder.Services);
            builder.Services.AddDbContext<MauiDbContext>(options => {
                if (builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    options.UseInMemoryDatabase("mauidb");
                }
                else
                {
                    string connection = builder.Configuration.GetConnectionString("mauidb");
                    var serverVersion = ServerVersion.AutoDetect(connection);
                    //var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
                    options.UseMySql(connection, serverVersion);
                }
            });

            
            

            builder.Services.AddScoped<MauiDbInitData>();

            var jwtConfigSection = builder.Configuration.GetSection("JWTConfig");
            var jwtConfig = new JWTConfig();
            jwtConfigSection.Bind(jwtConfig);
            builder.Services.Configure<JWTConfig>(jwtConfigSection);
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.ValidIssuer,
                    ValidAudience = jwtConfig.ValidAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.IssuerSigningKey!))
                };

                options.Events = new JwtBearerEvents()
                {
                    //认证失败自定义返回
                    OnChallenge = async context =>
                    {
                        //设置为已处理，就不会添加jwt默认的header
                        context.HandleResponse();
                        var obj = new
                        {
                            code = "401"
                        };
                        await context.Response.WriteAsJsonAsync(obj);
                    }
                };
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                if (!builder.Configuration.GetValue<bool>("UseInMemoryDatabase"))
                {
                    var context = scope.ServiceProvider.GetService<MauiDbContext>();
                    await context!.Database.MigrateAsync();
                }

                var initHandler = scope.ServiceProvider.GetService<MauiDbInitData>();
                await initHandler!.InitDataAsync();
            }
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        private static void AddSwaggerGen(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
                //添加Authorization
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new List<string>()
                    }
                });

                //List<Assembly> assemblies = new List<Assembly>() {typeof(Program).Assembly};

                //foreach (var item in assemblies)
                //{
                //    var xmlPath = Path.Combine(AppContext.BaseDirectory, $"{item.GetName().Name}.xml");
                //    // 启用xml注释. 该方法第二个参数启用控制器的注释，默认为false.
                //    c.IncludeXmlComments(xmlPath, true);
                //}
            });
        }
    }
}