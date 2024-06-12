using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class PhysicalEvidenceController : ControllerBase
    {
        private readonly IPhysicalEvidenceService _physicalEvidenceService;

        public PhysicalEvidenceController(IPhysicalEvidenceService physicalEvidenceService)
        {
            _physicalEvidenceService = physicalEvidenceService;
        }

        [HttpGet("physical-evidence")]
        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetPhysicalEvidences()
        {
            return await _physicalEvidenceService.GetPhysicalEvidences();
        }

        [HttpGet("physical-evidence/{id}")]
        public async Task<ActionResult<PhysicalEvidenceDTO>> GetPhysicalEvidenceById(int id)
        {
            return await _physicalEvidenceService.GetPhysicalEvidenceById(id);
        }

        [HttpGet("for-examination")]
        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetForExamination(bool forExamination)
        {
            return await _physicalEvidenceService.GetForExamination(forExamination);
        }

        [HttpGet("with-biological-traces")]
        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetWithBiologicalTraces(bool hasBiologicalTraces)
        {
            return await _physicalEvidenceService.GetWithBiologicalTraces(hasBiologicalTraces);
        }

        [HttpGet("by-risk")]
        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetByRisk(string riskLevel)
        {
            return await _physicalEvidenceService.GetByRisk(riskLevel);
        }

        [HttpPost("physical-evidence")]
        public async Task<ActionResult> AddPhysicalEvidence(PhysicalEvidenceDTO evidenceDTO)
        {
            return await _physicalEvidenceService.AddPhysicalEvidence(evidenceDTO);
        }

        [HttpPut("physical-evidence/{id}")]
        public async Task<ActionResult> UpdatePhysicalEvidence(int id, UpdatePhysicalEvidenceDTO updateEvidenceDTO)
        {
            return await _physicalEvidenceService.UpdatePhysicalEvidence(id, updateEvidenceDTO);
        }
    }
}
