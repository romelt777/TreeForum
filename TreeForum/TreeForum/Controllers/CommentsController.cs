﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
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

    public class CommentsController : Controller
    {
        private readonly TreeForumContext _context;
        //application user
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentsController(TreeForumContext context, UserManager<ApplicationUser> userManager )
        {
            _context = context;
            _userManager = userManager;
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

            //get user id
            var userId = _userManager.GetUserId(User);

            //setting discussion ID for the comment
            ViewData["DiscussionId"] = id;
            ViewData["CreateDate"] = DateTime.Now;
            ViewData["ApplicationUserId"] = userId;

            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentId,Content,CreateDate,DiscussionId")] Comment comment)
        {
            //get id of user logged in
            var userId = _userManager.GetUserId(User);
            comment.ApplicationUserId = userId;

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
            ViewData["ApplicationUserId"] = userId;


            return View(comment);
        }
    }
}
