using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTOs;

namespace API.Domain.IRepositories
{
    public interface IInventoryRepo
    {
        Task<BookCopy> AddBookWithCopies(BookCopy newBook);
        Task<IEnumerable<BookStatus>> GetBooksByTitleAndStatus(string? title, string? status);
        Task<IEnumerable<BookStatus>> GetBooksByFilter(BookFilter bookFilter);
    }
}