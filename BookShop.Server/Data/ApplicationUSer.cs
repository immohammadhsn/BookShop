using Microsoft.AspNetCore.Identity;

namespace BookShop.Server.Data
{
    public class ApplicationUser:IdentityUser
    {
        public string DisplayedName { get; set; }
    }
}