using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class BookTitleStatusFilter
    {
        public string? Title { get; set; }
        public string? Status { get; set; }

    }
}