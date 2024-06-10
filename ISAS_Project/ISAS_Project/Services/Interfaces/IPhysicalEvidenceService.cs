using ISAS_Project.DTOs.EvidenceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IPhysicalEvidenceService
    {
        public Task<ActionResult<List<EvidenceDTO>>> GetPhysicalEvidences();
        public Task<ActionResult> GetPhysicalEvidenceById(int id);
        public Task<ActionResult<List<EvidenceDTO>>> GetForExamination(bool b);
        public Task<ActionResult<List<EvidenceDTO>>> GetWithBiologicalTraces(bool b);
        public Task<ActionResult<List<EvidenceDTO>>> GetByRisk(string str);
        public Task<ActionResult> AddPhysicalEvidence(EvidenceDTO evidenceDTO);
        public Task<ActionResult> UpdatePhysicalEvidence(int id, UpdateEvidenceDTO updateEvidenceDTO);
    }
}
