using API.Domain.DTOs;

namespace API.Domain.IRepositories
{
    public interface ICartRepo
    {
        Task<string> AddBookToCart(AddToCart addToCart);
        Task<string> SchedulePickup(SchedulePickup request);
    }
}