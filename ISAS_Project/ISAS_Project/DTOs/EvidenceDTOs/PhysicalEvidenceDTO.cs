namespace ISAS_Project.DTOs.EvidenceDTOs
{
    public class PhysicalEvidenceDTO : EvidenceDTO
    {
        public bool UsedInCrime { get; set; }
        public string? RiskLevel { get; set; }
        public string Classification { get; set; } = null!;
        public bool RequiresExamination { get; set; }
        public bool HasBiologicalTraces { get; set; }
    }

    public class UpdatePhysicalEvidenceDTO : UpdateEvidenceDTO
    {
        public bool? UsedInCrime { get; set; }
        public string? RiskLevel { get; set; }
        public string? Classification { get; set; }
        public bool? RequiresExamination { get; set; }
        public bool? HasBiologicalTraces { get; set; }
    }
}
