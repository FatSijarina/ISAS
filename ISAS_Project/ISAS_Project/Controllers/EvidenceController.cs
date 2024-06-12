using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Services.Implementation;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class EvidenceController : ControllerBase
    {
        private readonly IEvidenceService _service;

        public EvidenceController(IEvidenceService service)
        {
            _service = service;
        }

        [HttpGet("all-evidence")]
        public async Task<ActionResult<List<EvidenceDTO>>> GetEvidences()
        {
            return await _service.GetEvidences();
        }

        [HttpGet("evidence/{id}")]
        public async Task<ActionResult<EvidenceDTO>> GetEvidenceById(int id)
        {
            return await _service.GetEvidenceById(id);
        }

        [HttpDelete("evidence/{id}")]
        public async Task<ActionResult> DeleteEvidence(int id)
        {
            return await _service.DeleteEvidence(id);
        }
    }
}