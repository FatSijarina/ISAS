namespace ISAS_Project.DTOs.PersonDTOs
{
    public class VictimDTO : PersonDTO
    {
        public string Location { get; set; } = null!;
        public DateTime Time { get; set; }
        public string? Method { get; set; }
        public string Condition { get; set; } = null!;
    }

    public class UpdateVictimDTO : UpdatePersonDTO
    {
        public string? Location { get; set; }
        public DateTime? Time { get; set; }
        public string? Method { get; set; }
        public string? Condition { get; set; }
    }
}
