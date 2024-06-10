using ISAS_Project.DTOs;
using ISAS_Project.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ISAS_Project.Controllers
{
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IInvolvedParty _involvedParty;

        public PersonController(IInvolvedParty involvedParty)
        {
            _involvedParty = involvedParty;
        }

        [HttpDelete("person/{id}")]
        public async Task<ActionResult> DeletePerson(int id)
        {
            return await _involvedParty.DeletePerson(id);
        }

        [HttpGet("declarations-of-person/{id}")]
        public async Task<ActionResult<List<StatementDTO>>> GetDeclarationsOfPerson(int id)
        {
            return await _involvedParty.GetDeclarationsOfPerson(id);
        }

        [HttpPost("declaration")]
        public async Task<ActionResult> AddDeclaration(StatementDTO statementDTO)
        {
            return await _involvedParty.AddDeclaration(statementDTO);
        }

        [HttpPut("declaration/{id}")]
        public async Task<ActionResult> UpdateDeclaration(int id, UpdateStatementDTO updateStatementDTO)
        {
            return await _involvedParty.UpdateDeclaration(id, updateStatementDTO);
        }

        /*[HttpOptions("compare-declarations")]
        public async Task<string> Compare(int d1Id, int d2Id)
        {
            return await _involvedParty.Compare(d1Id, d2Id);
        }*/

        /*[HttpGet("biological-traces-of-person/{id}")]
        public async Task<ActionResult<List<BiologicalTraceDTO>>> GetBiologicalTracesOfPerson(int id)
        {
            return await _involvedParty.GetBiologicalTracesOfPerson(id);
        }*/

        [HttpPost("biological-trace")]
        public async Task<ActionResult> AddBiologicalTrace(BiologicalTraceDTO biologicalTraceDTO)
        {
            return await _involvedParty.AddBiologicalTrace(biologicalTraceDTO);
        }

        /*[HttpPut("biological-trace/{id}")]
        public async Task<ActionResult> UpdateBiologicalTrace(int id, UpdateBiologicalTraceDTO updateBiologicalTraceDTO)
        {
            return await _involvedParty.UpdateBiologicalTrace(id, updateBiologicalTraceDTO);
        }*/
    }
}
