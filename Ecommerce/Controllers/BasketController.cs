using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Dtos;
using Ecommerce.Errors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;

        public BasketController(IBasketRepository basketRepository,IMapper mapper)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerBasket>> GetBasketAsync(string id) 
        {
            var basket =await basketRepository.GetBasketAsync(id);
            return basket ?? new CustomerBasket(id);
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasketAsync(CustomerBasketDto basket)
        {
            var mappedBasket = mapper.Map<CustomerBasketDto, CustomerBasket>(basket);

            var updateOrCreated = await basketRepository.UpdateBasketAsync(mappedBasket);
            if (updateOrCreated is null)
            {
                return BadRequest(new ApiResponse(400));
            }
            return updateOrCreated;
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasketAsync(string id)
        {
            return await basketRepository.DeleteBasketAsync(id);
        }
    }
}
