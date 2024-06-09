namespace ISAS_Project.DTOs
{
    public class StatementDTO
    {
        public DateTime TimeTaken { get; set; }
        public string Content { get; set; } = null!;
        public int PersonId { get; set; }
    }

    public class UpdateStatementDTO
    {
        public DateTime? TimeTaken { get; set; }
        public string? Content { get; set; }
        public int? PersonId { get; set; }
    }
}
