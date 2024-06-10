using ISAS_Project.DTOs.EvidenceDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IEvidenceService
    {
        public Task<ActionResult<List<EvidenceDTO>>> GetEvidences();
        public Task<ActionResult> GetEvidenceById(int id);
        public Task<ActionResult> DeleteEvidence(int id);
    }
}
