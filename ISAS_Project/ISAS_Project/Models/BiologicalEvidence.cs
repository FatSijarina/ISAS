namespace ISAS_Project.Models
{
    public class BiologicalEvidence : Evidence
    {
        public string Type { get; set; } = null!;
        public string Specification { get; set; } = null!;
        public string? ExtractionTechnique { get; set; }
    }
}