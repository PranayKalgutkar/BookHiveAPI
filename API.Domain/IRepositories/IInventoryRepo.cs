using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTOs;

namespace API.Domain.IRepositories
{
    public interface IInventoryRepo
    {
        Task<Book> AddBookWithCopies(Book newBook);
    }
}