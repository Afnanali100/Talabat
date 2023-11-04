using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductCountSpecificationAsync:BaseSpecifications<Product>
    {

        public ProductCountSpecificationAsync(ProductSpecParam ProductParam):base(
            p => (!ProductParam.BrandId.HasValue || p.ProductBrandId == ProductParam.BrandId) &&
               (!ProductParam.TypeId.HasValue || p.ProductTypeId == ProductParam.TypeId))
        {
            



        }

    }
}
