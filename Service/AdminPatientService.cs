using AutoMapper;
using Domain;
using Domain.DTOs.AdminDTOs;
using Domain.Repositories;
using Domain.Services;
using Domain.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class AdminPatientService : IAdminPatientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IImageService imageService;

        public AdminPatientService(IUnitOfWork unitOfWork, IMapper mapper, IImageService imageService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.imageService = imageService;
        }

        public async Task<ResponseModel<IEnumerable<PatientDTO>>> GetAllPatientsAsync(string role, string search, int page = 1, int pageSize = 5)
        {
            var users = await unitOfWork.AuthRepository.GetUsersInRole("Patient", search, page, pageSize);

            var patientsDTO = mapper.Map<IEnumerable<PatientDTO>>(users);

            foreach (var patient in patientsDTO)
                patient.Image = imageService.GenerateUrl(patient.Image);

            Metadata meta = new Metadata
            {
                Page = 1,
                PageSize = pageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<PatientDTO>> { MetaData = meta, Success = true, Message = "Patient retrieved.", Data = patientsDTO };
        }

        public async Task<ResponseModel<PatientDTO>> GetPatientByIdAsync(string id)
        {
            var patient = await unitOfWork.AuthRepository.GetUserByIdAsync(id);

            if (patient is null)
                return new ResponseModel<PatientDTO> { Message = "Invalid ID!" };

            var roles = await unitOfWork.AuthRepository.GetRolesAsync(patient);

            if (!roles.Contains("Patient"))
                return new ResponseModel<PatientDTO> { Message = "Invalid role!" };

            var patientDTO = mapper.Map<PatientDTO>(patient);

            patientDTO.Image = imageService.GenerateUrl(patientDTO.Image);

            return new ResponseModel<PatientDTO> { Success = true, Message = "Patient retrieved", Data = patientDTO };
        }

    }
}
