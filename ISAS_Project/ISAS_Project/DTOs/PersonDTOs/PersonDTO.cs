﻿namespace ISAS_Project.DTOs.PersonDTOs
{
    public class PersonDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public char Gender { get; set; }
        public string Profession { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string Residence { get; set; } = null!;
        public string MentalState { get; set; } = null!;
        public string Past { get; set; } = null!;
        public List<StatementDTO> Statements { get; set; } = null!;
        public List<BiologicalTraceDTO> BiologicalTraces { get; set; } = null!;
        public int CaseId { get; set; }
    }

    public class UpdatePersonDTO
    {
        public string? Name { get; set; }
        public char? Gender { get; set; }
        public string? Profession { get; set; }
        public string? Status { get; set; }
        public string? Residence { get; set; }
        public string? MentalState { get; set; }
        public string? Past { get; set; }
        public List<StatementDTO>? Statements { get; set; }
        public List<BiologicalTraceDTO>? BiologicalTraces { get; set; }
    }
}
