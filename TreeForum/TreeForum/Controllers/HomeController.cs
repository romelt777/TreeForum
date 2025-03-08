using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeForum.Data;
using TreeForum.Models;

namespace TreeForum.Controllers
{
    public class HomeController : Controller
    {
        private readonly TreeForumContext _context;
        //application user
        private readonly UserManager<ApplicationUser> _userManager;

        private List<Discussion> discussions = new List<Discussion>();

        public HomeController(TreeForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //home page with all discussions
        public async Task<IActionResult> Index()
        {
            //getting discussions from database, saving to list
            discussions = await _context.Discussion.Include("Comments").Include("ApplicationUser").ToListAsync();

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

            //getting the discussion and associated comments
            var discussion = await _context.Discussion.Include("Comments").FirstOrDefaultAsync(m => m.DiscussionId == id);


            if (discussion == null)
            {
                return NotFound();
            }

            //sorting commments by date, need null check
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

        // GET: Discussions/Create
        public IActionResult Create()
        {
            //getting the user logged in
            var userId = _userManager.GetUserId(User);

            if(userId == null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View();

            }
        }

        // POST: Discussions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiscussionId,Title,Content,ImageFile,CreateDate")] Discussion discussion)
        {
            //setting the date created for discussion
            discussion.CreateDate = DateTime.Now;

            // save image as "default.jpg" if null 
            if (discussion.ImageFile == null)
            {
                discussion.ImageFilename = "default.jpg";
            }
            else
            {
                // rename the uploaded file to a guid (unique filename). Set before discussion saved in database.
                discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile?.FileName);
            }

            //getting the user logged in
            var userId = _userManager.GetUserId(User);
            discussion.ApplicationUserId = userId;

            if (ModelState.IsValid)
            {
                _context.Add(discussion);
                await _context.SaveChangesAsync();

                // Save the uploaded file after the discussion is saved in the database.
                //saves in project wwwroot
                if (discussion.ImageFile != null)
                {
                    //making findpath to save too.
                    //it is a relative path on different environments.
                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", discussion.ImageFilename);

                    // rename the uploaded file to a guid (unique filename). Set before discussion saved in database.
                    discussion.ImageFilename = Guid.NewGuid().ToString() + Path.GetExtension(discussion.ImageFile?.FileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }

                return RedirectToAction("GetDiscussion", new { id = discussion.DiscussionId });


            }
            return View(discussion);

        }
    }
}
