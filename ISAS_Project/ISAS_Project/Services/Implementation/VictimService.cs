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
    public class VictimService : IVictimService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public VictimService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<VictimDTO>>> GetVictims()
        {
            var victims = await _context.Victims.ToListAsync();
            if (!victims.Any())
                return new NotFoundObjectResult("No victims registered!");

            var mappedVictims = _mapper.Map<List<VictimDTO>>(victims);

            foreach (var mappedVictim in mappedVictims)
            {
                var id = mappedVictim.Id;
                var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();
                mappedVictim.Statements = _mapper.Map<List<StatementDTO>>(declarations);
                var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();
                mappedVictim.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);
            }

            return new OkObjectResult(mappedVictims);
        }

        public async Task<ActionResult> GetVictimById(int id)
        {
            var victim = await _context.Victims.FindAsync(id);
            if (victim == null)
                return new NotFoundObjectResult("The victim does not exist!");

            var mappedVictim = _mapper.Map<VictimDTO>(victim);
            var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();
            mappedVictim.Statements = _mapper.Map<List<StatementDTO>>(declarations);
            var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();
            mappedVictim.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);

            return new OkObjectResult(mappedVictim);
        }

        public async Task<ActionResult> AddVictim(VictimDTO victimDTO)
        {
            if (victimDTO == null)
                return new BadRequestObjectResult("The victim cannot be null!!");
            var mappedVictim = _mapper.Map<Victim>(victimDTO);
            await _context.Victims.AddAsync(mappedVictim);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The victim has been successfully added!");
        }

        public async Task<ActionResult> UpdateVictim(int id, UpdateVictimDTO updateVictimDTO)
        {
            if (updateVictimDTO == null)
                return new BadRequestObjectResult("The victim cannot be null!!");

            var dbVictim = await _context.Victims.FindAsync(id);
            if (dbVictim == null)
                return new NotFoundObjectResult("The victim does not exist!!");

            dbVictim.Name = updateVictimDTO.Name ?? dbVictim.Name;
            dbVictim.Gender = updateVictimDTO.Gender ?? dbVictim.Gender;
            //dbVictim.Occupation = updateVictimDTO.Occupation ?? dbVictim.Occupation;
            dbVictim.Status = updateVictimDTO.Status ?? dbVictim.Status;
            dbVictim.Residence = updateVictimDTO.Residence ?? dbVictim.Residence;
            dbVictim.MentalState = updateVictimDTO.MentalState ?? dbVictim.MentalState;
            dbVictim.Past = updateVictimDTO.Past ?? dbVictim.Past;
            //dbVictim.Place = updateVictimDTO.Place ?? dbVictim.Place;
            dbVictim.Time = updateVictimDTO.Time ?? dbVictim.Time;
            dbVictim.Method = updateVictimDTO.Method ?? dbVictim.Method;
            dbVictim.Condition = updateVictimDTO.Condition ?? dbVictim.Condition;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The victim has been successfully updated!");
        }

        public async Task<ActionResult<string>> GetInfo(int id)
        {
            var dbVictim = await _context.Victims.FindAsync(id);
            return dbVictim == null
                ? "The victim does not exist!!"
                : $"Victim: Name -> " + dbVictim.Name + ", Occupation -> " /*+ dbVictim.Occupation*/
                + ", Residence -> " + dbVictim.Residence + ", "
                /*+ "\nWhere found? " + dbVictim.Place*/
                + ",\nWhen found? " + dbVictim.Time
                + ",\nHow found? " + dbVictim.Method
                + ",\nIn what condition found? " + dbVictim.Condition + ".";
        }
    }
}
