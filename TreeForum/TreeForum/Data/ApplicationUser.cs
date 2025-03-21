﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace TreeForum.Data
{
    public class ApplicationUser:IdentityUser
    {
        [PersonalData] // property is included in download of personal data.
        public required string Name { get; set; }

        [PersonalData]
        public string Location { get; set; } = string.Empty;

        public string ImageFilename { get; set; } = string.Empty;

        [NotMapped]
        [Display(Name = "Profile Picture")]
        public IFormFile? ImageFile { get; set; }
    }
}
