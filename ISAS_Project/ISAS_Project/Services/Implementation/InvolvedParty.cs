using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.PersonDTOs;
using ISAS_Project.DTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ISAS_Project.Services.Implementation
{
    public class InvolvedParty : IInvolvedParty
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        private readonly StatementService statementService;
        private readonly BiologicalTraceService biologicalTraceService;

        public InvolvedParty(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            statementService = new StatementService(_context, _mapper);
            biologicalTraceService = new BiologicalTraceService(_context, _mapper);
        }

        public async Task<ActionResult> DeletePerson(int id)
        {
            var dbPerson = await _context.Persons.FindAsync(id);
            if (dbPerson == null)
                return new NotFoundObjectResult("The person does not exist!!");

            _context.Persons.Remove(dbPerson);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The person has been successfully deleted!");
        }

        public async Task<ICollection<WitnessDTO>> GetWitnesses(int caseId) =>
            _mapper.Map<ICollection<WitnessDTO>>(await _context.Witnesses
                                .Where(p => p.CaseId == caseId)
                                .ToListAsync());

        public async Task<ICollection<SuspectDTO>> GetSuspects(int caseId) =>
            _mapper.Map<ICollection<SuspectDTO>>(await _context.Suspects
                                .Where(p => p.CaseId == caseId)
                                .ToListAsync());

        public async Task<ICollection<VictimDTO>> GetVictims(int caseId) =>
            _mapper.Map<ICollection<VictimDTO>>(await _context.Victims
                                    .Where(p => p.CaseId == caseId)
                                    .ToListAsync());

        public async Task<ActionResult<List<StatementDTO>>> GetDeclarationsOfPerson(int id) =>
            await statementService.GetPersonDeclarations(id);

        public async Task<ActionResult> AddDeclaration(StatementDTO declarationDTO) =>
            new OkObjectResult(await statementService.AddDeclaration(declarationDTO));

        public async Task<ActionResult> UpdateDeclaration(int id, UpdateStatementDTO updateStatementDTO) =>
            new OkObjectResult(await statementService.UpdateDeclaration(id, updateStatementDTO));

        public async Task<string> Compare(int d1Id, int d2Id) =>
            await statementService.Compare(d1Id, d2Id);

        public async Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTracesOfPerson(int id) =>
            await biologicalTraceService.GetPersonBiologicalTraces(id);

        public async Task<ActionResult> AddBiologicalTrace(BiologicalTraceDTO biologicalTraceDTO) =>
            new OkObjectResult(await biologicalTraceService.AddBiologicalTrace(biologicalTraceDTO));

        public async Task<ActionResult> UpdateBiologicalTrace(int id, UpdateBiologicalTraceDTO updateBiologicalTraceDTO) =>
            new OkObjectResult(await biologicalTraceService.UpdateBiologicalTrace(id, updateBiologicalTraceDTO));
    }
}
