using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs;
using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.Models;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Services.Implementation
{
    public class SuspectService : ISuspectService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public SuspectService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<SuspectDTO>>> GetSuspects()
        {
            var suspects = await _context.Suspects.ToListAsync();
            if (!suspects.Any())
                return new NotFoundObjectResult("No suspects registered!");

            var mappedSuspects = _mapper.Map<List<SuspectDTO>>(suspects);

            foreach (var mappedSuspect in mappedSuspects)
            {
                var id = mappedSuspect.Id;
                var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();
                mappedSuspect.Statements = _mapper.Map<List<StatementDTO>>(declarations);
                var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();
                mappedSuspect.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);
            }

            return new OkObjectResult(mappedSuspects);
        }

        public async Task<ActionResult> GetSuspectById(int id)
        {
            var suspect = await _context.Suspects.FindAsync(id);
            if (suspect == null)
                return new NotFoundObjectResult("The suspect does not exist!");

            var mappedSuspect = _mapper.Map<SuspectDTO>(suspect);
            var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();
            mappedSuspect.Statements = _mapper.Map<List<StatementDTO>>(declarations);
            var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();
            mappedSuspect.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);

            return new OkObjectResult(mappedSuspect);
        }

        public async Task<ActionResult<string>> GetSuspicionAboutSuspect(int id)
        {
            var suspicion = await _context.Suspects.FindAsync(id);
            return suspicion == null
                ? new NotFoundObjectResult("The person is not suspected!")
                : suspicion.Suspicion;
        }

        public async Task<ActionResult> AddSuspect(SuspectDTO suspectDto)
        {
            if (suspectDto == null)
                return new BadRequestObjectResult("The suspect cannot be null!");
            var mappedSuspect = _mapper.Map<Suspect>(suspectDto);
            await _context.Suspects.AddAsync(mappedSuspect);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The suspect has been successfully added!");
        }

        public async Task<ActionResult> UpdateSuspect(int id, UpdateSuspectDTO updateSuspectDto)
        {
            if (updateSuspectDto == null)
                return new BadRequestObjectResult("The suspect cannot be null!");

            var dbSuspect = await _context.Suspects.FindAsync(id);
            if (dbSuspect == null)
                return new NotFoundObjectResult("The suspect does not exist!");

            dbSuspect.Name = updateSuspectDto.Name ?? dbSuspect.Name;
            dbSuspect.Gender = updateSuspectDto.Gender ?? dbSuspect.Gender;
            //dbSuspect.Occupation = updateSuspectDto.Occupation ?? dbSuspect.Occupation;
            dbSuspect.Status = updateSuspectDto.Status ?? dbSuspect.Status;
            dbSuspect.Residence = updateSuspectDto.Residence ?? dbSuspect.Residence;
            dbSuspect.MentalState = updateSuspectDto.MentalState ?? dbSuspect.MentalState;
            dbSuspect.Past = updateSuspectDto.Past ?? dbSuspect.Past;
            dbSuspect.Suspicion = updateSuspectDto.Suspicion ?? dbSuspect.Suspicion;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The suspect has been successfully updated!");
        }

        public async Task<ActionResult<string>> GetInfo(int id)
        {
            var dbSuspect = await _context.Suspects.FindAsync(id);
            return dbSuspect == null
                ? "The suspect does not exist!!"
                : $"Suspect: Name -> {dbSuspect.Name}, Profession -> {dbSuspect.Profession}, Residence -> {dbSuspect.Residence}, " +
                $"\nReason for suspicion -> {dbSuspect.Suspicion}.";
        }
    }
}
