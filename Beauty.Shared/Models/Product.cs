using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Beauty.Shared.Models
{
    public class Product
    {
        [BindNever]
        public long ProductId { get; set; }
        [Required(ErrorMessage ="Please Enter product name")]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Please enter an image URL")]
        [DisplayName("Product Image")]
        public string ImgUrl { get; set; }
    }
}
