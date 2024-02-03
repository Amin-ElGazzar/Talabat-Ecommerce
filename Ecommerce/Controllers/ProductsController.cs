using AutoMapper;
using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Domain.Specification;
using Ecommerce.Dtos;
using Ecommerce.Errors;
using Ecommerce.Helpers;
using Ecommerce.Repository.SpecificationClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IGenericRepository<Product> genericRepository;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;
        private readonly IMapper mapper;

        public ProductsController
            (
            IGenericRepository<Product> genericRepository,
            IGenericRepository<ProductBrand> brandRepo,
            IGenericRepository<ProductType> typeRepo,
            IMapper mapper
            )
        {
            this.genericRepository = genericRepository;
            this.brandRepo = brandRepo;
            this.typeRepo = typeRepo;
            this.mapper = mapper;
        }
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<PaginationGetAllResponse<ProductToreturnDto>>> GetAllProducts([FromQuery]SpecificationParameter parameter)
        {
            var spes = new ProductWithBrandAndTypeSpecification(parameter);
            var products = await genericRepository.GetAllWithSpesAsync(spes);
            var data = mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToreturnDto>>(products);
            var countSpecification = new ProductWithFiltrationForCountSpecification(parameter);
            var count = await genericRepository.GetCountWithspesAsync(countSpecification);
            return Ok(new PaginationGetAllResponse<ProductToreturnDto>(parameter.PageSize,parameter.PageIndex,count,data));
        }

        [ProducesResponseType(typeof(ProductToreturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToreturnDto>> GetProductById(int id)
        {
            var spes = new ProductWithBrandAndTypeSpecification(id);
            var product = await genericRepository.GetByIdWithSpesAsync(spes);
            if (product == null) return NotFound(new ApiResponse(404));
            return Ok(mapper.Map<Product, ProductToreturnDto>(product));
        }

        [HttpGet("brand")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands =await brandRepo.GetAllAsync();
            return Ok(brands);
        }

        [HttpGet("type")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllType()
        {
            var type = await typeRepo.GetAllAsync();
            return Ok(type);
        }
    }
}
