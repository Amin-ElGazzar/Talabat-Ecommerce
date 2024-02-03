using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.IRepositorys
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> GetAllWithSpesAsync(ISpecification<T> spes);

        Task<T> GetByIdWithSpesAsync(ISpecification<T> spes);

        Task <int> GetCountWithspesAsync(ISpecification<T> spes);

        Task add(T entity);
        void Update (T entity);
        void Delete (T entity);

    }
}
