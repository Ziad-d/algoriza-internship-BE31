using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DiscountCode
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Discount { get; set; }
        public DiscountType DiscountType { get; set; }
        public int Value { get; set; }
        public bool IsActive { get; set; }
        public int BookingsNumber { get; set; }

        // Navigation property
        [JsonIgnore]
        public virtual ICollection<ApplicationUser> Patients { get; set; }
    }
}
