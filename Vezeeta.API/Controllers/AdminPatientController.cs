using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.API.Controllers
{
    [Route("admin/patient")]
    [ApiController]
    public class AdminPatientController : ControllerBase
    {
        private readonly IAdminPatientService adminPatientService;

        public AdminPatientController(IAdminPatientService adminPatientService)
        {
            this.adminPatientService = adminPatientService;
        }

        [HttpGet("getAllPatients")]
        public async Task<IActionResult> GetPatientsAsync(string search = "", int page = 1, int pageSize = 5)
        {
            var result = await adminPatientService.GetAllPatientsAsync("Patient", search, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getPatient/id={patientId}")]
        public async Task<IActionResult> GetPatientById(string patientId)
        {
            var result = await adminPatientService.GetPatientByIdAsync(patientId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
