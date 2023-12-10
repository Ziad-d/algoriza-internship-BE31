using Domain.Models;
using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Domain.DTOs.AuthDTOs;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<ResponseModel<AuthDTO>> RegisterAsync(ApplicationUser user, string password)
        {
            var result = await userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return ErrorMessage(result);
            }

            return new ResponseModel<AuthDTO> { Success = true, Message = "Account created successfully" };
        }

        public async Task<ResponseModel<AuthDTO>> AddUserToRole(ApplicationUser user, string role)
        {
            var result = await userManager.AddToRoleAsync(user, role);

            if (!result.Succeeded)
            {
                return ErrorMessage(result);
            }

            return new ResponseModel<AuthDTO> { Success = true, Message = $"{user.FirstName} is added to role {role}" };
        }

        public async Task<ResponseModel<AuthDTO>> UpdateAsync(ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return ErrorMessage(result);
            }

            return new ResponseModel<AuthDTO> { Success = true, Message = "Updated account successfully" };
        }

        public async Task<ResponseModel<AuthDTO>> DeleteAsync(ApplicationUser user)
        {
            try
            {
                await userManager.DeleteAsync(user);
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<AuthDTO> { Message = "Something went wrong." };
            }

            return new ResponseModel<AuthDTO> { Message = "Deleted successfully", Success = true };
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await userManager.FindByEmailAsync(email);
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await userManager.FindByIdAsync(id);
        }

        public async Task<IList<Claim>> GetClaimsAsync(ApplicationUser user)
        {
            return await userManager.GetClaimsAsync(user);
        }

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await userManager.GetRolesAsync(user);
        }

        public async Task<IEnumerable<ApplicationUser>> GetUsersInRole(string role, string? search, int page = 1, int pageSize = 5)
        {
            var users = await userManager.GetUsersInRoleAsync(role);

            if (!string.IsNullOrEmpty(search))
            {
                return users.Where(u => $"{u.FirstName} {u.LastName}".Contains(search)).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return users.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            if (await userManager.FindByEmailAsync(email) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> UserNameExistAsync(string userName)
        {
            if (await userManager.FindByNameAsync(userName) is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await userManager.CheckPasswordAsync(user, password);
        }

        public async Task<int> GetUsersInRoleCount(string roleName)
        {
            var usersInRole = await userManager.GetUsersInRoleAsync(roleName);
            return usersInRole.Count;
        }

        //public async Task<bool> RoleExistsAsync(string role)
        //{
        //    return await roleManager.RoleExistsAsync(role);
        //}

        //public async Task<bool> IsInRoleAsync(ApplicationUser user, string role)
        //{
        //    return await userManager.IsInRoleAsync(user, role);
        //}

        internal ResponseModel<AuthDTO> ErrorMessage(IdentityResult result)
        {
            var errors = string.Empty;

            foreach (var error in result.Errors)
            {
                errors += $"{error.Description},";
            }

            return new ResponseModel<AuthDTO> { Success = false, Message = errors };
        }

    }
}
