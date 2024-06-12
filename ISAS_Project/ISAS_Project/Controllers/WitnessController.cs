using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class WitnessController : ControllerBase
    {
        private readonly IWitnessService _witnessService;

        public WitnessController(IWitnessService witnessService)
        {
            _witnessService = witnessService;
        }

        [HttpGet("witnesses")]
        public async Task<ActionResult<List<WitnessDTO>>> GetWitnesses()
        {
            return await _witnessService.GetWitnesses();
        }

        [HttpGet("witness/{id}")]
        public async Task<ActionResult> GetWitnessById(int id)
        {
            return await _witnessService.GetWitnessById(id);
        }

        [HttpGet("is-suspected/{id}")]
        public async Task<ActionResult<bool>> IsSuspected(int id)
        {
            return await _witnessService.IsSuspected(id);
        }

        [HttpGet("is-observed/{id}")]
        public async Task<ActionResult<bool>> IsObserved(int id)
        {
            return await _witnessService.IsObserved(id);
        }

        [HttpGet("witness/info/{id}")]
        public async Task<ActionResult<string>> GetInfo(int id)
        {
            return await _witnessService.GetInfo(id);
        }

        [HttpPost("witness")]
        public async Task<ActionResult> AddWitness(WitnessDTO witnessDTO)
        {
            return await _witnessService.AddWitness(witnessDTO);
        }

        [HttpPut("witness/{id}")]
        public async Task<ActionResult> UpdateWitness(int id, UpdateWitnessDTO updateWitnessDTO)
        {
            return await _witnessService.UpdateWitness(id, updateWitnessDTO);
        }

        [HttpPatch("save-as-suspected/{id}")]
        public async Task<ActionResult> SaveAsSuspected(int id)
        {
            return await _witnessService.SaveAsSuspect(id);
        }
    }
}