using Domain.DTOs.DoctorDTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class AppointmentPatientDTO
    {
        public int Price { get; set; }
        public Days Days { get; set; }
        public virtual ICollection<DayTimeDTO> Time { get; set; }
    }
}
