﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities
{
    public class CustomerBasket
    {
       

        public string Id { get; set; }
        public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
        public CustomerBasket(string id)
        {
            Id = id;
        }
    }
}
