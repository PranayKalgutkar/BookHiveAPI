using Microsoft.AspNetCore.Mvc;
using API.Domain.DTOs;
using API.Domain.IDALs;

namespace API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartDAL _dal;

        public CartController(ICartDAL dal)
        {
            _dal = dal;
        }
        [HttpPost("addtocart")]
        public async Task<IActionResult> AddBookToCart([FromBody] AddToCart addToCart)
        {
            var result = await _dal.AddBookToCart(addToCart);

            return Ok(result);
        }

        [HttpPost("schedulepickup")]
        public async Task<IActionResult> SchedulePickup([FromBody] SchedulePickup schedulePickup)
        {
            var result = await _dal.SchedulePickup(schedulePickup);

            return Ok(result);
        }
    }
}