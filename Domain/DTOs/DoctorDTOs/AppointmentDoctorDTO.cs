using Domain.DTOs.BaseDTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DoctorDTOs
{
    public class AppointmentDoctorDTO : AppointmentDTO
    {
        public ICollection<TimeOnly> Time { get; set; }
    }
}
