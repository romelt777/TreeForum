using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeForum.Data;
using TreeForum.Models;

namespace TreeForum.Controllers
{
    public class CommentsController : Controller
    {
        private readonly TreeForumContext _context;

        public CommentsController(TreeForumContext context)
        {
            _context = context;
        }

        //DELETED METHODS:
        // GET: Comments
        // GET: Comments/Details/5
        // GET: Comments/Edit/5
        // POST: Comments/Edit/5
        // GET: Comments/Delete/5
        // POST: Comments/Delete/5


        // GET: Comments/Create
        public IActionResult Create(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            //setting discussion ID for the comment
            ViewData["DiscussionId"] = id;
            ViewData["CreateDate"] = DateTime.Now;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();

                //redirect back to discussion page
                return RedirectToAction("GetDiscussion", "Home", new { id = comment.DiscussionId });
            }

            //setting discussion ID for the comment
            ViewData["DiscussionId"] = comment.DiscussionId;
            ViewData["CreateDate"] = DateTime.Now;


            return View(comment);
        }
    }
}
