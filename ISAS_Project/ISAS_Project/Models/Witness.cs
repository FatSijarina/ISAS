namespace ISAS_Project.Models
{
    public class Witness : Person
    {
        public string? RelationshipWithVictim { get; set; }
        public bool IsMonitored { get; set; }
        public bool IsSuspected { get; set; }
    }
}