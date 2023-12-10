using Domain.DTOs.DoctorDTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IDoctorService
    {
        Task<ResponseModel<IEnumerable<AppointmentDTO>>> GetAppointmentsAsync(string doctorId, int page = 1, int pageSize = 5);
        Task<ResponseModel<string>> ConfirmCheckUpsAsync(string doctorId, int bookingId);
        Task<ResponseModel<Appointment>> CreateAppointmentAsync(AppointmentDTO appointmentDTO, string doctorId);
        Task<ResponseModel<Appointment>> UpdateAppointmentAsync(int appointmentId, AppointmentDTO appointmentDTO, string doctorId);
        Task<ResponseModel<Appointment>> DeleteAppointmentAsync(int appointmentId, string doctorId);
    }
}
