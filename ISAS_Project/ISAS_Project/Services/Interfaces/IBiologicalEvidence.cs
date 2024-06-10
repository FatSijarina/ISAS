using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IBiologicalEvidence
    {
        public Task<ActionResult<List<BiologicalEvidenceDTO>>> GetBiologicalEvidences();
        public Task<ActionResult> GetBiologicalEvidenceById(int id);
        public Task<ActionResult> AddBiologicalEvidence(BiologicalEvidenceDTO evidenceDTO);
        public Task<ActionResult> UpdateBiologicalEvidence(int id, UpdateBiologicalEvidenceDTO updateEvidenceDTO);

        public Task<ActionResult<List<BiologicalTraceDTO>>> Compare(int evidenceId, int personId);
    }
}
