using AutoMapper;
using Domain;
using Domain.DTOs.AuthDTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Repository;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class AuthService : IAuthService
    {
        private readonly JWT jwt;
        private readonly IUnitOfWork unitOfWork;
        private readonly IImageService imageService;
        private readonly IMapper mapper;

        public AuthService(IOptions<JWT> jwt, IUnitOfWork unitOfWork, IImageService imageService, IMapper mapper)
        {
            this.jwt = jwt.Value;
            this.unitOfWork = unitOfWork;
            this.imageService = imageService;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<AuthDTO>> RegisterAsync<TRegisterDTO>(TRegisterDTO model, string role) where TRegisterDTO : RegisterDTO
        {
            if (await unitOfWork.AuthRepository.EmailExistAsync(model.Email))
                return new ResponseModel<AuthDTO> { Success = false, Message = "Email is already registered" };

            if (await unitOfWork.AuthRepository.UserNameExistAsync(model.Username))
                return new ResponseModel<AuthDTO> { Success = false, Message = "Username is already registered" };

            var imgUrl = await imageService.ValidateImage(model.ImageFile);
            if (imgUrl is null)
                return new ResponseModel<AuthDTO> { Success = false, Message = "Image is not valid (must not exceed 2mb and allowed extensions are (.png, .jpg, .webp))" };

            ApplicationUser user = mapper.Map<ApplicationUser>(model);
            user.Image = imgUrl;

            Specialization specialize = new Specialization();

            if (model is RegisterDoctorDTO doctorModel)
            {
                specialize = await unitOfWork.Specializations.GetByIdAsync(doctorModel.SpecializeId);

                if (specialize is null)
                    return new ResponseModel<AuthDTO> { Success = false, Message = "No specialize match that id: " + doctorModel.SpecializeId };

                user.Specialize = specialize;
            }

            var result = await unitOfWork.AuthRepository.RegisterAsync(user, model.Password);

            if (result.Success)
            {
                await unitOfWork.AuthRepository.AddUserToRole(user, role);

                var jwtSecurityToken = await CreateJwtToken(user);

                return new ResponseModel<AuthDTO>
                {
                    Message = result.Message,
                    Success = result.Success,
                    Data = new AuthDTO
                    {
                        Email = user.Email,
                        ExpiresOn = jwtSecurityToken.ValidTo,
                        IsAuthenticated = true,
                        Roles = new List<string> { "Doctor" },
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Username = user.UserName
                    }
                };
            }
            else
            {
                imageService.DeleteImage(imgUrl);
                return result;
            }
        }

        public async Task<ResponseModel<AuthDTO>> LoginAsync(LoginDTO model)
        {
            var user = await unitOfWork.AuthRepository.GetUserByEmailAsync(model.Email);

            if (user is null || !await unitOfWork.AuthRepository.CheckPasswordAsync(user, model.Password))
            {
                return new ResponseModel<AuthDTO> { Message = "Invalid credentials!" };
            }

            var jwtSecurityToken = await CreateJwtToken(user);

            return new ResponseModel<AuthDTO>
            {
                Message = "Logged in successfully",
                Success = true,
                Data = new AuthDTO
                {
                    Email = user.Email,
                    ExpiresOn = jwtSecurityToken.ValidTo,
                    IsAuthenticated = true,
                    Roles = (List<string>)await unitOfWork.AuthRepository.GetRolesAsync(user),
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                    Username = user.UserName,
                    Image = imageService.GenerateUrl(user.Image)
                }
            };
        }

        public async Task<ResponseModel<AuthDTO>> UpdateAsync(EditDoctorDTO model)
        {
            var user = await unitOfWork.AuthRepository.GetUserByIdAsync(model.Id);

            if (user is null)
                return new ResponseModel<AuthDTO> { Message = $"this ID: {model.Id} doesn't exist" };

            mapper.Map(model, user);
            
            var oldImg = user.Image;

            if (model.ImageFile is not null)
            {
                user.Image = await imageService.ValidateImage(model.ImageFile);
            }

            Specialization specialize = new Specialization();
            var roles = (List<string>)await unitOfWork.AuthRepository.GetRolesAsync(user);

            if (roles.Contains("Doctor"))
            {
                specialize = await unitOfWork.Specializations.GetByIdAsync(model.SpecializeId);

                if (specialize is null)
                {
                    return new ResponseModel<AuthDTO> { Success = false, Message = "No specialize match that id: " + model.SpecializeId };
                }
                user.Specialize = specialize;
            }

            var result = await unitOfWork.AuthRepository.UpdateAsync(user);

            if (result.Success)
            {

                var jwtSecurityToken = await CreateJwtToken(user);

                imageService.DeleteImage(oldImg);

                return new ResponseModel<AuthDTO>
                {
                    Message = result.Message,
                    Success = result.Success,
                    Data = new AuthDTO
                    {
                        Email = user.Email,
                        ExpiresOn = jwtSecurityToken.ValidTo,
                        Roles = (List<string>)await unitOfWork.AuthRepository.GetRolesAsync(user),
                        Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                        Username = user.UserName,
                        Image = imageService.GenerateUrl(user.Image)
                    }
                };
            }
            else
                return result;
        }

        public async Task<ResponseModel<AuthDTO>> DeleteAsync(string id)
        {
            var user = await unitOfWork.AuthRepository.GetUserByIdAsync(id);

            if (user is null)
            {
                return new ResponseModel<AuthDTO> { Message = $"this ID: {id} doesn't exist" };
            }

            return await unitOfWork.AuthRepository.DeleteAsync(user);
        }

        //public async Task<string> AddRoleAsync(AddRoleDTO model, string role)
        //{
        //    var user = await unitOfWork.AuthRepository.GetUserByIdAsync(model.UserId);

        //    if (user is null || !await unitOfWork.AuthRepository.RoleExistsAsync(role))
        //        return "Invalid user ID or Role";

        //    if (await unitOfWork.AuthRepository.IsInRoleAsync(user, role))
        //        return "User already assigned to this role";

        //    var result = await unitOfWork.AuthRepository.AddUserToRole(user, role);

        //    return result.Success ? string.Empty : "Something went wrong";
        //}

        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await unitOfWork.AuthRepository.GetClaimsAsync(user);
            var roles = await unitOfWork.AuthRepository.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: jwt.Issuer,
                audience: jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
