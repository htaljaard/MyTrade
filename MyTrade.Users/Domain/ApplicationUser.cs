using Microsoft.AspNetCore.Identity;

namespace MyTrade.Users.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}