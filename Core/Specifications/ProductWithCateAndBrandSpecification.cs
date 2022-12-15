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
        public ProductWithCateAndBrandSpecification()
        {
            AddIncludes(x => x.Brand);
            AddIncludes(x => x.Category);
        }

        public ProductWithCateAndBrandSpecification(int id) : base(x => x.Id ==id)
        {
            AddIncludes(x => x.Brand);
            AddIncludes(x => x.Category);
        }
    }
}