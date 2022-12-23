using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFilterForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFilterForCountSpecification(ProductSpecParams productParams)
        : base(x =>
            (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
        )
        {
            
        }
    }
}