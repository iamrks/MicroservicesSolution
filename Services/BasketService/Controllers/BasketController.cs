using BasketService.Models;
using BasketService.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BasketService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repo;

        public BasketController(IBasketRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{userId}")]
        public ActionResult<CustomerBasket> GetBasket(string userId)
        {
            var basket = _repo.GetBasket(userId);
            return basket is null ? NotFound() : Ok(basket);
        }

        [HttpPost]
        public IActionResult UpdateBasket(CustomerBasket basket)
        {
            _repo.UpdateBasket(basket);
            return Ok(basket);
        }

        [HttpDelete("{userId}")]
        public IActionResult DeleteBasket(string userId)
        {
            _repo.DeleteBasket(userId);
            return NoContent();
        }
    }
}
