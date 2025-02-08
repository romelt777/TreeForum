using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeForum.Data;
using TreeForum.Models;

namespace TreeForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly TreeForumContext _context;

        private List<Discussion> discussions = new List<Discussion>();

        public HomeController(TreeForumContext context)
        {
            _context = context;
        }

        //home page with all discussions
        public async Task<IActionResult> Index()
        {
            //getting discussions from database, saving to list
            discussions = await _context.Discussion.ToListAsync();
            return View(discussions);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
