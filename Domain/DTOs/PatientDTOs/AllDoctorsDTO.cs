using Domain.DTOs.DoctorDTOs;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class AllDoctorsDTO
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string Specialize { get; set; }
        public string? Image { get; set; }

        public ICollection<AppointmentPatientDTO>? Appointments { get; set; }
    }
}
