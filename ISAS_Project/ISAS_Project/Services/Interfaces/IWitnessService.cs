using ISAS_Project.DTOs.PersonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IWitnessService : IGetInfo
    {
        public Task<ActionResult<List<WitnessDTO>>> GetWitnesses();
        public Task<ActionResult> GetWitnessById(int id);
        public Task<ActionResult<bool>> IsSuspected(int id);
        //public Task<ActionResult<bool>> IsObserved(int id);
        public Task<ActionResult> AddWitness(WitnessDTO witnessDTO);
        public Task<ActionResult> UpdateWitness(int id, UpdateWitnessDTO updateWitnessDTO);
    }
}
