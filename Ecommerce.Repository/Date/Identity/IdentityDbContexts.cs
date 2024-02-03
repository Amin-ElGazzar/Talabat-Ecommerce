using Ecommerce.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Date.Identity
{
    public class IdentityDbContexts : IdentityDbContext<ApplicationUser>
    {
        public IdentityDbContexts(DbContextOptions<IdentityDbContexts> options):base(options)
        {
            
        }
    }
}
