using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Services.Implementation
{
    public class BiologicalTraceService : IBiologicalTraceService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public async Task<ActionResult> DeleteBiologicalTrace(int id)
        {
            var trace = await _context.BiologicalTraces.FindAsync(id);
            if (trace == null)
                return new NotFoundObjectResult("Trace does not exist!!");

            _context.BiologicalTraces.Remove(trace);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Trace deleted successfully!!");
        }

        public async Task<ActionResult<BiologicalTraceDTO>> GetBiologicalTraceById(int id)
        {
            var trace = _mapper.Map<BiologicalTraceDTO>(await _context.BiologicalTraces.FindAsync(id));
            return trace == null
                ? new NotFoundObjectResult("Trace does not exist!!")
                : new OkObjectResult(trace);
        }

        public async Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTraces() =>
            _mapper.Map<List<BiologicalTraceDTO>>(await _context.BiologicalTraces.ToListAsync());
    }
}
