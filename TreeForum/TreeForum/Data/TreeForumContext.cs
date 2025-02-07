using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TreeForum.Models;

namespace TreeForum.Data
{
    public class TreeForumContext : DbContext
    {
        public TreeForumContext (DbContextOptions<TreeForumContext> options)
            : base(options)
        {
        }

        public DbSet<TreeForum.Models.Discussion> Discussion { get; set; } = default!;
        public DbSet<TreeForum.Models.Comment> Comment { get; set; } = default!;
    }
}
