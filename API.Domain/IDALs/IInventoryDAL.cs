using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Domain.DTOs;
using API.Shared.Helpers;

namespace API.Domain.IDALs
{
    public interface IInventoryDAL
    {
        Task<ApiResponse<Book>> AddBookWithCopies(Book newBook);
    }
}