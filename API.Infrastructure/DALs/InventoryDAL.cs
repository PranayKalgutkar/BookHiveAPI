using System.Net;
using Microsoft.Extensions.Logging;
using API.Domain.DTOs;
using API.Shared.Helpers;
using API.Domain.IDALs;
using API.Domain.IRepositories;

namespace API.Infrastructure.DALs
{
    public class InventoryDAL : IInventoryDAL
    {
        private readonly IInventoryRepo _repo;
        private readonly ILogger<InventoryDAL> _logger;
        private readonly ApiResponseHelper _helper;

        public InventoryDAL(IInventoryRepo repo, ILogger<InventoryDAL> logger, ApiResponseHelper helper)
        {
            _repo = repo;
            _logger = logger;
            _helper = helper;
        }
        public async Task<ApiResponse<Book>> AddBookWithCopies(Book newBook)
        {
            var correlationId = Guid.NewGuid();
            try
            {
                var result = await _repo.AddBookWithCopies(newBook);
                return await _helper.GenerateResponse(result, correlationId, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{correlationId}");
                return await _helper.GenerateResponse<Book>(null, correlationId, HttpStatusCode.InternalServerError);
            }
        }
    }
}