using Domain.DTOs.BaseDTOs;
using Domain.DTOs.DoctorDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class AppointmentPatientDTO : AppointmentDTO
    {
        public virtual ICollection<TimeDTO> Time { get; set; }
    }
}
