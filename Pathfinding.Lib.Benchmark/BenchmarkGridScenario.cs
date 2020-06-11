using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Exporters;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Benchmarking;
using Pathfinding.Lib.Heuristic;
using Pathfinding.Lib.Scenarios;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static System.Environment;

namespace Pathfinding.Lib.Benchmark
{
    [MemoryDiagnoser]
    [HtmlExporter]
    public class BenchmarkGridScenario
    {
        private const string Map1ForBenchmark = "AR0011SR.map";
        private const string Map2ForBenchmark = "AR0531SR.map";
        private const string Map3ForBenchmark = "AR0704SR.map";

        [GlobalSetup]
        public void BenchmarkGridScenarioSetUp()
        {
            BP = new BenchmarkParameters
            {
                MapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", MapName),
                ScenarioFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Scenarios", MapName + ".scen"),
                AmountOfScenarios = 50,
                WriteResultIntoReport = true
            };
        }

        [Params(Map1ForBenchmark, Map2ForBenchmark, Map3ForBenchmark)]
        public string MapName { get; set; }

        public BenchmarkParameters BP { get; set; }

        [Benchmark(Baseline = true)]
        public void RunAStarWithDiagonalHeuristic()
        {
            BP.Heuristic = new DiagonalDistanceHeuristic();
            BP.Algorithm = new AStar(BP.Heuristic);

            new BenchmarkRunner().RunBenchmark(BP);
        }

        [Benchmark()]
        public void RunAStarWithManhattanHeuristic()
        {
            BP.Heuristic = new ManhattanDistanceHeuristic();
            BP.Algorithm = new AStar(BP.Heuristic);

            new BenchmarkRunner().RunBenchmark(BP);
        }

        [Benchmark()]
        public void RunAStarWithEuclidianHeuristic()
        {
            BP.Heuristic = new EuclideanDistanceHeuristic();
            BP.Algorithm = new AStar(BP.Heuristic);

            new BenchmarkRunner().RunBenchmark(BP);
        }
    }
}
