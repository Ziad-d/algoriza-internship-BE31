using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class BookingDTO
    {
        public int TimeId { get; set; }
        public TimeOnly Time { get; set; }
        public string DoctorName { get; set; }
        public RequestState RequestState { get; set; }
    }
}
