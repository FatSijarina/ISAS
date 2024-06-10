using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Models;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Services.Implementation
{
    public class PhysicalEvidenceService : IPhysicalEvidenceService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public PhysicalEvidenceService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetEvidencesFizike() =>
            _mapper.Map<List<PhysicalEvidenceDTO>>(await _context.PhysicalEvidences.ToListAsync());

        public async Task<ActionResult> GetEvidenceFizikeById(int id)
        {
            var mappedEvidence = _mapper.Map<PhysicalEvidenceDTO>(await _context.PhysicalEvidences.FindAsync(id));
            return mappedEvidence == null
                ? new NotFoundObjectResult("The evidence does not exist.")
                : new OkObjectResult(mappedEvidence);
        }

        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetForExamination(bool b)
        {
            return _mapper.Map<List<PhysicalEvidenceDTO>>(await _context.PhysicalEvidences
                                .Where(p => p.RequiresExamination == b)
                                .ToListAsync());
        }

        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetWithBiologicalTraces(bool b)
        {
            return _mapper.Map<List<PhysicalEvidenceDTO>>(await _context.PhysicalEvidences
                                .Where(p => p.HasBiologicalTraces == b)
                                .ToListAsync());
        }

        public async Task<ActionResult<List<PhysicalEvidenceDTO>>> GetByDangerLevel(string str)
        {
            return _mapper.Map<List<PhysicalEvidenceDTO>>(await _context.PhysicalEvidences
                                .Where(p => p.DangerLevel == str)
                                .ToListAsync());
        }

        public async Task<ActionResult> AddEvidenceFizike(PhysicalEvidenceDTO evidenceDTO)
        {
            if (evidenceDTO == null)
                return new BadRequestObjectResult("The evidence cannot be null!!");

            var mappedEvidence = _mapper.Map<PhysicalEvidence>(evidenceDTO);
            await _context.PhysicalEvidences.AddAsync(mappedEvidence);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The evidence was successfully added!");
        }

        public async Task<ActionResult> UpdateEvidenceFizike(int id, UpdatePhysicalEvidenceDTO updateEvidenceDTO)
        {
            if (updateEvidenceDTO == null)
                return new BadRequestObjectResult("The evidence cannot be null!!");

            var dbEvidence = await _context.PhysicalEvidences.FindAsync(id);
            if (dbEvidence == null)
                return new NotFoundObjectResult("The evidence does not exist!!");

            dbEvidence.Name = updateEvidenceDTO.Name ?? dbEvidence.Name;
            //dbEvidence.TimeOfExtraction = updateEvidenceDTO.TimeOfExtraction ?? dbEvidence.TimeOfExtraction;
            dbEvidence.Location = updateEvidenceDTO.Location ?? dbEvidence.Location;
            dbEvidence.Attachment = updateEvidenceDTO.Attachment ?? dbEvidence.Attachment;
            dbEvidence.UsedInCrime = updateEvidenceDTO.UsedInCrime ?? dbEvidence.UsedInCrime;
            dbEvidence.DangerLevel = updateEvidenceDTO.DangerLevel ?? dbEvidence.DangerLevel;
            dbEvidence.Classification = updateEvidenceDTO.Classification ?? dbEvidence.Classification;
            dbEvidence.RequiresExamination = updateEvidenceDTO.RequiresExamination ?? dbEvidence.RequiresExamination;
            dbEvidence.HasBiologicalTraces = updateEvidenceDTO.HasBiologicalTraces ?? dbEvidence.HasBiologicalTraces;
            dbEvidence.PersonId = updateEvidenceDTO?.PersonId ?? dbEvidence.PersonId;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The evidence was successfully updated!");
        }
    }
}
