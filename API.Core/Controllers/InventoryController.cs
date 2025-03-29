using Microsoft.AspNetCore.Mvc;
using API.Domain.DTOs;
using API.Domain.IDALs;

namespace API.Core.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryDAL _dal;

        public InventoryController(IInventoryDAL dal)
        {
            _dal = dal;
        }
        
        [HttpPost("addbookcopies")]
        public  async Task<IActionResult> AddBookWithCopies([FromBody] Book newBook)
        {
            var response = await _dal.AddBookWithCopies(newBook);
            return Ok(response);
        }
    }
}