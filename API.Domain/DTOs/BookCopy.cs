using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class BookCopy : Book
    {
         public int? NumberOfCopies { get; set; }
    }
}