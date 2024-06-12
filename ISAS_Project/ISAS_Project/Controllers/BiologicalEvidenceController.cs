using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.DTOs;
using ISAS_Project.Services.Implementation;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class BiologicalEvidenceController : ControllerBase
    {
        private readonly IBiologicalEvidence _evidence;

        public BiologicalEvidenceController(IBiologicalEvidence evidence)
        {
            _evidence = evidence;
        }

        [HttpGet("biological-evidence")]
        public async Task<ActionResult<List<BiologicalEvidenceDTO>>> GetBiologicalEvidences()
        {
            return await _evidence.GetBiologicalEvidences();
        }

        [HttpGet("biological-evidence/{id}")]
        public async Task<ActionResult<BiologicalEvidenceDTO>> GetBiologicalEvidenceById(int id)
        {
            return await _evidence.GetBiologicalEvidenceById(id);
        }

        [HttpPost("biological-evidence")]
        public async Task<ActionResult> AddBiologicalEvidence(BiologicalEvidenceDTO evidenceDTO)
        {
            return await _evidence.AddBiologicalEvidence(evidenceDTO);
        }

        [HttpPut("biological-evidence/{id}")]
        public async Task<ActionResult> UpdateBiologicalEvidence(int id, UpdateBiologicalEvidenceDTO updateEvidenceDTO)
        {
            return await _evidence.UpdateBiologicalEvidence(id, updateEvidenceDTO);
        }

        [HttpGet("compare-evidence")]
        public async Task<ActionResult<List<BiologicalTraceDTO>>> CompareEvidence(int evidenceId, int personId)
        {
            return await _evidence.Compare(evidenceId, personId);
        }
    }
}
