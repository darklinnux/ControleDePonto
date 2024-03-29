namespace backend.Domain.Entities
{
    public class EmployeeMarking
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        public int MarkingId { get; set; }
        public Marking? Marking { get; set; }

        public DateTime DateTime { get; set; }

    }
}

