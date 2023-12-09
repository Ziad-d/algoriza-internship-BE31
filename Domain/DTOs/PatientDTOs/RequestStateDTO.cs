using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.PatientDTOs
{
    public class RequestStateDTO
    {
        public int Id { get; set; }
        public RequestState State { get; set; }
    }
}
