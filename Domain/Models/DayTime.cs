using System.Text.Json.Serialization;

namespace Domain.Models
{
    public class DayTime
    {
        public int Id { get; set; }
        public TimeOnly Time { get; set; }

        // Navigation properties
        [JsonIgnore]
        public virtual Appointment Appointment { get; set; }
        public virtual Booking? Booking { get; set; }
    }
}