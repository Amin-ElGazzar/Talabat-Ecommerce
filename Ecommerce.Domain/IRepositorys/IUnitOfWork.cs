using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.IRepositorys
{
    public interface IUnitOfWork :IAsyncDisposable
    {
       IGenericRepository<T>? repository<T>() where T :BaseEntity ;
        Task<int> Save();
    }
}
