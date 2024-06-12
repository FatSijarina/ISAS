using AutoMapper;
using BenchmarkDotNet.Attributes;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using BenchmarkDotNet.Running;

namespace StressTests
{
    [MemoryDiagnoser]
    public class EvidenceServiceBenchmark
    {
        private readonly BiologicalEvidenceService _service;

        public EvidenceServiceBenchmark()
        {
            var options = new DbContextOptionsBuilder<ISASDbContext>()
                .UseInMemoryDatabase(databaseName: "BenchmarkDB")
                .Options;
            var context = new ISASDbContext(options);
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig())).CreateMapper();
            _service = new BiologicalEvidenceService(context, mapper, null);
        }

        [Benchmark]
        public async Task AddEvidenceBenchmark()
        {
            var evidenceDto = new BiologicalEvidenceDTO
            {
                Name = "Sample",
                ExtractionTime = DateTime.Now,
                Location = "Location",
                Attachment = "Attachment",
                ExtractionTechnique = "Technique",
                Specification = "Specification",
                Type = "Type",
                PersonId = 1
            };
            await _service.AddBiologicalEvidence(evidenceDto);
        }
    }
}
