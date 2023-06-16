using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Beauty.Shared.Models
{
    public class ShoppingCart
    {
        public ShoppingCart() 
        {
            Count = 1;
        }
        [BindNever]
        public long ShoppingCartId { get; set; }
        [ForeignKey("ProductId")]
        [ValidateNever]
        public Product Product { get; set; }
        [Range(1, 100, ErrorMessage = " Please select a count between 1 and 100")]
        public int Count { get; set; }
        public string UserId { get; set; }

    }
}
