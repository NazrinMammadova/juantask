using juan.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace juan.DAL
{

    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {


        }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Coment> Coments    { get; set; }
    }
}
