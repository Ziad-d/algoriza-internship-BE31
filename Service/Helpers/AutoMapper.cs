using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.AuthDTOs;
using Domain.DTOs.DoctorDTOs;
using Domain.DTOs.PatientDTOs;
using Domain.Models;
using static Azure.Core.HttpHeader;

namespace Services.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDoctorDTO, ApplicationUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<RegisterPatientDTO, ApplicationUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<ApplicationUser, DoctorDTO>()
                .ForMember(dest => dest.Specialize, src => src.MapFrom(src => src.Specialize.Name));
            
            CreateMap<ApplicationUser, PatientDTO>();

            CreateMap<EditDoctorDTO, ApplicationUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Specialization, SpecializationDTO>();

            CreateMap<AppointmentDTO, Appointment>()
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Appointment, AppointmentPatientDTO>()
                .ReverseMap();

            CreateMap<Request, RequestStateDTO>()
                .ReverseMap();

            CreateMap<DayTime, DayTimeDTO>()
                .ReverseMap();

            CreateMap<DiscountCode, DiscountCodeDTO>()
                .ReverseMap();

            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.TimeId, src => src.MapFrom(src => src.Time.Id))
                .ForMember(dest => dest.Time, src => src.MapFrom(src => src.Time.Time))
                .ForMember(dest => dest.Days, src => src.MapFrom(src => src.Time.Appointment.Days))
                .ForMember(dest => dest.DoctorName, src => src.MapFrom(src => src.Time.Appointment.Doctor.FirstName + " " + src.Time.Appointment.Doctor.LastName))
                .ForMember(dest => dest.RequestState, src => src.MapFrom(src => src.Request.RequestState))
                .ForMember(dest => dest.Price, src => src.MapFrom(src => src.Time.Appointment.Price))
                .ForMember(dest => dest.FinalPrice, src => src.MapFrom(src => src.FinalPrice))
                .ReverseMap();

            CreateMap<Booking, BookingPatientDTO>()
                .ForMember(dest => dest.PatientId, src => src.MapFrom(src => src.Patient.Id))
                .ForMember(dest => dest.PatientName, src => src.MapFrom(src => src.Patient.FirstName + " " + src.Patient.LastName))
                .ForMember(dest => dest.FinalPrice, src => src.MapFrom(src => src.FinalPrice))
                .ReverseMap();

            CreateMap<ApplicationUser, AllDoctorsDTO>();
        }
    }
}