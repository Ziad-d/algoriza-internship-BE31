using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class BookingPatientDTO
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string PatientName { get; set; }
        public RequestStateDTO State { get; set; }
    }
}
