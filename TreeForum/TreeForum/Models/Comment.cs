using System.ComponentModel.DataAnnotations;
using TreeForum.Data;

namespace TreeForum.Models
{
    public class Comment
    {
        //primary key
        public int CommentId { get; set; }

        [Display(Name = "Comment")]
        public string Content { get; set; } = string.Empty;
        public DateTime CreateDate { get; set; } = DateTime.Now;

        //foreign key
        public int DiscussionId { get; set; }

        // Navigation property
        public Discussion? Discussion { get; set; } //nullable

        //application User (foreign key), aspnetusers table
        public string ApplicationUserId { get; set; } = string.Empty;

        //navigation property
        public ApplicationUser? ApplicationUser { get; set; } //nullable
    }
}
