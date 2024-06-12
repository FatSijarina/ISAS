using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IInvolvedParty
    {
        public Task<ActionResult> DeletePerson(int id);
        public Task<ICollection<VictimDTO>> GetVictims(int caseId);
        public Task<ICollection<WitnessDTO>> GetWitnesses(int caseId);
        public Task<ICollection<SuspectDTO>> GetSuspects(int caseId);
        public Task<ActionResult<List<StatementDTO>>> GetDeclarationsOfPerson(int id);
        public Task<ActionResult> AddDeclaration(StatementDTO declarationDTO);
        public Task<ActionResult> UpdateDeclaration(int id, UpdateStatementDTO updateStatementDTO);
        public Task<string> Compare(int d1Id, int d2Id);
        public Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTracesOfPerson(int id);
        public Task<ActionResult> AddBiologicalTrace(BiologicalTraceDTO biologicalTraceDTO);
        public Task<ActionResult> UpdateBiologicalTrace(int id, UpdateBiologicalTraceDTO updateBiologicalTraceDTO);
    }
}