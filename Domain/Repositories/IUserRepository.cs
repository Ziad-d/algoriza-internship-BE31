using Domain.Models;
using Domain;
using System.Security.Claims;
using Domain.DTOs.AuthDTOs;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ResponseModel<AuthDTO>> RegisterAsync(ApplicationUser user, string password);
        Task<ResponseModel<AuthDTO>> UpdateAsync(ApplicationUser user);
        Task<ResponseModel<AuthDTO>> DeleteAsync(ApplicationUser user);
        Task<ResponseModel<AuthDTO>> AddUserToRole(ApplicationUser user, string role);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string id);
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<IEnumerable<ApplicationUser>> GetUsersInRole(string role, string? search, int page = 1, int pageSize = 5);
        Task<bool> EmailExistAsync(string email);
        Task<bool> UserNameExistAsync(string userName);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
        //Task<bool> RoleExistsAsync(string role);
        //Task<bool> IsInRoleAsync(ApplicationUser user, string role);
    }
}