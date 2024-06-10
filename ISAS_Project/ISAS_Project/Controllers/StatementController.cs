using ISAS_Project.DTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class StatementController : ControllerBase
    {
        private readonly IStatementService _statementService;

        public StatementController(IStatementService declarationService)
        {
            _statementService = declarationService;
        }

        [HttpGet("declarations")]
        public async Task<ActionResult<List<StatementDTO>>> GetDeclarations()
        {
            return await _statementService.GetDeclarations();
        }

        [HttpGet("declaration/{id}")]
        public async Task<ActionResult<StatementDTO>> GetDeclarationById(int id)
        {
            return await _statementService.GetDeclarationById(id);
        }

        [HttpGet("declaration-content/{id}")]
        public async Task<ActionResult<string>> GetDeclarationContent(int id)
        {
            return await _statementService.GetDeclarationContent(id);
        }

        [HttpDelete("declaration/{id}")]
        public async Task<ActionResult> DeleteDeclaration(int id)
        {
            return await _statementService.DeleteDeclaration(id);
        }
    }
}
