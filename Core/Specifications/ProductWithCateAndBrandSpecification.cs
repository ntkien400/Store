using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithCateAndBrandSpecification : BaseSpecification<Product>
    {
        public ProductWithCateAndBrandSpecification(ProductSpecParams productParams)
        : base(x =>
            (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
            (!productParams.BrandId.HasValue || x.BrandId == productParams.BrandId) &&
            (!productParams.CategoryId.HasValue || x.CategoryId == productParams.CategoryId)
        )
        {
            AddIncludes(x => x.Brand);
            AddIncludes(x => x.Category);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize, productParams.PageSize *(productParams.PageIndex -1));
            if(!string.IsNullOrEmpty(productParams.Sort))
            {
                switch(productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductWithCateAndBrandSpecification(int id) : base(x => x.Id ==id)
        {
            AddIncludes(x => x.Brand);
            AddIncludes(x => x.Category);
        }
    }
}