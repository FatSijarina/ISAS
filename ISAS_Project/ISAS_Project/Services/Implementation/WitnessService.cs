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
    public class WitnessService : IWitnessService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public WitnessService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<WitnessDTO>>> GetWitnesses()
        {
            var witnesses = await _context.Witnesses.ToListAsync();
            if (!witnesses.Any())
                return new NotFoundObjectResult("There is no witness registered!!");

            var mappedWitnesses = _mapper.Map<List<WitnessDTO>>(witnesses);

            foreach (var mappedWitness in mappedWitnesses)
            {
                var id = mappedWitness.Id;

                var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();

                //mappedWitness.Declarations = _mapper.Map<List<DeclarationDTO>>(declarations);

                var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();

                mappedWitness.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);
            }
            return new OkObjectResult(mappedWitnesses);
        }

        public async Task<ActionResult> GetWitnessById(int id)
        {
            var witness = await _context.Witnesses.FindAsync(id);
            if (witness == null)
                return new NotFoundObjectResult("The witness does not exist!");

            var mappedWitness = _mapper.Map<WitnessDTO>(witness);
            var declarations = _context.Declarations.Where(d => d.PersonId == id).ToList();
            //mappedWitness.Declarations = _mapper.Map<List<DeclarationDTO>>(declarations);
            var biologicalTraces = _context.BiologicalTraces.Where(g => g.PersonId == id).ToList();
            mappedWitness.BiologicalTraces = _mapper.Map<List<BiologicalTraceDTO>>(biologicalTraces);

            return new OkObjectResult(mappedWitness);
        }

        public async Task<ActionResult<bool>> IsSuspected(int id)
        {
            var dbWitness = await _context.Witnesses.FindAsync(id);
            if (dbWitness == null)
                return new NotFoundObjectResult("The witness does not exist!!");
            return dbWitness.IsSuspected;
        }

        /*public async Task<ActionResult<bool>> IsObserved(int id)
        {
            var dbWitness = await _context.Witnesses.FindAsync(id);
            if (dbWitness == null)
                return new NotFoundObjectResult("The witness does not exist!!");
            return dbWitness.IsObserved;
        }*/

        public async Task<ActionResult> AddWitness(WitnessDTO witnessDTO)
        {
            if (witnessDTO == null)
                return new BadRequestObjectResult("The witness cannot be null!!");
            var mappedWitness = _mapper.Map<Witness>(witnessDTO);
            await _context.Witnesses.AddAsync(mappedWitness);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The witness was added successfully!");
        }

        public async Task<ActionResult> UpdateWitness(int id, UpdateWitnessDTO updateWitnessDTO)
        {
            if (updateWitnessDTO == null)
                return new BadRequestObjectResult("The witness cannot be null!");

            var dbWitness = await _context.Witnesses.FindAsync(id);
            if (dbWitness == null)
                return new NotFoundObjectResult("The witness does not exist!");

            dbWitness.Name = updateWitnessDTO.Name ?? dbWitness.Name;
            dbWitness.Gender = updateWitnessDTO.Gender ?? dbWitness.Gender;
            dbWitness.Profession = updateWitnessDTO.Profession ?? dbWitness.Profession;
            dbWitness.Status = updateWitnessDTO.Status ?? dbWitness.Status;
            dbWitness.Residence = updateWitnessDTO.Residence ?? dbWitness.Residence;
            dbWitness.MentalState = updateWitnessDTO.MentalState ?? dbWitness.MentalState;
            dbWitness.Past = updateWitnessDTO.Past ?? dbWitness.Past;
            dbWitness.RelationshipWithVictim = updateWitnessDTO.RelationshipWithVictim ?? dbWitness.RelationshipWithVictim;
            //dbWitness.IsObserved = updateWitnessDTO.IsObserved ?? dbWitness.IsObserved;
            dbWitness.IsSuspected = updateWitnessDTO.IsSuspected ?? dbWitness.IsSuspected;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The witness was successfully updated!");
        }
    }
}
