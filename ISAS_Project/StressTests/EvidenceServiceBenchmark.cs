using AutoMapper;
using BenchmarkDotNet.Attributes;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Services.Implementation;
using Microsoft.EntityFrameworkCore;
using BenchmarkDotNet.Running;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StressTests
{
    [MemoryDiagnoser]
    [SimpleJob(iterationCount: 5, warmupCount: 2)]
    public class EvidenceServiceBenchmark
    {
        private readonly BiologicalEvidenceService _service;
        private readonly ILogger<EvidenceServiceBenchmark> _logger;

        public EvidenceServiceBenchmark()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(configure => configure.AddConsole())
                              .Configure<LoggerFilterOptions>(options => options.MinLevel = LogLevel.Information);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            _logger = loggerFactory.CreateLogger<EvidenceServiceBenchmark>();

            var options = new DbContextOptionsBuilder<ISASDbContext>()
                .UseInMemoryDatabase(databaseName: "BenchmarkDB")
                .Options;
            var context = new ISASDbContext(options);
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile(new MapperConfig())).CreateMapper();
            var evidenceLogger = loggerFactory.CreateLogger<BiologicalEvidenceService>();
            _service = new BiologicalEvidenceService(context, mapper, evidenceLogger);
        }

        [Benchmark]
        public async Task AddEvidenceBenchmark()
        {
            _logger.LogInformation("Starting AddEvidenceBenchmark");

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

            _logger.LogInformation("Completed AddEvidenceBenchmark");
        }
    }
}
