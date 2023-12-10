using AutoMapper;
using Domain;
using Domain.DTOs.AdminDTOs;
using Domain.Models;
using Domain.Repositories;
using Domain.Services;
using Domain.Utilities;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Azure.Core.HttpHeader;

namespace Service
{
    public class DiscountCodeService : IDiscountCodeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public DiscountCodeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<ResponseModel<DiscountCode>> AddDiscountCodeAsync(DiscountCodeDTO codeDTO)
        {
            var code = mapper.Map<DiscountCode>(codeDTO);

            await unitOfWork.DiscountCodes.CreateAsync(code);

            try
            {
                unitOfWork.Complete();
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<DiscountCode> { Message = "Something went wrong" };
            }

            return new ResponseModel<DiscountCode> { Message = "DiscountCode is added successfully" };
        }
        public async Task<ResponseModel<IEnumerable<DiscountCode>>> GetAllCodesAsync(string search = "", int page = 1, int PageSize = 5)
        {
            var coupons = await unitOfWork.DiscountCodes.GetAllPaginatedFilteredAsync(c => c.Name.Contains(search), page, PageSize);

            var meta = new Metadata
            {
                Page = page,
                PageSize = PageSize,
                Next = page + 1,
                Previous = page - 1
            };

            return new ResponseModel<IEnumerable<DiscountCode>> { Message = "DiscountCodes retrieved successfully.", Success = true, Data = coupons, MetaData = meta };
        }
        public async Task<ResponseModel<DiscountCode>> DeactivateCodeAsync(int codeId)
        {
            var code = await unitOfWork.DiscountCodes.GetByIdAsync(codeId);

            if (code is null)
                return new ResponseModel<DiscountCode> { Message = "No such code with that ID" };

            code.IsActive = false;

            unitOfWork.DiscountCodes.Update(code);

            try
            {
                unitOfWork.Complete();
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<DiscountCode> { Message = "Something went wrong." };
            }

            return new ResponseModel<DiscountCode> { Message = "Code is updated successfully.", Success = true, Data = code };
        }
        public async Task<ResponseModel<DiscountCode>> DeleteCodeAsync(int codeId)
        {
            var code = await unitOfWork.DiscountCodes.GetByIdAsync(codeId);

            if (code is null)
                return new ResponseModel<DiscountCode> { Message = " no coupon match that id" };

            unitOfWork.DiscountCodes.Delete(code);

            try
            {
                unitOfWork.Complete();
            }
            catch (DbUpdateException)
            {
                return new ResponseModel<DiscountCode> { Message = "Something went wrong." };
            }

            return new ResponseModel<DiscountCode> { Message = "Successfully deleted coupon", Success = true, Data = code };
        }
    }
}
