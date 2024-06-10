using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IGetInfo
    {
        public Task<ActionResult<string>> GetInfo(int id);
    }
}
