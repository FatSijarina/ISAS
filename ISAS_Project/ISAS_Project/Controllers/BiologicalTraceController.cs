using ISAS_Project.DTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class BiologicalTraceController : ControllerBase
    {
        private readonly IBiologicalTraceService _biologicalTrace;

        public BiologicalTraceController(IBiologicalTraceService biologicalTrace)
        {
            _biologicalTrace = biologicalTrace;
        }

        [HttpGet("Biological_Traces")]
        public async Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTraces()
        {
            return await _biologicalTrace.GetBiologicalTraces();
        }

        [HttpGet("Biological_Trace/{id}")]
        public async Task<ActionResult<BiologicalTraceDTO>> GetBiologicalTraceById(int id)
        {
            return await _biologicalTrace.GetBiologicalTraceById(id);
        }

        [HttpDelete("Biological_Trace/{id}")]
        public async Task<ActionResult> DeleteBiologicalTrace(int id)
        {
            return await _biologicalTrace.DeleteBiologicalTrace(id);
        }
    }
}