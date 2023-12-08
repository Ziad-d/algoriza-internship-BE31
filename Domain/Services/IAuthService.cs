using Domain.DTOs.AuthDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IAuthService
    {
        Task<ResponseModel<AuthDTO>> RegisterAsync(RegisterDoctorDTO model, string role);
        Task<ResponseModel<AuthDTO>> LoginAsync(LoginDTO model);
        Task<ResponseModel<AuthDTO>> UpdateAsync(EditDoctorDTO model);
        Task<ResponseModel<AuthDTO>> DeleteAsync(string id);
        //Task<string> AddRoleAsync(AddRoleDTO model, string role);
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
