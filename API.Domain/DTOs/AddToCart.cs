using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class AddToCart
    {
        public Guid UserId { get; set; }
        public int BookId { get; set; }
    }
}