using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

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
        public string UserId { get; set; }
    }
}
