using Domain;
using Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Vezeeta.API.Controllers
{
    [Route("admin/stats")]
    [ApiController]
    public class AdminStatsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IUnitOfWork unitOfWork;

        public AdminStatsController(ApplicationDbContext context, IUnitOfWork unitOfWork)
        {
            this.context = context;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet("numberOfDoctors")]
        public async Task<IActionResult> GetNumberOfDoctors()
        {
            var count = await unitOfWork.AuthRepository.GetUsersInRoleCount("Doctor");
            return Ok(new ResponseModel<object> { Message = "Number of doctors retrieved", Success = true, Data = count });
        }

        [HttpGet("numberOfPatients")]
        public async Task<IActionResult> GetNumberOfPatients()
        {
            var count = await unitOfWork.AuthRepository.GetUsersInRoleCount("Patient");
            return Ok(new ResponseModel<object> { Message = "Number of patient retrieved", Success = true, Data = count });
        }

        [HttpGet("numberOfRequests")]
        public async Task<IActionResult> GetNumberOfRequests()
        {
            var numberOfRequests = await context.Requests.CountAsync();
            return Ok(new ResponseModel<object> { Message = "Number of requests retrieved", Success = true, Data = numberOfRequests });
        }

        [HttpGet("topFiveSpecializations")]
        public async Task<IActionResult> GetTopFiveSpecializations()
        {
            var specialization = context.Specializations.OrderByDescending(x => x.Requests).Take(5).ToList();

            return Ok(specialization);
        }
    }
}
