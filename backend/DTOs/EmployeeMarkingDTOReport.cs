using backend.Domain.Entities;
using System.Text.Json.Serialization;

namespace backend.DTOs
{
    public class EmployeeMarkingDTOReport
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        [JsonIgnore]
        public Employee? Employee { get; set; }

        public int MarkingId { get; set; }
        [JsonIgnore]
        public Marking? Marking { get; set; }

        public DateTime DateTime { get; set; }

        [JsonPropertyName("employee")]
        public object SerializedEmployee => new
        {
            Employee?.Id,
            Employee?.Name,
            Employee?.Registration,
            Employee?.TitleId,
            Employee?.UserId,
            Employee?.ScheduleId,

            // Adicione mais campos necessários do Employee aqui
        };

    }
}
