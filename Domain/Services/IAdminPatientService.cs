using Domain.DTOs.AdminDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAdminPatientService
    {
        Task<ResponseModel<IEnumerable<PatientDTO>>> GetAllPatientsAsync(string role, string search, int page = 1, int pageSize = 5);
        Task<ResponseModel<PatientDTO>> GetPatientByIdAsync(string id);
    }
}
