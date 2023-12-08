using Domain.Enums;

namespace Domain.Models
{
    public class Request
    {
        public int Id { get; set; }
        public RequestState RequestState { get; set; }

        // Navigation property
        public virtual Booking Booking { get; set; }
    }
}