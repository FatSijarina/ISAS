namespace ISAS_Project.Models
{
    public class Person
    {
        public int Id { get; set; }
        public int CaseId { get; set; }
        public string Name { get; set; } = null!;
        public char Gender { get; set; }
        public string Profession { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Residence { get; set; } = null!;
        public string MentalState { get; set; } = null!;
        public string Past { get; set; } = null!;

        public List<Statement>? Declarations { get; set; } = null!;
        public List<BiologicalTrace>? BiologicalTraces { get; set; } = null!;
        public List<Evidence>? Evidences { get; set; } = null!;
    }
}