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
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public string DoctorId { get; set; }

        [ForeignKey("AppointmentForeignKey")]
        public int AppointmentId { get; set; }

        // Navigation properties
        public virtual ApplicationUser Patient { get; set; }
        public virtual Appointment Appointment { get; set; }
        public virtual Request Request { get; set; }
    }
}
