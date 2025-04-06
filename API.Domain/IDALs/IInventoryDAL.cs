using API.Domain.DTOs;
using API.Shared.Helpers;

namespace API.Domain.IDALs
{
    public interface IInventoryDAL
    {
        Task<ApiResponse<BookCopy>> AddBookWithCopies(BookCopy newBook);
        Task<ApiResponse<IEnumerable<BookStatus>>> GetBooksByTitleAndStatus(string? title, string? status);
        Task<ApiResponse<IEnumerable<BookStatus>>> GetBooksByFilter(BookFilter bookFilter);
    }
}