using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeForum.Data;
using TreeForum.Models;

namespace TreeForum.Controllers
{
    [Authorize]
    public class DiscussionsController : Controller
    {
        private readonly TreeForumContext _context;
        //application user
        private readonly UserManager<ApplicationUser> _userManager;

        private List<Discussion> discussions = new List<Discussion>();


        public DiscussionsController(TreeForumContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        //DELETED METHODS:
        // GET: Discussions/Details/5


        // GET: Discussions
        public async Task<IActionResult> Index()
        {
            //get id of user logged in
            var userId = _userManager.GetUserId(User);


            //getting discussions from database, saving to list
            discussions = await _context.Discussion
                .Where(d => d.ApplicationUserId == userId) //only discussions created by user
                .Include("Comments").ToListAsync();

            //sorting list from new to old
            return View(discussions.OrderByDescending(t => t.CreateDate).ToList());
        }

        // GET: Discussions/Create
        public IActionResult Create()
        {
            return View();
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

            //set the user id to the logged in user
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

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await discussion.ImageFile.CopyToAsync(fileStream);
                    }
                }


                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get id of user logged in
            var userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Include("Comments")
                .Where(d => d.ApplicationUserId == userId) //only discussions created by user
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }
            return View(discussion);
        }

        // POST: Discussions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiscussionId,Title,Content,ImageFilename,CreateDate,ApplicationUserId")] Discussion discussion)
        {
            if (id != discussion.DiscussionId)
            {
                return NotFound();
            }
            if(discussion.ImageFilename == null)
            {
                discussion.ImageFilename = "default.jpg";
            }

            Console.WriteLine(ModelState);

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(discussion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiscussionExists(discussion.DiscussionId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(discussion);
        }

        // GET: Discussions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //get id of user logged in
            var userId = _userManager.GetUserId(User);


            var discussion = await _context.Discussion
                .Where(d => d.ApplicationUserId == userId) //only discussions created by user
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion == null)
            {
                return NotFound();
            }

            return View(discussion);
        }

        // POST: Discussions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //get id of user logged in
            var userId = _userManager.GetUserId(User);

            var discussion = await _context.Discussion
                .Where(d => d.ApplicationUserId == userId) //only discussions created by user
                .FirstOrDefaultAsync(m => m.DiscussionId == id);

            if (discussion != null)
            {
                _context.Discussion.Remove(discussion);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DiscussionExists(int id)
        {
            return _context.Discussion.Any(e => e.DiscussionId == id);
        }
    }
}
