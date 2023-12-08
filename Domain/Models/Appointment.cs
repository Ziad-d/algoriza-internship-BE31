using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public int Price { get; set; }
        public Days Days { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual ApplicationUser Doctor { get; set; }
        public virtual ICollection<DayTime> Time { get; set; }
    }
}
