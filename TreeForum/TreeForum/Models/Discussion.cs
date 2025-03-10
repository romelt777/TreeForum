﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TreeForum.Data;

namespace TreeForum.Models
{
    public class Discussion
    {
        public int DiscussionId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        // Navigation property
        public List<Comment>? Comments { get; set; }  // nullable!!!

        //application User (foreign key), aspnetusers table
        public string ApplicationUserId { get; set; } = string.Empty;

        //navigation property
        public ApplicationUser? ApplicationUser { get; set; } //nullable
    }
}
