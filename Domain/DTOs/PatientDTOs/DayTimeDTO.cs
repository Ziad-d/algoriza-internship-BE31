using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class DayTimeDTO
    {
        public int Id { get; set; }
        public TimeOnly Time { get; set; }
        public BookingPatientDTO? Booking { get; set; }
    }
}
