namespace ISAS_Project.Models
{
    public class Statement
    {
        public int Id { get; set; }
        public DateTime TimeOfReceipt { get; set; }
        public string Content { get; set; } = null!;

        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
    }
}