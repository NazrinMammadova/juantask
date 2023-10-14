using juan.DAL;
using Microsoft.AspNetCore.Mvc;

namespace juan.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var blogs = _context.Blogs.ToList();
            return View(blogs);
        }
    }
}
