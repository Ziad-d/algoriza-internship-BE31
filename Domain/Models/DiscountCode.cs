using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Value { get; set; }
        public bool IsActive { get; set; }
        public int BookingsNumber { get; set; }

        // Navigation property
        public virtual List<Booking> Bookings { get; set; }
    }
}
