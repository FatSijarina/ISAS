using AutoMapper;
using ISAS_Project.DTOs;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.Models;

namespace ISAS_Project.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() {
            CreateMap<BiologicalEvidence, BiologicalEvidenceDTO>().ReverseMap();
            CreateMap<Evidence, EvidenceDTO>().ReverseMap();
            CreateMap<PhysicalEvidence, PhysicalEvidenceDTO>().ReverseMap();
            CreateMap<Person, PersonDTO>().ReverseMap();
            CreateMap<Suspect, SuspectDTO>().ReverseMap();
            CreateMap<Victim, VictimDTO>().ReverseMap();
            CreateMap<Witness, WitnessDTO>().ReverseMap();
            CreateMap<BiologicalTrace, BiologicalTraceDTO>().ReverseMap();
            CreateMap<Statement, StatementDTO>().ReverseMap();
        }
    }
}
