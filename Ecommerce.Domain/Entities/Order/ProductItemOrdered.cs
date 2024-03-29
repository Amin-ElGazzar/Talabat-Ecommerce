﻿namespace Ecommerce.Domain.Entities.Order
{
    public class ProductItemOrdered
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }

        public ProductItemOrdered()
        {
            
        }
        public ProductItemOrdered(int productId, string productName, string pictureUrl)
        {
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

    }
}