using AutoMapper;
using Domain.Models;
using Domain;
using Domain.Repositories;
using Domain.Services;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.DTOs.PatientDTOs;
using Domain.Enums;
using Domain.Utilities;

namespace Service
{
    public class PatientService : IPatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public PatientService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<ResponseModel<BookingDTO>> BookAppointmentAsync(string patientId, int timeId)
        {
            var patient = await unitOfWork.AuthRepository.GetUserByIdAsync(patientId);

            var time = await unitOfWork.Time.GetByIdAsync(timeId);

            if (time.Booking is not null)
            {
                return new ResponseModel<BookingDTO> { Message = "There's an appointment at this time" };
            }

            var request = new Request
            {
                RequestState = RequestState.Pending
            };

            Booking booking = new Booking
            {
                Patient = patient,
                Time = time,
                Request = request
            };

            await unitOfWork.Bookings.CreateAsync(booking);

            patient.Requests.Add(request);

            try
            {
                unitOfWork.Complete();
            }
            catch (DbUpdateException)
            {
                new ResponseModel<BookingDTO> { Message = "Something went wrong." };
            }

            var bookingDTO = new BookingDTO
            {
                TimeId = timeId,
                Time = time.Time,
                RequestState = booking.Request.RequestState,
                DoctorName = time.Appointment.Doctor.FirstName + " " + time.Appointment.Doctor.LastName,
            };

            return new ResponseModel<BookingDTO> { Message = "Appointment is booked successfully.", Success = true, Data = bookingDTO };
        }
        public async Task<ResponseModel<IEnumerable<AllDoctorsDTO>>> GetAllAppointmentsAsync(string search, int page = 1, int pageSize = 5)
        {
            var doctors = await unitOfWork.AuthRepository.GetUsersInRole("Doctor", search, page, pageSize);

            foreach (var doctor in doctors)
                doctor.Image = imageService.GenerateUrl(doctor.Image);

            var doctorsAppointments = mapper.Map<IEnumerable<AllDoctorsDTO>>(doctors);

            var meta = new Metadata
            {
                Page = page,
                PageSize = pageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<AllDoctorsDTO>> { Message = "Doctors and thier appointments are retrieved.", Success = true, Data = doctorsAppointments, MetaData = meta };
        }

    }
}
