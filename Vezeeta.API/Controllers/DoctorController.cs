using Domain.DTOs.AuthDTOs;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;
using Domain.Services;
using Domain.DTOs.DoctorDTOs;
using Microsoft.AspNetCore.Authorization;

namespace Vezeeta.API.Controllers
{
    [Route("/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IDoctorService doctorService;

        public DoctorController(IAuthService authService, IDoctorService doctorService)
        {
            this.authService = authService;
            this.doctorService = doctorService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.LoginAsync(model);

            if (!result.Success)
                return BadRequest(result);

            if (result.Data.Roles.Contains("Doctor"))
                return Ok(result);
            else
                return BadRequest(new ResponseModel<AuthDTO> { Message = "Invalid role!" });
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("getAppointments")]
        public async Task<IActionResult> GetAllAppointmentsAsync(int page = 1, int pageSize = 5)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await doctorService.GetAppointmentsAsync(userId, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("confirmCheckUp/id={bookingId}")]
        public async Task<IActionResult> ConfirmCheckUpsAsync(int bookingId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await doctorService.ConfirmCheckUpsAsync(userId, bookingId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("CreateAppointment")]
        public async Task<IActionResult> CreateAppointmentAsync([FromForm] AppointmentDTO appointment)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await doctorService.CreateAppointmentAsync(appointment, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("updateAppointment/id={appointmentId}")]
        public async Task<IActionResult> UpdateAppointmentAsync([FromForm] AppointmentDTO appointment, int appointmentId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await doctorService.UpdateAppointmentAsync(appointmentId, appointment, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpDelete("deleteAppointment/id={appointmentId}")]
        public async Task<IActionResult> DeleteAppointmentAsync(int appointmentId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await doctorService.DeleteAppointmentAsync(appointmentId, userId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
