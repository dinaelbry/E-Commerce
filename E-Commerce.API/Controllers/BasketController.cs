using E_Commerce.Application.Common;
using E_Commerce.Application.Contracts;
using E_Commerce.Application.DTO_s.Baskets;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
 
    public class BasketController : ApiBaseController
    {
        private readonly IBasketServices _basketService;

        public BasketController(IBasketServices basketService)
        {
            _basketService = basketService;
        }

        // GET baseUrl/api/Baskets/Id
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BasketDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BasketDto>> GetBasket(string id, CancellationToken cancellationToken)
        {
            var basket = await _basketService.GetBasketAsync(id, cancellationToken);
            return ToActionResult(basket);
        }



        // Post BaseUrl/api/Baskets -> Body [BasketDto]
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto basket, CancellationToken cancellationToken)
        {
            var saved = await _basketService.CreateOrUpdateBasketAsync(basket, cancellationToken);
            return ToActionResult(saved);
        }

        // Delete baseUrl/api/Baskets/Id

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteBasket(string id, CancellationToken cancellationToken)
        {
            var result = await _basketService.DeleteBasketAsync(id, cancellationToken);
            return ToActionResult(result);
        }

    }
}
