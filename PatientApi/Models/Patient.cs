namespace PatientApi.Models
{
    public class Patient
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Name? Name { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool Active { get; set; }
    }

    public class Name
    {
        public string Use { get; set; }
        public string? Family { get; set; }

        public string Given { get; set; } = string.Empty;
    }
}
