using Microsoft.AspNetCore.Identity;

namespace juan.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public List<Coment> Coments { get; set; }

    }
}
