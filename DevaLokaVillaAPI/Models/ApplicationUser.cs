using Microsoft.AspNetCore.Identity;

namespace DevaLokaVillaAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string name { get; set; }
    }
}
