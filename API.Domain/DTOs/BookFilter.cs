using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class BookFilter
    {
        public string? SearchText { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }
        public string? Genre { get; set; }
        public string? Status { get; set; }
        public string? Language { get; set; } 
         public string? ISBN { get; set; } 
        public string SortColumn { get; set; } = "title";
        public string SortOrder { get; set; } = "ASC";
        public int PageIndex { get; set; } = 0;
        public int PageSize { get; set; } = 10;
    }
}