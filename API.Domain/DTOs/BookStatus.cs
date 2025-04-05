using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class BookStatus : BookCopy
    {
        public string CopyStatus { get; set; } = string.Empty;
        //public int NumberOfCopies { get; set; }
    }
}