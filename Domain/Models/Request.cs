using Domain.Enums;

namespace Domain.Models
{
    public class Request
    {
        public int Id { get; set; }

        public RequestState RequestState { get; set; }

        public string PatientId { get; set; }
        public virtual ApplicationUser Doctor { get; set; }
    }
}