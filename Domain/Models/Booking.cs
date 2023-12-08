using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [ForeignKey("TimeForeignKey")]
        public int TimeId { get; set; }

        [ForeignKey("RequestForeignKey")]
        public int RequestId { get; set; }

        // Navigation properties
        public virtual ApplicationUser Patient { get; set; }
        public virtual DayTime Time { get; set; }
        public virtual Request Request { get; set; }
    }
}
