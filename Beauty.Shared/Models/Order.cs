using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty.Shared.Models
{
    public class Order
    {
        [BindNever]
        public long OrderId { get; set; }
        [BindNever]
        public ICollection<ShoppingCart> Lines { get; set; }
        [Required(ErrorMessage = "Please Enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Pick Up Date")]
        [NotMapped]
        [Display(Name="Pick Up Date")]
        public DateTime PickUpDate { get; set; }
        public string Comments { get; set; }
        public string Status { get; set; }
        [Display(Name = "Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }

    }
}
