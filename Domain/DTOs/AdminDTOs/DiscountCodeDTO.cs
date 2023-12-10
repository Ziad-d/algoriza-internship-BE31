using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AdminDTOs
{
    public class DiscountCodeDTO
    {
        public string Name { get; set; }
        public int NumberOfRequests { get; set; }
        public bool IsActive { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Discount { get; set; }
    }
}
