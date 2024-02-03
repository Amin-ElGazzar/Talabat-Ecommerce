using Ecommerce.Domain.Entities;
using Ecommerce.Domain.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.SpecificationClass
{
    public class ProductWithFiltrationForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltrationForCountSpecification(SpecificationParameter parameter) :
            base(p => 
            (string.IsNullOrEmpty(parameter.Search) || p.Name.ToLower().Contains(parameter.Search)) &&
            (!parameter.BrandId.HasValue || p.ProductBrandId == parameter.BrandId) &&
            (!parameter.TypeId.HasValue || p.ProductTypeId == parameter.TypeId))

        {

        }
    }
}
