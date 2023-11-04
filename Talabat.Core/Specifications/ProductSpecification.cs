using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
    public class ProductSpecification:BaseSpecifications<Product>
    {
        public ProductSpecification(ProductSpecParam ProductParam) :base(
            p=>(string.IsNullOrEmpty(ProductParam.Search)||p.Name.ToLower().Contains(ProductParam.Search))&&
            (!ProductParam.BrandId.HasValue||p.ProductBrandId == ProductParam.BrandId)&&
               (!ProductParam.TypeId.HasValue || p.ProductTypeId == ProductParam.TypeId))
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType);

            if (!string.IsNullOrEmpty(ProductParam.sort))
            {
                switch (ProductParam.sort)
                {
                    case "PriceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "PriceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }


            ApplyPagination(ProductParam.PageSize,ProductParam.PageSize*(ProductParam.PageIndex - 1));


        }


        public ProductSpecification(int id):base(p=>p.Id ==id)
        {
            Includes.Add(p => p.ProductBrand);
            Includes.Add(p => p.ProductType); 
        }





    }
}
