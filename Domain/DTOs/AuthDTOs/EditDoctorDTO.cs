using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Domain.DTOs.AuthDTOs
{
    public class EditDoctorDTO
    {
        [Required]
        public string Id { get; set; }

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

        [Required]
        public int SpecializeId { get; set; }

        public IFormFile? ImageFile { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
