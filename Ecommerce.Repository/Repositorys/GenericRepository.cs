using Ecommerce.Domain.Entities;
using Ecommerce.Domain.IRepositorys;
using Ecommerce.Domain.Specification;
using Ecommerce.Repository.Date;
using Ecommerce.Repository.SpecificationClass;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Repositorys
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext context;

        public GenericRepository(StoreContext context)
        {
            this.context = context;
        }

      

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            //if (typeof(T) == (typeof(Product)))
            //    return (IEnumerable<T>)await context.Products.Include(p=>p.ProductType).Include(p=>p.ProductBrand).ToListAsync();
            //else
                return await context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpesAsync(ISpecification<T> spes)
        {
            return await ApplySpecification(spes).ToListAsync();
        } 

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
         
        }

        public async Task<T> GetByIdWithSpesAsync(ISpecification<T> spes)
        {
            return await ApplySpecification(spes).FirstOrDefaultAsync();
        }

        public async Task<int> GetCountWithspesAsync(ISpecification<T> spes)
        {
           return await ApplySpecification(spes).CountAsync();
        }
        private IQueryable<T> ApplySpecification(ISpecification<T> spes)
        {
            return SpecificationEvaluator<T>.GetQuery(context.Set<T>(), spes);
        }
        public async Task add(T entity)
             => await context.Set<T>().AddAsync(entity);
      

      
        public void Update(T entity)
        {
          context.Set<T>().Update(entity);
        }
        public void Delete(T entity)
        {
           context.Set<T>().Remove(entity);
        }

       

    }
}
