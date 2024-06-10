using ISAS_Project.DTOs.PersonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IVictimService : IGetInfo
    {
        public Task<ActionResult<List<VictimDTO>>> GetVictims();
        public Task<ActionResult> GetVictimById(int id);
        public Task<ActionResult> AddVictim(VictimDTO victimDTO);
        public Task<ActionResult> UpdateVictim(int id, UpdateVictimDTO updateVictimDTO);
    }
}
