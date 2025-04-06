using System.Net;
using Microsoft.Extensions.Logging;
using API.Domain.DTOs;
using API.Shared.Helpers;
using API.Domain.IDALs;
using API.Domain.IRepositories;

namespace API.Infrastructure.DALs
{
    public class CartDAL : ICartDAL
    {
        private readonly ICartRepo _repo;
        private readonly ILogger<CartDAL> _logger;
        private readonly ApiResponseHelper _helper;

        public CartDAL(ICartRepo repo, ILogger<CartDAL> logger, ApiResponseHelper helper)
        {
            _repo = repo;
            _logger = logger;
            _helper = helper;
        }
        public async Task<ApiResponse<string>> AddBookToCart(AddToCart addToCart)
        {
            var correlationId = Guid.NewGuid();
            try
            {
                var result = await _repo.AddBookToCart(addToCart);
                return await _helper.GenerateResponse(result, correlationId, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{correlationId}");
                return await _helper.GenerateResponse<string>(null, correlationId, HttpStatusCode.InternalServerError);
            }
        }

        public async Task<ApiResponse<string>> SchedulePickup(SchedulePickup schedulePickup)
        {
            var correlationId = Guid.NewGuid();
            try
            {
                var result = await _repo.SchedulePickup(schedulePickup);
                return await _helper.GenerateResponse(result, correlationId, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{correlationId}");
                return await _helper.GenerateResponse<string>(null, correlationId, HttpStatusCode.InternalServerError);
            }
        }
    }
}