﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Order
{
    public class OrderItem :BaseEntity
    {
        public ProductItemOrdered Product {  get; set; }
        public decimal Cost { get; set; }
        public int Quantity { get; set; }
        public OrderItem()
        {
            
        }
        public OrderItem(ProductItemOrdered product, decimal cost, int quantity)
        {
            Product = product;
            Cost = cost;
            Quantity = quantity;
        }

    }
}
