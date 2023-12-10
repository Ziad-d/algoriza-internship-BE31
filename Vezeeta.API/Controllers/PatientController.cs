using Domain;
using Domain.DTOs.AuthDTOs;
using Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace Vezeeta.API.Controllers
{
    [Route("/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IPatientService patientService;

        public PatientController(IAuthService authService, IPatientService patientService)
        {
            this.authService = authService;
            this.patientService = patientService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromForm] RegisterPatientDTO model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await authService.RegisterAsync(model, "Patient");

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] LoginDTO model)
        {
            var result = await authService.LoginAsync(model);

            if (!result.Success)
                return BadRequest(result);

            if (result.Data.Roles.Contains("Patient"))
                return Ok(result);
            else
                return BadRequest(new ResponseModel<AuthDTO> { Message = "Invalid role!" });
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("getDoctorsAppointments")]
        public async Task<IActionResult> GetAllDoctorAppointments(string search = "", int page = 1, int pageSize = 5)
        {
            var result = await patientService.GetAllAppointmentsAsync(search, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("book/id={timeId}")]
        public async Task<IActionResult> BookAsync(int timeId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await patientService.BookAppointmentAsync(userId, timeId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("getAllBookings")]
        public async Task<IActionResult> GetAllBookingsAsync(int page = 1, int pageSize = 5)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await patientService.GetAllBookingsAsync(userId, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [Authorize(Roles = "Patient")]
        [HttpDelete("cancelBooking/id={bookingId}")]
        public async Task<IActionResult> CancelBookingAsync(int bookingId)
        {
            var userId = User.FindFirst("uid")?.Value;

            var result = await patientService.CancelBookingAsync(userId, bookingId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
