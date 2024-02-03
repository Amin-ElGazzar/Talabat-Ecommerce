using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepositorys;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly IGenericRepository<Departments> genericRepository;

        public DepartmentsController(IGenericRepository<Departments> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

      

    }
}
