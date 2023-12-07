using Domain.DTOs;
using Domain.DTOs.AdminDTOs;
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
        Task<ResponseModel<AuthDTO>> RegisterAsync(RegisterDocDTO model, string role);
        Task<ResponseModel<AuthDTO>> LoginAsync(DocLoginDTO model);
        Task<ResponseModel<AuthDTO>> UpdateAsync(EditDocDTO model);
        Task<ResponseModel<AuthDTO>> DeleteAsync(string id);
        //Task<string> AddRoleAsync(AddRoleDTO model, string role);
        Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user);
    }
}
