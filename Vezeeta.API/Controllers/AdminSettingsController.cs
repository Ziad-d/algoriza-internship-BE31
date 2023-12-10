using Domain.DTOs.AdminDTOs;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Vezeeta.API.Controllers
{
    [Route("admin/settings")]
    [ApiController]
    public class AdminSettingsController : ControllerBase
    {
        private readonly IDiscountCodeService discountCodeService;

        public AdminSettingsController(IDiscountCodeService discountCodeService)
        {
            this.discountCodeService = discountCodeService;
        }

        [HttpPost("addDiscountCode")]
        public async Task<IActionResult> AddCouponAsync([FromForm] DiscountCodeDTO codeDTO)
        {
            var result = await discountCodeService.AddDiscountCodeAsync(codeDTO);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getAllCodes")]
        public async Task<IActionResult> GetAllCouponsAsync(string search = "", int page = 1, int pageSize = 5)
        {
            var result = await discountCodeService.GetAllCodesAsync(search, page, pageSize);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("deactivateCoupon/id={codeId}")]
        public async Task<IActionResult> DeactivateCoupon(int codeId)
        {
            var result = await discountCodeService.DeactivateCodeAsync(codeId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("deleteCoupon/id={codeId}")]
        public async Task<IActionResult> DeleteCoupon(int codeId)
        {
            var result = await discountCodeService.DeleteCodeAsync(codeId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
