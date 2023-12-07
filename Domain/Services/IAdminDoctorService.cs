using Domain.DTOs.AdminDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAdminDoctorService
    {
        Task<ResponseModel<IEnumerable<SpecializationDTO>>> GetAllSpecializationsAsync(string search = "", int page = 1, int pageSize = 5);
        Task<ResponseModel<IEnumerable<DoctorDTO>>> GetAllDoctorsAsync(string role, string search, int page = 1, int pageSize = 5);
        Task<ResponseModel<DoctorDTO>> GetDoctorByIdAsync(string id);
    }
}
