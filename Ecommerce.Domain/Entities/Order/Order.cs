using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Entities.Order
{
    public class Order:BaseEntity
    {
       

        public string BuyerEmail { get; set; }
        public Address ShippingAddress { get; set; }
        public DeliveryOrderMethod DeliveryOrderMethod { get; set;}
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public DateTimeOffset OrderDate { get; set; }= DateTimeOffset.Now;
        public ICollection<OrderItem> Items { get; set;}=new HashSet<OrderItem>();

        public decimal SubTotal { get; set; }
        public decimal GetTotal() => DeliveryOrderMethod.Cost + SubTotal; 
        public string PaymentIntentId { get; set; }

        public Order()
        {

        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryOrderMethod deliveryOrderMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryOrderMethod = deliveryOrderMethod;
            Items = items;
            SubTotal = subTotal;
        }
    }
}
