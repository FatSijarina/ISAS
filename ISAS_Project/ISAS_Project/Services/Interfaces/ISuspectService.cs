using ISAS_Project.DTOs.PersonDTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface ISuspectService : IGetInfo
    {
        public Task<ActionResult<List<SuspectDTO>>> GetSuspects();
        public Task<ActionResult> GetSuspectById(int id);
        public Task<ActionResult<string>> GetSuspicionAboutSuspect(int id);
        public Task<ActionResult> AddSuspect(SuspectDTO iDyshuariDto);
        public Task<ActionResult> UpdateSuspect(int id, UpdateSuspectDTO updateiDyshuariDto);
    }
}
