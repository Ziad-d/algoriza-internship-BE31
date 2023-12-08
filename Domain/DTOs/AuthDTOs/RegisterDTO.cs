using Domain.Enums;
using Domain.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.AuthDTOs
{
    public class RegisterDTO
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First Name must not exceed 50 characters."),
            MinLength(3, ErrorMessage = "First Name must not be less than 3 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First name must not exceed 50 characters."),
            MinLength(3, ErrorMessage = "First name must not be less than 3 characters.")]
        public string LastName { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public Gender Gender { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
