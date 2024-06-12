using ISAS_Project.DTOs.EvidenceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IPhysicalEvidenceService
    {
        public Task<ActionResult<List<PhysicalEvidenceDTO>>> GetPhysicalEvidences();
        public Task<ActionResult> GetPhysicalEvidenceById(int id);
        public Task<ActionResult<List<PhysicalEvidenceDTO>>> GetForExamination(bool b);
        public Task<ActionResult<List<PhysicalEvidenceDTO>>> GetWithBiologicalTraces(bool b);
        public Task<ActionResult<List<PhysicalEvidenceDTO>>> GetByRisk(string str);
        public Task<ActionResult> AddPhysicalEvidence(PhysicalEvidenceDTO evidenceDTO);
        public Task<ActionResult> UpdatePhysicalEvidence(int id, UpdatePhysicalEvidenceDTO updateEvidenceDTO);
    }
}
