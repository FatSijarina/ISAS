```

BenchmarkDotNet v0.13.12, Windows 11 (10.0.22631.3737/23H2/2023Update/SunValley3)
AMD Ryzen 5 5600H with Radeon Graphics, 1 CPU, 12 logical and 6 physical cores
.NET SDK 8.0.202
  [Host]     : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2
  Job-NMVQTQ : .NET 8.0.3 (8.0.324.11423), X64 RyuJIT AVX2

IterationCount=5  WarmupCount=2  

```
| Method               | Mean     | Error    | StdDev    | Gen0     | Gen1    | Allocated |
|--------------------- |---------:|---------:|----------:|---------:|--------:|----------:|
| AddEvidenceBenchmark | 3.588 ms | 3.751 ms | 0.5805 ms | 187.5000 | 15.6250 |   1.51 MB |
