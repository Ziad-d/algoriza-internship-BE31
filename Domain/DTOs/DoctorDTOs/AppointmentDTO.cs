﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.DoctorDTOs
{
    public class AppointmentDTO
    {
        public int Price { get; set; }
        public Days Days { get; set; }
        public ICollection<TimeOnly> TimeOnly { get; set; }
    }
}
