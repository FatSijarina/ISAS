namespace ISAS_Project.DTOs.EvidenceDTOs
{
    public class BiologicalEvidenceDTO : EvidenceDTO
    {
        public string Type { get; set; } = null!;
        public string Specification { get; set; } = null!;
        public string? ExtractionTechnique { get; set; }
    }

    public class UpdateBiologicalEvidenceDTO : UpdateEvidenceDTO
    {
        public string? Type { get; set; }
        public string? Specification { get; set; }
        public string? ExtractionTechnique { get; set; }
    }
}
