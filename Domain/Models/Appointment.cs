using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        public int Price { get; set; }
        public Days Days { get; set; }

        // Foreign keys
        [ForeignKey("BookingForeignKey")]
        public int BookingId { get; set; }

        // Navigation properties
        public virtual Booking Booking { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
        public virtual ICollection<DayTime> Time { get; set; }
    }
}
