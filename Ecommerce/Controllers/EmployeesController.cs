using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Repository.SpecificationClass;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IGenericRepository<Employees> genericRepository;

        public EmployeesController(IGenericRepository<Employees> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmps()
        {
            var spes = new EmployeesSpecification();
            var Employees = await genericRepository.GetAllWithSpesAsync(spes);
            return Ok(Employees);
               
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var spes = new EmployeesSpecification(id);
            var employee = await genericRepository.GetByIdWithSpesAsync(spes);
            return Ok(employee);
        }
    }
}
