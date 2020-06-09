using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Running;
using System;
using System.IO;

namespace Pathfinding.Lib.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkGridScenario>(ManualConfig
                    .Create(DefaultConfig.Instance) 
                    .WithArtifactsPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Benchmark.Results")));
            //TODO: f
        }
    }
}
