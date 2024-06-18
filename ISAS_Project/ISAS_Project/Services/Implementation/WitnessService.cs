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

                mappedWitness.Statements = _mapper.Map<List<StatementDTO>>(declarations);

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
            mappedWitness.Statements = _mapper.Map<List<StatementDTO>>(declarations);
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

        public async Task<ActionResult<bool>> IsObserved(int id)
        {
            var dbWitness = await _context.Witnesses.FindAsync(id);
            if (dbWitness == null)
                return new NotFoundObjectResult("The witness does not exist!!");
            return dbWitness.IsMonitored;
        }

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
            dbWitness.IsMonitored = updateWitnessDTO.IsMonitored ?? dbWitness.IsMonitored;
            dbWitness.IsSuspected = updateWitnessDTO.IsSuspected ?? dbWitness.IsSuspected;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The witness was successfully updated!");
        }

        public async Task<ActionResult<string>> GetInfo(int id)
        {
            var dbWitness = await _context.Witnesses.FindAsync(id);
            return dbWitness == null
                ? "The witness does not exist!!"
                : $"Witness: Name -> {dbWitness.Name}, Profession -> {dbWitness.Profession}, Residence -> {dbWitness.Residence}, " +
                $"\nRelationship with the victim -> {dbWitness.RelationshipWithVictim}, Is under surveillance?? " +
                $"{dbWitness.IsMonitored}, Is suspected?? {dbWitness.IsSuspected}!";
        }

        public async Task<ActionResult> SaveAsSuspect(int id)
        {
            var d = await GetWitnessById(id);
            if (d is NotFoundObjectResult)
                return d;

            WitnessDTO? witness = ((OkObjectResult)d).Value as WitnessDTO;
            await SetAsSuspect(id);
            SuspectDTO suspect = ConvertToSuspect(witness);
            await AddSuspect(suspect);
            return new OkObjectResult(suspect);
        }

        private async Task SetAsSuspect(int id)
        {
            var dbWitness = await _context.Witnesses.FindAsync(id);
            if (dbWitness == null)
                return;
            dbWitness.IsSuspected = true;
            await _context.SaveChangesAsync();
        }

        // This method is called by the SaveAsSuspect method to convert a witness to a suspect.
        private SuspectDTO ConvertToSuspect(WitnessDTO witness)
        {
            // Create an object of type SuspectDTO 
            // and initialize it immediately to have the same data as the witness
            // except that the Suspicion should be changed later.
            SuspectDTO suspect = new()
            {
                Name = witness.Name,
                Gender = witness.Gender,
                Profession = witness.Profession,
                Status = witness.Status,
                Residence = witness.Residence,
                MentalState = witness.MentalState,
                Past = witness.Past,
                Suspicion = "Suspicion formulated meanwhile..."
            };

            return suspect;
        }

        // This method is called by the SaveAsSuspect method to add a suspect.
        private async Task AddSuspect(SuspectDTO suspect)
        {
            // Create an instance of the SuspectService class to use the AddSuspect method
            // so that the same logic is not implemented in two different classes!
            SuspectService s = new(_context, _mapper);
            await s.AddSuspect(suspect);
        }
    }
}
