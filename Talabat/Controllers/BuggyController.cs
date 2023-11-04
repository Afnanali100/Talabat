using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Errors;
using Talabat.Repository.Data;

namespace Talabat.Controllers
{
  
    public class BuggyController : ApiBaseController
    {
        private readonly StoreContext store;

        public BuggyController(StoreContext store)
        {
            this.store = store;
        }



        [HttpGet("notfound")]
        public ActionResult<Product> notfounderror()
        {
            var product = store.Product.Find(100);
            if (product is null) return NotFound(new ApiErrorResponde(404));
            
            return Ok(product);
        }


        [HttpGet("servererror")]
        public ActionResult<Product> ServerError()
        {
            var product = store.Product.Find(100);
           var productToReturn=product.ToString();

            return Ok(productToReturn);
        }

        [HttpGet("BadRequest")]
        public ActionResult<Product> BadRequestError()
        {
            return BadRequest(new ApiErrorResponde(400));
        }


        [HttpGet("ValidationError/{id}")]
        public ActionResult<Product> ValidationError(int id)
        {
            return BadRequest();
        }




    }
}
