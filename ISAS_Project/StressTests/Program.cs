using BenchmarkDotNet.Running;
using NBench;
using StressTests;

class Program
{
    static void Main(string[] args)
    {
        // Run NBench tests
        Console.WriteLine("Running NBench tests...");
        var nbenchResults = NBenchRunner.Run<Program>();
        Console.WriteLine("NBench tests completed.");

        // Run BenchmarkDotNet tests
        Console.WriteLine("Running BenchmarkDotNet tests...");
        var benchmarkResults = BenchmarkRunner.Run<EvidenceServiceBenchmark>();
        Console.WriteLine("BenchmarkDotNet tests completed.");

        string resultsDirectory = Path.Combine(Directory.GetCurrentDirectory(), "BenchmarkDotNet.Artifacts", "results");
        Console.WriteLine($"BenchmarkDotNet results are available in: {resultsDirectory}");
    }
}