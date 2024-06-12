using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class SuspectController : ControllerBase
    {
        private readonly ISuspectService _service;

        public SuspectController(ISuspectService service)
        {
            _service = service;
        }

        [HttpGet("suspects")]
        public async Task<ActionResult<List<SuspectDTO>>> GetSuspects()
        {
            return await _service.GetSuspects();
        }

        [HttpGet("suspect/{id}")]
        public async Task<ActionResult> GetSuspectById(int id)
        {
            return await _service.GetSuspectById(id);
        }

        [HttpGet("suspicion-about-suspect/{id}")]
        public async Task<ActionResult<string>> GetSuspicionAboutSuspect(int id)
        {
            return await _service.GetSuspicionAboutSuspect(id);
        }

        [HttpGet("suspect/info/{id}")]
        public async Task<ActionResult<string>> GetInfo(int id)
        {
            return await _service.GetInfo(id);
        }

        [HttpPost("suspect")]
        public async Task<ActionResult> AddSuspect(SuspectDTO suspectDTO)
        {
            return await _service.AddSuspect(suspectDTO);
        }

        [HttpPut("suspect/{id}")]
        public async Task<ActionResult> UpdateSuspect(int id, UpdateSuspectDTO updateSuspectDTO)
        {
            return await _service.UpdateSuspect(id, updateSuspectDTO);
        }
    }
}