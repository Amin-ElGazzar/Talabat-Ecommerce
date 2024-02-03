using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Specification;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.SpecificationClass
{
    public static class SpecificationEvaluator<T> where T : BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> querycontextandentity, ISpecification<T> spes)
        {
            var Query = querycontextandentity;

            if (spes.Criteria is not null)
                Query = querycontextandentity.Where(spes.Criteria);

            if (spes.OrderBy is not null)
                Query=Query.OrderBy(spes.OrderBy);
            if (spes.OrderByDescending is not null)
                Query=Query.OrderByDescending(spes.OrderByDescending);


            if (spes.IsPaginationEnable)
                Query = Query.Skip(spes.Skip).Take(spes.Take);


            Query = spes.Include.Aggregate(Query, (currentQuery, includeQuery) => currentQuery.Include(includeQuery));

            return Query;
        }
    }
}
