namespace ISAS_Project.DTOs.PersonDTOs
{
    public class SuspectDTO : PersonDTO
    {
        public string Suspicion { get; set; } = null!;
    }

    public class UpdateSuspectDTO : UpdatePersonDTO
    {
        public string? Suspicion { get; set; }
    }
}
