using AutoMapper;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Domain;
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
    public class AdminDoctorService : IAdminDoctorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public AdminDoctorService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<ResponseModel<IEnumerable<SpecializationDTO>>> GetAllSpecializationsAsync(string search = "", int page = 1, int pageSize = 5)
        {
            IEnumerable<Specialization> specializations = new List<Specialization>();

            try
            {
                if (!string.IsNullOrEmpty(search))
                    specializations = await unitOfWork.Specializations.GetAllPaginatedFilteredAsync(s => s.Name.Contains(search), page, pageSize);
                else
                    specializations = await unitOfWork.Specializations.GetAllPaginatedFilteredAsync(null, page, pageSize);
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<IEnumerable<SpecializationDTO>> { Message = "Something went wrong." };
            }

            var specializationsDTO = mapper.Map<IEnumerable<SpecializationDTO>>(specializations);

            Metadata meta = new Metadata
            {
                Page = 1,
                PageSize = pageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<SpecializationDTO>> { MetaData = meta, Success = true, Message = "Specializations retrieved.", Data = specializationsDTO };
        }

        public async Task<ResponseModel<IEnumerable<DoctorDTO>>> GetAllDoctorsAsync(string role, string search, int page = 1, int pageSize = 5)
        {
            var users = await unitOfWork.AuthRepository.GetUsersInRole("Doctor", search, page, pageSize);

            var doctorsDTO = mapper.Map<IEnumerable<DoctorDTO>>(users);

            foreach (var doctor in doctorsDTO)
                doctor.Image = imageService.GenerateUrl(doctor.Image);

            Metadata meta = new Metadata
            {
                Page = 1,
                PageSize = pageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<DoctorDTO>> { MetaData = meta, Success = true, Message = "Doctors retrieved.", Data = doctorsDTO };
        }

        public async Task<ResponseModel<DoctorDTO>> GetDoctorByIdAsync(string id)
        {
            var doctor = await unitOfWork.AuthRepository.GetUserByIdAsync(id);

            if (doctor is null)
                return new ResponseModel<DoctorDTO> { Message = "Invalid ID!" };

            var roles = await unitOfWork.AuthRepository.GetRolesAsync(doctor);

            if (!roles.Contains("Doctor"))
                return new ResponseModel<DoctorDTO> { Message = "Invalid role!" };

            var doctorDTO = mapper.Map<DoctorDTO>(doctor);

            doctorDTO.Image = imageService.GenerateUrl(doctorDTO.Image);

            return new ResponseModel<DoctorDTO> { Success = true, Message = "Doctor retrieved", Data = doctorDTO };
        }

    }
}
