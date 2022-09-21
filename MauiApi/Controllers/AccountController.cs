using MauiApi.EFContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MauiApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly JWTConfig _jwtConfig;
        private MauiDbContext _context;
        public AccountController(ILogger<AccountController> logger, MauiDbContext context,IOptions<JWTConfig> options)
        {
            _logger = logger;
            _context = context;
            _jwtConfig = options.Value;
        }

        [HttpPost("/auth/login")]
        public async Task<LoginResponseModel> Login(LoginRequestModel login)
        {
            var account =await _context.AccountInfo.Where(a => a.UserName == login.UserName).FirstOrDefaultAsync();
            if (account!=null)
            {
                if (account.CkeckPassword(login.Password))
                {
                    var claims = new[]
                     {
                        new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                        new Claim("Id", account.Id),
                    };

                    var token = GenerateJWT(claims);
                    return new LoginResponseModel()
                    {
                        Token = token,
                        ExpiredTime= _jwtConfig.Expired,
                    };
                }
            }

            return null;
        }

        [HttpGet("/auth/test")]
        [Authorize]
        public LoginResponseModel Test()
        {
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
}