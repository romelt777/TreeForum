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

        //GetDiscussion : gets 1 discussion and all comments with discussion
        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           

            var discussion = await _context.Discussion.Include("Comments").FirstOrDefaultAsync(m => m.DiscussionId == id);

            Console.WriteLine(discussion.DiscussionId);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);

            //return View()
        }
    }
}
