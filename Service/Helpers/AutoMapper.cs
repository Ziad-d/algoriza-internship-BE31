using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.AuthDTOs;
using Domain.DTOs.BaseDTOs;
using Domain.DTOs.DoctorDTOs;
using Domain.DTOs.PatientDTOs;
using Domain.Models;

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

            //CreateMap<AppointmentDTO, Appointment>()
            //    .ForMember(dest => dest.Time, opt => opt.Ignore())
            //    .ReverseMap();

            CreateMap<AppointmentDoctorDTO, Appointment>()
                .ForMember(dest => dest.Time, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Appointment, AppointmentPatientDTO>()
                .ReverseMap();

            CreateMap<Request, RequestStateDTO>()
                .ReverseMap();

            CreateMap<DayTime, DayTimeDTO>()
                .ReverseMap();

            CreateMap<Booking, BookingDTO>()
                .ForMember(dest => dest.TimeId, src => src.MapFrom(src => src.Time.Id))
                .ForMember(dest => dest.Time, src => src.MapFrom(src => src.Time.Time))
                .ForMember(dest => dest.Days, src => src.MapFrom(src => src.Time.Appointment.Days))
                .ForMember(dest => dest.DoctorName, src => src.MapFrom(src => src.Time.Appointment.Doctor.FirstName + " " + src.Time.Appointment.Doctor.LastName))
                .ForMember(dest => dest.RequestState, src => src.MapFrom(src => src.Request.RequestState))
                .ReverseMap();

            CreateMap<ApplicationUser, AllDoctorsDTO>();
        }
    }
}