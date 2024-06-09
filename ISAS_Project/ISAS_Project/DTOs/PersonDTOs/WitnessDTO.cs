namespace ISAS_Project.DTOs.PersonDTOs
{
    public class WitnessDTO : PersonDTO
    {
        public string RelationshipWithVictim { get; set; } = null!;
        public bool IsMonitored { get; set; }
        public bool IsSuspected { get; set; }
    }

    public class UpdateWitnessDTO : UpdatePersonDTO
    {
        public string? RelationshipWithVictim { get; set; }
        public bool? IsMonitored { get; set; }
        public bool? IsSuspected { get; set; }
    }
}
