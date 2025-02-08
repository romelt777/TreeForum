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
            discussions = await _context.Discussion.Include("Comments").ToListAsync();

            //sorting list from new to old
            return View(discussions.OrderByDescending(t => t.CreateDate).ToList());
        }

        //GetDiscussion : gets 1 discussion and all comments with discussion
        public async Task<IActionResult> GetDiscussion(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           

            var discussion = await _context.Discussion.Include("Comments").FirstOrDefaultAsync(m => m.DiscussionId == id);


            if (discussion == null)
            {
                return NotFound();
            }


            if (discussion.Comments != null)
            {
                discussion.Comments = discussion.Comments.OrderByDescending(c => c.CreateDate).ToList();
                return View(discussion);
            }
            else
            {
                return View(discussion);
            }




        }
    }
}
