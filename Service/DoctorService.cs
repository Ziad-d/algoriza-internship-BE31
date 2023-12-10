using AutoMapper;
using Domain;
using Domain.DTOs.DoctorDTOs;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class DoctorService : IDoctorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DoctorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<IEnumerable<AppointmentDoctorDTO>>> GetAppointmentsAsync(string doctorId, int page = 1, int pageSize = 5)
        {
            var appointments = await unitOfWork.Appointments.GetAllPaginatedFilteredAsync(a => a.Doctor.Id == doctorId, page, pageSize);

            Metadata meta = new Metadata
            {
                Page = page,
                PageSize = pageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<AppointmentDoctorDTO>> { Success = true, Message = "Appointments retrieved.", Data = mapper.Map<IEnumerable<AppointmentDoctorDTO>>(appointments), MetaData = meta };
        }
        public async Task<ResponseModel<string>> ConfirmCheckUpsAsync(string doctorId, int bookingId)
        {
            var doctor = await unitOfWork.AuthRepository.GetUserByIdAsync(doctorId);

            var booking = await unitOfWork.Bookings.GetByIdAsync(bookingId);

            if (booking is null)
                return new ResponseModel<string> { Message = "No such booking with that id" };

            if (!doctor.Appointments.Any(a => a.Time.Any(t => t.Booking.Id == booking.Id)))
                return new ResponseModel<string> { Message = "No such booking with that id" };

            var request = booking.Request;

            unitOfWork.Bookings.Delete(booking);

            try
            {
                request.RequestState = RequestState.Completed;
                unitOfWork.Complete();
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<string> { Message = "Something went wrong." };
            }

            return new ResponseModel<string> { Message = "Booking confirmed", Success = true, Data = "" };
        }
        public async Task<ResponseModel<Appointment>> CreateAppointmentAsync(AppointmentDoctorDTO appointmentDTO, string doctorId)
        {
            var doctor = await unitOfWork.AuthRepository.GetUserByIdAsync(doctorId);

            var appointment = mapper.Map<Appointment>(appointmentDTO);

            appointment.Doctor = doctor;
            appointment.Time = appointmentDTO.Time.Select(t => new DayTime { Time = t }).ToList();

            var currentAppointments = await unitOfWork.Appointments.GetAllPaginatedFilteredAsync(
                a => a.Doctor.Id == doctorId && a.Days == appointmentDTO.Days, 1, 7);

            if (currentAppointments.Count() >= 1)
                return new ResponseModel<Appointment> { Message = $"There is already an appointment at {appointmentDTO.Days}" };
            
            try
            {
                await unitOfWork.Appointments.CreateAsync(appointment);
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<Appointment> { Message = "Something went wrong." };
            }

            unitOfWork.Complete();

            return new ResponseModel<Appointment> { Success = true, Message = "New appointment is added successfully.", Data = appointment };
        }
        public async Task<ResponseModel<Appointment>> UpdateAppointmentAsync(int appointmentId, AppointmentDoctorDTO appointmentDTO, string doctorId)
        {
            var doctor = await unitOfWork.AuthRepository.GetUserByIdAsync(doctorId);

            var appointment = await unitOfWork.Appointments.GetByIdAsync(appointmentId);

            foreach (var time in appointment.Time)
            {
                if (time.Booking is not null)
                    return new ResponseModel<Appointment> { Message = "You can't update an already booked appointment." };
            }

            mapper.Map(appointmentDTO, appointment);

            appointment.Doctor = doctor;
            appointment.Time = appointmentDTO.Time.Select(t => new DayTime { Time = t }).ToList();

            try
            {
                unitOfWork.Appointments.Update(appointment);
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<Appointment> { Message = "Something went wrong." };
            }

            unitOfWork.Complete();

            return new ResponseModel<Appointment> { Success = true, Message = "Appointment is updated successfully.", Data = appointment };
        }
        public async Task<ResponseModel<Appointment>> DeleteAppointmentAsync(int appointmentId, string doctorId)
        {
            var appointment = await unitOfWork.Appointments.GetByIdAsync(appointmentId);

            if (appointment is null)
                return new ResponseModel<Appointment> { Message = "No appointment with that ID." };

            if (appointment.Doctor.Id != doctorId)
                return new ResponseModel<Appointment> { Message = "No appointment with that ID." };

            foreach (var time in appointment.Time)
            {
                if (time.Booking is not null)
                    return new ResponseModel<Appointment> { Message = "Can't be deleted, already booked" };
            }

            try
            {
                unitOfWork.Appointments.Delete(appointment);
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<Appointment> { Message = "Something went wrong." };
            }

            unitOfWork.Complete();

            return new ResponseModel<Appointment> { Success = true, Message = "Appointment is deleted successfully.", Data = appointment };
        }
    }
}
