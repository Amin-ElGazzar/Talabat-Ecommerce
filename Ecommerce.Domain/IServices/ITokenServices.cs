using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.IServices
{
    public interface ITokenServices
    {
        Task<string> CreateTokenAsync(ApplicationUser user ,UserManager<ApplicationUser> userManager);
    }
}
