using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(50, ErrorMessage = "First Name must not exceed 50 characters."), 
            MinLength(3, ErrorMessage = "First Name must not be less than 3 characters.")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "First name must not exceed 50 characters."),
                    MinLength(3, ErrorMessage = "First name must not be less than 3 characters.")]
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string? Image { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Navigation property
        public virtual ICollection<Appointment>? Appointments { get; set; }
        public virtual ICollection<Booking>? Bookings { get; set; }
        public virtual ICollection<Request>? Requests { get; set; }
        public virtual Specialization? Specialize { get; set; }

    }
}
