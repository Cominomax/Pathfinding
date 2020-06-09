using BenchmarkDotNet.Running;

namespace Pathfinding.Lib.Benchmark
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<BenchmarkGridScenario>();
        }
    }
}
