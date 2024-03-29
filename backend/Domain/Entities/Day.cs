using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace backend.Domain.Entities
{
    public class Day
    {
        public Day()
        {
            ScheduleDays = new Collection<ScheduleDay>();
        }
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string? Name { get; set; }

        //public ICollection<Schedule> Schedules { get; set; }
        public ICollection<ScheduleDay> ScheduleDays { get; set; }

    }
}
