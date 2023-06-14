using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Shared.Models
{
    public class Feedback
    {
        [BindNever]
        public long FeedbackId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Author { get; set; }
        public string ImageUrl { get; set; }
        [Range(0, 5, ErrorMessage = "Rate should be between 0-5")]
        public double Rate { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public IdentityUser User { get; set; }
    }
}
