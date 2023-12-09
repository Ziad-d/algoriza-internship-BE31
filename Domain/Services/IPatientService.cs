using Domain.DTOs.PatientDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IPatientService
    {
        Task<ResponseModel<BookingDTO>> BookAppointmentAsync(string patientId, int timeId);
        Task<ResponseModel<IEnumerable<AllDoctorsDTO>>> GetAllAppointmentsAsync(string search, int page = 1, int pageSize = 5);
    }
}
