using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class VictimController : ControllerBase
    {
        private readonly IVictimService _victimService;

        public VictimController(IVictimService victiService)
        {
            _victimService = victiService;
        }

        [HttpGet("victims")]
        public async Task<ActionResult<List<VictimDTO>>> GetVictims()
        {
            return await _victimService.GetVictims();
        }

        [HttpGet("victim/{id}")]
        public async Task<ActionResult<List<VictimDTO>>> GetVictimById(int id)
        {
            return await _victimService.GetVictimById(id);
        }

        /*[HttpGet("info/{id}")]
        public async Task<ActionResult<string>> GetInfo(int id)
        {
            return await _victimService.GetInfo(id);
        }*/

        [HttpPost("victims")]
        public async Task<ActionResult> AddVictim(VictimDTO victimDTO)
        {
            return await _victimService.AddVictim(victimDTO);
        }

        [HttpPut("victim/{id}")]
        public async Task<ActionResult> UpdateVictim(int id, UpdateVictimDTO updateVictimDTO)
        {
            return await _victimService.UpdateVictim(id, updateVictimDTO);
        }
    }
}
