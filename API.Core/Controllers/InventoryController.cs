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
        public  async Task<IActionResult> AddBookWithCopies([FromBody] BookCopy newBook)
        {
            var response = await _dal.AddBookWithCopies(newBook);
            return Ok(response);
        }

        [HttpPost("getbooksbytitleandstatus")]
        public  async Task<IActionResult> GetBooksByTitleAndStatus([FromBody] BookTitleStatusFilter filter)
        {
            var response = await _dal.GetBooksByTitleAndStatus(filter.Title, filter.Status);
            return Ok(response);
        }

        [HttpPost("getbookbyfilter")]
        public async Task<IActionResult> GetBooksByFilter([FromBody] BookFilter bookFilter)
        {
            var response = await _dal.GetBooksByFilter(bookFilter);
            return Ok(response);
        }
    }
}