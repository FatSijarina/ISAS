using ISAS_Project.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IBiologicalTraceService
    {
        public Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTraces();
        public Task<ActionResult<BiologicalTraceDTO>> GetBiologicalTraceById(int id);
        public Task<ActionResult> AddBiologicalTrace(BiologicalTraceDTO biologicalTraceDTO);
        public Task<ActionResult> DeleteBiologicalTrace(int id);
    }
}
