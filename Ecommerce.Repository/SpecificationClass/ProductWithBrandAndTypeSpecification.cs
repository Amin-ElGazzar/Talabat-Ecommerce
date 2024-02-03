using Ecommerce.Domain.Entities;
using Ecommerce.Repository.SpecificationClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Domain.Specification
{
    public class ProductWithBrandAndTypeSpecification : BaseSpecification<Product>
    {
        public ProductWithBrandAndTypeSpecification(SpecificationParameter parameter)
            : base
            (
                 p =>
                     (string.IsNullOrEmpty(parameter.Search) || p.Name.ToLower().Contains(parameter.Search))&&
                     (!parameter.BrandId.HasValue || p.ProductBrandId == parameter.BrandId)&& 
                     (!parameter.TypeId.HasValue || p.ProductTypeId == parameter.TypeId)
            )
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);

            OrderBy = p => p.Name;
            if (!string.IsNullOrEmpty(parameter.Sort))
            {
                switch (parameter.Sort)
                {
                    case "priceAsc":
                        OrderBy = p => p.Price;
                        break;

                    case "priceDes":
                        OrderByDescending = p => p.Price;
                        break;
                    default: OrderBy = p => p.Name; break;

                }

            }

            ApplyPagination(parameter.PageSize*(parameter.PageIndex -1),parameter.PageSize);

        }

        public ProductWithBrandAndTypeSpecification(int id) : base(P => P.Id == id)
        {
            Include.Add(P => P.ProductBrand);
            Include.Add(P => P.ProductType);
        }

    }
}
