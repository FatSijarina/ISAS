using AutoMapper;
using ISAS_Project.Configurations;
using ISAS_Project.DTOs.EvidenceDTOs;
using ISAS_Project.Models;
using ISAS_Project.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using NBench;

namespace StressTests
{
    internal class EvidenceStressTest
    {
        Counter testCounter;
        PhysicalEvidenceService evidence;

        [PerfSetup]
        public void Setup(BenchmarkContext context)
        {
            var serviceProvider = new ServiceCollection()
                .AddAutoMapper(typeof(EvidenceStressTest))
                .AddHttpClient()
                .BuildServiceProvider();

            var dataContext = serviceProvider.GetService<ISASDbContext>();
            var mapper = serviceProvider.GetService<IMapper>();

            evidence = new PhysicalEvidenceService(dataContext, mapper);

            testCounter = context.GetCounter("EvidencesCounter");
        }


        [PerfBenchmark(NumberOfIterations = 5,
            RunMode = RunMode.Throughput,
            RunTimeMilliseconds = 2000,
            TestMode = TestMode.Test)]

        [CounterThroughputAssertion("EvidencesCounter", MustBe.GreaterThan, 90000)]

        [MemoryAssertion(MemoryMetric.TotalBytesAllocated, MustBe.LessThanOrEqualTo, ByteConstants.SixtyFourKb)]
        public void Evidence_Test()
        {

            evidence.AddPhysicalEvidence(
                new PhysicalEvidenceDTO
                {
                    Name = "Prova 1",
                    ExtractionTime = DateTime.Now,
                    Attachment = "Attachment 1",
                    PersonId = 1,
                    UsedInCrime = true,
                    DangerLevel = "Medium",
                    Classification = "Classification 1",
                    RequiresExamination = true,
                    HasBiologicalTraces = true
                });

            testCounter.Increment();
        }
    }
}
