using System.Security.Claims;

namespace MauiApi
{
    public interface ICurrentUserService
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
    }
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            var user = _httpContextAccessor.HttpContext?.User;
            if (user != null)
            {
                Id = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.NameIdentifier)?.Value;
                Name = user.Claims.FirstOrDefault(a => a.Type == ClaimTypes.Name)?.Value;
                UserName = user.Claims.FirstOrDefault(a => a.Type == "UserName")?.Value;
            }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }

    }
}
