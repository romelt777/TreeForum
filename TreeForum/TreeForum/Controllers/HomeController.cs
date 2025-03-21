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
            var discussions = await _context.Discussion
                .Include(d => d.ApplicationUser)  //User for each discussion
                .Include(d => d.Comments)        //comments associated with each discussion
                .ToListAsync();
                        
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

            //getting the discussion (without comments and application user)
            var discussion = await _context.Discussion
                .Include(d => d.ApplicationUser) //load the applicationUser for the discussion
                .FirstOrDefaultAsync(m => m.DiscussionId == id);


            if (discussion == null)
            {
                return NotFound();
            }

            //loading the comments with their associated ApplicationUser
            await _context.Entry(discussion)
                .Collection(d => d.Comments)
                .Query()
                .Include(c => c.ApplicationUser) //load the applicationUser for each comment
                .LoadAsync();

            //initialize comments (avoid null reference)
            discussion.Comments = discussion.Comments ?? new List<Comment>();
            discussion.Comments = discussion.Comments.OrderBy(c => c.CreateDate).ToList();

            return View(discussion);
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

        //Profile 
        public async Task<IActionResult> Profile(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            //getting user by name(handle)
            var userProfile = await _context.Users.FirstOrDefaultAsync(u => u.Name == name);

            if (userProfile == null)
            {
                return NotFound();
            }

            //getting the user discussions
            var userDiscussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == userProfile.Id)
                .ToListAsync();

            ViewData["ProfileUser"] = userProfile;
            ViewData["UserDiscussions"] = userDiscussions;

            return View();

        }
    }
}
