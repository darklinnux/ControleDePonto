using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace backend.Domain.Entities
{
    public class Schedule
    {
        public Schedule()
        {
            ScheduleDays = new Collection<ScheduleDay>();
        }
        public int Id { get; set; }

        [StringLength(15)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public TimeSpan? Start { get; set; }

        [Required]
        public TimeSpan? End { get; set; }

        [JsonIgnore]
        //public ICollection<Day> Days { get; set; }
        public ICollection<ScheduleDay> ScheduleDays { get; set; }

    }
}
