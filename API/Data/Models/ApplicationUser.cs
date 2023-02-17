using Microsoft.AspNetCore.Identity;

namespace FilmFlow.Server.Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsKioskUser { get; set; }
    }
}