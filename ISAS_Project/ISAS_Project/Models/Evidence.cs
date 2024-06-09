namespace ISAS_Project.Models
{
    public class Evidence
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime TimeOfExtraction { get; set; }
        public string? Location { get; set; }
        public string Attachment { get; set; } = null!;

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}