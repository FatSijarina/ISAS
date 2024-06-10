using ISAS_Project.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Services.Interfaces
{
    public interface IStatementService
    {
        public Task<ActionResult<List<StatementDTO>>> GetDeclarations();
        public Task<ActionResult<StatementDTO>> GetDeclarationById(int id);
        public Task<ActionResult<string>> GetDeclarationContent(int id);
        public Task<ActionResult> DeleteDeclaration(int id);
        //public Task<string> CompareDeclarations(int d1Id, int d2Id);
    }
}
