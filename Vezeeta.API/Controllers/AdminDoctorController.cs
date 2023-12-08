using Domain;
using Domain.DTOs.AuthDTOs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Vezeeta.API.Controllers
{
    [Route("admin/doctor")]
    [ApiController]
    public class AdminDoctorController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IAdminDoctorService adminDoctorService;

        public AdminDoctorController(IAuthService authService, IAdminDoctorService adminDoctorService)
        {
            this.authService = authService;
            this.adminDoctorService = adminDoctorService;
        }

        [HttpPost("registerDoctor")]
        public async Task<IActionResult> Register([FromForm] RegisterDoctorDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.RegisterAsync(model, "Doctor");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("updateDoctor")]
        public async Task<IActionResult> RegisterAsync([FromForm] EditDoctorDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.UpdateAsync(model);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("deleteDoctor/id={doctorId}")]
        public async Task<IActionResult> RegisterAsync(string doctorId)
        {
            var result = await authService.DeleteAsync(doctorId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getAllSpecializations")]
        public async Task<IActionResult> GetSpecializationsAsync(string search = null, int page = 1, int pageSize = 5)
        {
            var result = await adminDoctorService.GetAllSpecializationsAsync(search, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getAllDoctors")]
        public async Task<IActionResult> GetDoctorsAsync(string search = "", int page = 1, int pageSize = 5)
        {
            var result = await adminDoctorService.GetAllDoctorsAsync("Doctor", search, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getDoctor/id={doctorId}")]
        public async Task<IActionResult> GetDoctorByIdAsync(string doctorId)
        {
            var result = await adminDoctorService.GetDoctorByIdAsync(doctorId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        //[HttpPost("addrole")]
        //public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleDTO model, string role)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);

        //    var result = await authService.AddRoleAsync(model, role);

        //    if (!string.IsNullOrEmpty(result))
        //        return BadRequest(result);

        //    return Ok(model);
        //}
    }
}
