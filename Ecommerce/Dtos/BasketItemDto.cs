﻿using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Dtos
{
    public class BasketItemDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.1,double.MaxValue,ErrorMessage ="Price must be greater than zero")]
        public decimal Price { get; set; }
         [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Quantity must be one item at least ")]
        public int Quantity { get; set; }
        [Required]
     
        public string PictureUrl { get; set; }
         [Required]
        public string Brand { get; set; }
         [Required]
        public string Type { get; set; }
    }
}