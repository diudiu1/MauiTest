using MauiApi.Domain.Base;
using System.Text;

namespace MauiApi.Domain.Entities.Accounts
{
    public class AccountInfo : EntityBase
    {
        public string Name { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string PasswordCode { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime? BirthdayTime { get; set; }
        public string? Phone { get; set; } = null!;
        public string? Email { get; set; } = null!;
        public string AvatarUrl { get; set; } = null!;
        public void CreatePassword(string password)
        {
            this.PasswordCode = Guid.NewGuid().ToString();
            this.PasswordHash = GetPasswordHash(password);
        }
        public bool CkeckPassword(string password)
        {
            return this.PasswordHash == GetPasswordHash(password);
        }
        private string GetPasswordHash(string password)
        {
            //简单写写
            var array = Encoding.ASCII.GetBytes(this.PasswordCode + password);
            return Convert.ToBase64String(array);
        }
    }
}
