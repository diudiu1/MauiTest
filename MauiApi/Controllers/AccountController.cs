using MauiApi.EFContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using static System.Collections.Specialized.BitVector32;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly JWTConfig _jwtConfig;
        private MauiDbContext _context;
        private ICurrentUserService _currentUser;
        private IWebHostEnvironment _webhostEnv;
        public AccountController(ILogger<AccountController> logger, MauiDbContext context,IOptions<JWTConfig> options, ICurrentUserService currentUser, IWebHostEnvironment webhostEnv)
        {
            _logger = logger;
            _context = context;
            _jwtConfig = options.Value;
            _currentUser = currentUser;
            _webhostEnv = webhostEnv;
        }

        [HttpPost("/auth/login")]
        [AllowAnonymous]
        public async Task<LoginResponseModel> Login(LoginRequestModel login)
        {
            var account =await _context.AccountInfo.Where(a => a.UserName == login.UserName).FirstOrDefaultAsync();
            if (account!=null)
            {
                if (account.CkeckPassword(login.Password))
                {
                    var claims = new[]
                     {
                        new Claim(JwtRegisteredClaimNames.Sub, account.Id),
                        new Claim(JwtRegisteredClaimNames.Name, account.Name),
                        new Claim("UserName", account.UserName),
                    };

                    var token = GenerateJWT(claims);
                    return new LoginResponseModel()
                    {
                        Token = token,
                        ExpiredTime= _jwtConfig.Expired,
                    };
                }
            }

            throw new Exception("");
        }
        [HttpGet("myinfo")]
        
        public async Task<AccountInfoResponseModel> Myinfo()
        {
            var account = await _context.AccountInfo.Where(a => a.UserName == _currentUser.UserName).FirstOrDefaultAsync();
            if (account != null)
            {
                return new AccountInfoResponseModel() {
                AvatarUrl = account.AvatarUrl,
                BirthdayTime = account.BirthdayTime,
                CreateTime = account.CreateTime,
                Email = account.Email,
                Id = account.Id,
                Name = account.Name,
                Phone = account.Phone,
                UserName = account.UserName,
                };
            }
            throw new Exception("");
        }
        [HttpPost("updateAvatar")]
        public async Task<AccountInfoResponseModel> UpdateAvatar(IFormFile file)
        {
            string[] fileNames = file.FileName.Split('.');
            var xiangduiPath = $"/images/{Guid.NewGuid()}.{fileNames[1]}";
            var fullPath = _webhostEnv.WebRootPath+ xiangduiPath;
            using (var stream = System.IO.File.Create(fullPath))
            {
                await file.CopyToAsync(stream);
            }
            var account = await _context.AccountInfo.Where(a => a.UserName == _currentUser.UserName).FirstOrDefaultAsync();
            if (account != null)
            {
                account.AvatarUrl = xiangduiPath;
                await _context.SaveChangesAsync();

                return new AccountInfoResponseModel() { AvatarUrl = account.AvatarUrl, };
            }
            throw new Exception("");
        }
        [HttpGet("/auth/test")]
        [Authorize]
        public LoginResponseModel Test()
        {
            string name = _currentUser.Name;
            return new LoginResponseModel() { Token="123456" };
        }
        private string GenerateJWT(Claim[] claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.IssuerSigningKey!));

            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _jwtConfig.ValidIssuer,                             //Issuer
                _jwtConfig.ValidAudience,                           //Audience
                claims,                                             //Claims,
                DateTime.Now,                                       //notBefore
                DateTime.Now.AddMinutes(_jwtConfig.Expired),        //expires
                signingCredentials
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            return jwtToken;
        }
    }
    public class LoginRequestModel
    { 
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginResponseModel
    {
        public string Token { get; set; }
        public long ExpiredTime { get; set; }
    }
    public class AccountInfoResponseModel
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime? BirthdayTime { get; set; }
        public string? Phone { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public DateTime CreateTime { get; set; }
    }
}