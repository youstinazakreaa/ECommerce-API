using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using onine.core.Entites;
using onine.core.Repositories;
using onineproject.Errors;

namespace onineproject.Controllers
{
 
    public class BasketController : ApiBaseController
    {
        private readonly IBasketRepository _basketRepository;
        public BasketController(IBasketRepository basketRepository)
        {
            _basketRepository = basketRepository;
        }
        //GET or CREATE BASKET
        [HttpGet ("{id}")]
        public async Task <ActionResult<CustomerBasket>> GetCustomerBasket(string basketId)
        {
            var basket = await _basketRepository.GetBasketAsync(basketId);
            return basket is null ?new CustomerBasket(basketId) : Ok (basket);
        }
        //update basket or create basket
        [HttpPost]
        public async Task <ActionResult<CustomerBasket>> updateBasket (CustomerBasket basket)
        {
            var CreatedorUpdateBasket = await _basketRepository.UpdateBasketAsync(basket);

            if (CreatedorUpdateBasket is null)

            

            
                return BadRequest(new ApiResponse(400));
            return Ok(CreatedorUpdateBasket);

        }
        //delete basket
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket (string basketId)
        {
            return await _basketRepository.DeleteBasketAsync(basketId);
        }
    }
}
