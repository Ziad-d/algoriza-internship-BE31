using Domain.DTOs.AdminDTOs;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface IDiscountCodeService
    {
        Task<ResponseModel<DiscountCode>> AddDiscountCodeAsync(DiscountCodeDTO codeDTO);
        Task<ResponseModel<IEnumerable<DiscountCode>>> GetAllCodesAsync(string search = "", int page = 1, int PageSize = 5);
        Task<ResponseModel<DiscountCode>> DeactivateCodeAsync(int codeId);
        Task<ResponseModel<DiscountCode>> DeleteCodeAsync(int codeId)
    }
}
