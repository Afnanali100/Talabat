using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Core.Specifications;
using Talabat.Dtos;
using Talabat.Errors;
using Talabat.Helpers;

namespace Talabat.Controllers
{
    public class ProductController : ApiBaseController
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IMapper mapper;
        private readonly IGenericRepository<ProductBrand> brandRepo;
        private readonly IGenericRepository<ProductType> typeRepo;

        public ProductController(IGenericRepository<Product> ProductRepo, IMapper mapper
            ,IGenericRepository<ProductBrand>BrandRepo,IGenericRepository<ProductType>TypeRepo
            )
        {
            productRepo = ProductRepo;
            this.mapper = mapper;
            brandRepo = BrandRepo;
            typeRepo = TypeRepo;
        }





        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Product>>> GetAllProducts() {

        //    var products = await productRepo.GetAllAsync();

        //    return Ok(products);

        //}

        //[HttpGet("{id}")]
        //public async Task<ActionResult<Product>> GetProductById(int id)
        //{
        //    var product = await productRepo.GetByIdAsync(id);

        //    return Ok(product);
        //}

        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDtos>>> GetAllProductsBySpecification([FromQuery] ProductSpecParam ProductParam)
        {
            ProductSpecification spec = new ProductSpecification(ProductParam);
            var products = await productRepo.GetAllBySpecificationAsync(spec);

           var Data= mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDtos>>(products);

            ProductCountSpecificationAsync spec2 = new ProductCountSpecificationAsync(ProductParam);
            var Count=await productRepo.GetProductCountSpecAsync(spec2);

            return Ok(new Pagination<ProductToReturnDtos>(Count,ProductParam.PageSize,ProductParam.PageIndex,Data) );

        }

        [ProducesResponseType(typeof(ProductToReturnDtos),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponde), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDtos>> GetProductByIdSpecifcation(int id)
        {
            ProductSpecification spec = new ProductSpecification(id);
            var product = await productRepo.GetByIdSpecificationAsync(spec);
            if(product == null) { return NotFound(new ApiErrorResponde(404)); }
            var MappedProduct=mapper.Map<Product,ProductToReturnDtos>(product);
            return Ok(MappedProduct);
        }

        [HttpGet("brand")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetAllBrands()
        {
            var brands=await brandRepo.GetAllAsync();
            return Ok(brands);
        }



        [HttpGet("type")]

        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetAllTypes()
        {
            var types = await typeRepo.GetAllAsync();
            return Ok(types);
        }

    }
}
