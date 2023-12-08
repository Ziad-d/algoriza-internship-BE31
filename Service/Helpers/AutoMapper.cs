using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.DTOs.AuthDTOs;
using Domain.DTOs.DoctorDTOs;
using Domain.Models;

namespace Services.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDoctorDTO, ApplicationUser>()
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
        }
    }
}