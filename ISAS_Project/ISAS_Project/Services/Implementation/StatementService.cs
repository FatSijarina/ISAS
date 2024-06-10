using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs;
using ISAS_Project.Models;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ISAS_Project.Services.Implementation
{
    public class StatementService : IStatementService
    {
        private readonly ISASDbContext _context;
        private readonly IMapper _mapper;

        public StatementService(ISASDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<StatementDTO>>> GetDeclarations() =>
    _mapper.Map<List<StatementDTO>>(await _context.Declarations.ToListAsync());

        public async Task<ActionResult<StatementDTO>> GetDeclarationById(int id)
        {
            var mappedDeclaration = _mapper.Map<StatementDTO>(await _context.Declarations.FindAsync(id));
            return mappedDeclaration == null
                ? new NotFoundObjectResult("The declaration does not exist!")
                : new OkObjectResult(mappedDeclaration);
        }

        public async Task<ActionResult<List<StatementDTO>>> GetPersonDeclarations(int id)
        {
            var dbPerson = await _context.Persons.FindAsync(id);
            return dbPerson == null
                ? new NotFoundObjectResult("The person does not exist!")
                : _mapper.Map<List<StatementDTO>>(await _context.Declarations
                                .Where(p => p.Person.Id == id)
                                .ToListAsync());
        }

        public async Task<ActionResult<string>> GetDeclarationContent(int id)
        {
            var dbContent = await _context.Declarations.FindAsync(id);
            return dbContent == null
                ? new NotFoundObjectResult("The declaration does not exist!")
                : dbContent.Content;
        }

        public async Task<ActionResult> AddDeclaration(StatementDTO declarationDTO)
        {
            if (declarationDTO == null)
                return new BadRequestObjectResult("The declaration cannot be null!");
            var mappedDeclaration = _mapper.Map<Statement>(declarationDTO);
            await _context.Declarations.AddAsync(mappedDeclaration);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The declaration has been successfully added!");
        }

        public async Task<ActionResult> UpdateDeclaration(int id, UpdateStatementDTO updateDeclarationDTO)
        {
            if (updateDeclarationDTO == null)
                return new BadRequestObjectResult("The declaration cannot be null!");

            var dbDeclaration = await _context.Declarations.FindAsync(id);
            if (dbDeclaration == null)
                return new NotFoundObjectResult("The declaration does not exist!");

            //dbDeclaration.ReceptionTime = updateDeclarationDTO.ReceptionTime ?? dbDeclaration.ReceptionTime;
            dbDeclaration.Content = updateDeclarationDTO.Content ?? dbDeclaration.Content;
            await _context.SaveChangesAsync();

            return new OkObjectResult("The declaration has been successfully updated!");
        }

        public async Task<ActionResult> DeleteDeclaration(int id)
        {
            var dbDeclaration = await _context.Declarations.FindAsync(id);
            if (dbDeclaration == null)
                return new NotFoundObjectResult("The declaration does not exist!");

            _context.Declarations.Remove(dbDeclaration);
            await _context.SaveChangesAsync();
            return new OkObjectResult("The declaration has been successfully deleted!");
        }
    }
}
