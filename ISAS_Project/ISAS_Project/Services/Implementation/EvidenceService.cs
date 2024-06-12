using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Services.Implementation
{
    public class EvidenceService : IEvidenceService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;
        public EvidenceService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<EvidenceDTO>>> GetEvidences() =>
            _mapper.Map<List<EvidenceDTO>>(await _context.Evidences.ToListAsync());

        public async Task<ActionResult> GetEvidenceById(int id)
        {
            var mappedProva = _mapper.Map<EvidenceDTO>(await _context.Evidences.FindAsync(id));
            return mappedProva == null
                ? new NotFoundObjectResult("Evidence does not exist.")
                : new OkObjectResult(mappedProva);
        }

        public async Task<ActionResult> DeleteEvidence(int id)
        {
            var evidence = await _context.Evidences.FindAsync(id);
            if (evidence == null)
                return new NotFoundObjectResult("Evidence does not exist!");

            _context.Evidences.Remove(evidence);
            await _context.SaveChangesAsync();
            return new OkObjectResult("Prova was successfully deleted!");
        }
    }
}