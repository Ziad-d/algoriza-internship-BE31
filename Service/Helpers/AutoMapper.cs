using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;

namespace Services.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<RegisterDocDTO, ApplicationUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<ApplicationUser, DoctorDTO>()
                .ForMember(dest => dest.Specialize, src => src.MapFrom(src => src.Specialize.Name)); 
            
            CreateMap<ApplicationUser, PatientDTO>();

            CreateMap<EditDocDTO, ApplicationUser>()
                .ForMember(dest => dest.Image, opt => opt.Ignore());

            CreateMap<Specialization, SpecializationDTO>();
        }
    }
}