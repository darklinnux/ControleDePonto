using System.Text.Json.Serialization;

namespace backend.Domain.Entities
{
    public class ScheduleDay
    {
        public int ScheduleId { get; set; }
        [JsonIgnore]
        public Schedule? Schedule { get; set; }

        public int DayId { get; set; }
        [JsonIgnore]
        public Day? Day { get; set; }
    }
}
