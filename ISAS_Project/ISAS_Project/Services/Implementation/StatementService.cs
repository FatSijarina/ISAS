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

            dbDeclaration.TimeOfReceipt = updateDeclarationDTO.TimeTaken ?? dbDeclaration.TimeOfReceipt;
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

        public async Task<string> Compare(int d1Id, int d2Id)
        {
            Statement d1 = await _context.Declarations.FindAsync(d1Id);
            Statement d2 = await _context.Declarations.FindAsync(d2Id);

            if (d1 == null)
                return "The first declaration is empty!!";
            if (d2 == null)
                return "The second declaration is empty!!";

            var deklaration1 = (await GetDeclarationContent(d1Id)).Value;
            var deklaration2 = (await GetDeclarationContent(d2Id)).Value;

            if (d1.PersonId != d2.PersonId)
                return "The declarations are not from the same person!!";

            // API request payload - commented out
            /*
            string API_KEY = "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";
            string endpoint = "https://api.openai.com/v1/models/text-davinci-002/engines/text-curie/jobs";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {API_KEY}");

            // Define the input for the API
            var input = new
            {
                prompt = "Compare the semantics of the following two strings:",
                strings = new[] { deklarata1, deklarata2 }
            };

            // Serialize the input to a JSON string
            var content = new StringContent(JsonConvert.SerializeObject(input), Encoding.UTF8, "application/json");

            // Make the API request
            var response = await client.PostAsync(endpoint, content);

            // Check if the API call was successful
            if (response.IsSuccessStatusCode)
            {
                // Read the response content
                var responseContent = await response.Content.ReadAsStringAsync();

                // Deserialize the response content to a dynamic object
                dynamic responseData = JsonConvert.DeserializeObject(responseContent);

                // Extract the comparison result from the response
                bool isSemanticMatch = responseData.result;

                return (isSemanticMatch ? "The semantics of the two strings are the same." : "The semantics of the two strings are different.");
            }
            else
            {
                return ($"The API request failed with status code {(int)response.StatusCode}.");
            }
            */

            string[] str1Words = deklaration1.ToLower().Split(' ');
            string[] str2Words = deklaration2.ToLower().Split(' ');
            var uniqueWords = str2Words.Except(str1Words).ToList();

            return "First declaration -> " + deklaration1 + "\n"
                + "Differences in the second declaration from the first -> "
                + $"{String.Join(" ", uniqueWords)}";
        }
    }
}
