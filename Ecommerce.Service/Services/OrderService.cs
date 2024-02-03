using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Order;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Domain.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository basketRepository;
        private readonly IUnitOfWork unitOfWork;



        public OrderService(
            IBasketRepository basketRepository,
            IUnitOfWork unitOfWork

            )
        {
            this.basketRepository = basketRepository;
            this.unitOfWork = unitOfWork;

        }
        public async Task<Order?> CreateOrderAsync(string buyerEmail, string basketId, int deliveryMethodid, Address shippingAddress)
        {
            // basket 
            var basket = await basketRepository.GetBasketAsync(basketId);
            // Order items in basket

            var orderItems = new List<OrderItem>();
            if (basket?.BasketItems.Count() > 0)
            {
                foreach (var item in basket.BasketItems)
                {
                    var productRepo = unitOfWork.repository<Product>();
                    if (productRepo != null)
                    {
                        var product = await productRepo.GetByIdAsync(item.Id);
                        if (product != null)
                        {
                            var productItemOrder = new ProductItemOrdered(product.Id, product.Name, product.PictureUrl);


                            var orderItem = new OrderItem(productItemOrder, product.Price, item.Quantity);

                            orderItems.Add(orderItem);
                        }
                    }
                }
            }

            // delivery method
            DeliveryOrderMethod deliveryMethod=new DeliveryOrderMethod();
            var deliveryRepository = unitOfWork.repository<DeliveryOrderMethod>();
            if (deliveryRepository != null) { 
                   deliveryMethod = await deliveryRepository.GetByIdAsync(deliveryMethodid);
            }
            // subTotal
            var subTotal = orderItems.Sum(I => I.Cost * I.Quantity);

            // order
            var order = new Order(buyerEmail, shippingAddress, deliveryMethod, orderItems, subTotal);

            var orderRepository = unitOfWork.repository<Order>();
            if (orderRepository != null) {
                await orderRepository.add(order);

                var result = unitOfWork.Save();
                if (result != null) 
                   return order;
            }
            return null;
        }

        public Task<Order> GetOrderByIdForUserAsync(string buyerEmail, string OrderId)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<Order>> GetOrderForUserAsync(string buyerEmail)
        {
            throw new NotImplementedException();
        }
    }
}
