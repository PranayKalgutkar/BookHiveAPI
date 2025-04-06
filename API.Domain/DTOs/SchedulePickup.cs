using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Domain.DTOs
{
    public class SchedulePickup
    {
        public Guid UserId { get; set; }
        public DateTime ScheduledOn { get; set; }

    }
}