using juan.DAL;
using juan.Models;
using juan.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace juan.Controllers
{
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BlogController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Detail(int?id)
        {
           if (id == null) return NotFound();
            var blog = _context.Blogs
                .Include(b=>b.Coments)
                .ThenInclude(c=>c.AppUser)
                
                .FirstOrDefault(b => b.Id == id);
            if (blog == null) return NotFound();
            BlogDetailVM blogDetailVM = new BlogDetailVM
            {
                blog = blog,
                blogs = _context.Blogs.OrderByDescending(b => b.Id).Take(4).ToList()

       

            };
            
            
            return View(blogDetailVM);
        }

        [HttpPost]
        public IActionResult AddComent(string message,int blogId)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("login", "account");
            var user = _userManager.FindByNameAsync(User.Identity.Name).Result;
            if (string.IsNullOrEmpty(message)) return NotFound();
            Coment newComent = new();
            newComent.Message = message;

            newComent.BlogId = blogId;
            newComent.AppUserId = user.Id;
            newComent.CreatedAt = DateTime.Now;

            _context.Coments.Add(newComent);
            _context.SaveChanges();
            return RedirectToAction("detail", new { Id = blogId });



        }
        public IActionResult DeleteComent(int? id)
        {
            if (id == null) return NotFound();
            var coment = _context.Coments.FirstOrDefault(c => c.Id == id);
            if (coment == null) return NotFound();
            _context.Coments.Remove(coment);
            _context.SaveChanges();
            return RedirectToAction("detail", new { Id=coment.BlogId });
        }
    }
}
