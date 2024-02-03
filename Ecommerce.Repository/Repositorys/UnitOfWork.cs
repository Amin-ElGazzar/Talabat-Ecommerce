using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Entities.Order;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Repository.Date;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositorys
{
    public class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories;

        private readonly StoreContext context;

      
        public UnitOfWork(StoreContext context)
        {
            this.context = context;

        }
        public IGenericRepository<T>? repository<T>() where T : BaseEntity
        {
            if (repositories is null)
                repositories = new Hashtable();
            var type = typeof(T).Name;
            if (!repositories.ContainsKey(type))
            {
                var repository = new GenericRepository<T>(context);
                repositories.Add(type, repository);
            }
            return repositories as IGenericRepository<T>;
        }

        public ValueTask DisposeAsync()
        {
         return   context.DisposeAsync();
        }

        public async Task<int> Save()
        {
          return await context.SaveChangesAsync();
        }
    }
}
