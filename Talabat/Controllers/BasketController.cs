using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Interfaces;
using Talabat.Errors;

namespace Talabat.Controllers
{
 
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository basketRepository;

        public BasketController(IBasketRepository basketRepository)
        {
            this.basketRepository = basketRepository;
        }

       

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasket(string basketId)
        {
            var basket= await basketRepository.GetBasketAsync(basketId);
           
            return basket is null? new CustomerBasket(basketId) : basket;
        
        }

        [HttpPost]

        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var CreatedOrUpdatedBasket = await basketRepository.UpdateBacketAsync(basket);

            if(CreatedOrUpdatedBasket is  null) { return BadRequest( new ApiErrorResponde(400)); }

            return Ok(CreatedOrUpdatedBasket);

        }

        [HttpDelete]

        public async Task<ActionResult<bool>> DeleteBasket(string basketId)
        {
          return await  basketRepository.DeleteBasketAsync(basketId);
        }

    }
}
