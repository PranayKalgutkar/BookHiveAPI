using API.Domain.DTOs;
using API.Shared.Helpers;

namespace API.Domain.IDALs
{
    public interface ICartDAL
    {
        Task<ApiResponse<string>> AddBookToCart(AddToCart addToCart);
        Task<ApiResponse<string>> SchedulePickup(SchedulePickup schedulePickup);
    }
}