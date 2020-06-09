using BenchmarkDotNet.Attributes;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using System.Collections.Generic;
using System.IO;
using static System.Environment;

namespace Pathfinding.Lib.Benchmark
{
    [MemoryDiagnoser]
    [HtmlExporter]
    public class BenchmarkGridScenario
    {
        private readonly Dictionary<string, ScenarioParams[]> _scenarioNodes;
        private const string Map1ForBenchmark = "AR0011SR.map";
        private const string Map2ForBenchmark = "AR0012SR.map";
        private const string Map3ForBenchmark = "AR0013SR.map";
        public BenchmarkGridScenario()
        {
            _scenarioNodes = new Dictionary<string, ScenarioParams[]>()
            {
                {Map1ForBenchmark, GetScenarioParamsForAR0011SR() },
                {Map2ForBenchmark, GetScenarioParamsForAR0012SR() },
                {Map3ForBenchmark, GetScenarioParamsForAR0013SR() }
            };
        }

        [Params(Map1ForBenchmark, Map2ForBenchmark, Map3ForBenchmark)]
        public string MapNames { get; set; }

        [Benchmark(Baseline = true)]
        public void RunSyncScenarios()
        {
            var scenarioParams = _scenarioNodes[MapNames];
            foreach (var parameters in scenarioParams)
            {
                var scenario = new Scenario();
                scenario.TrySetScenario(parameters);
                scenario.RunScenario();
            }
        }

        [Benchmark()]
        public void RunTimedScenarios()
        {
            var scenarioParams = _scenarioNodes[MapNames];
            foreach (var parameters in scenarioParams)
            {
                var scenario = new TimedScenario<Scenario>(new Scenario());
                scenario.TrySetScenario(parameters);
                scenario.RunScenario();
            }
        }

        private static ScenarioParams[] GetScenarioParamsForAR0011SR()
        {
            var mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", Map1ForBenchmark);
            return new ScenarioParams[]
            {
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(82, 64), End = new GridNode(38, 126) },
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(103, 63), End = new GridNode(122, 158) },     
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(64, 59), End = new GridNode(161, 114) },      
            };
        }

        private static ScenarioParams[] GetScenarioParamsForAR0012SR()
        {
            var mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", Map2ForBenchmark);
            return new ScenarioParams[]
            {
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(23, 67), End = new GridNode(89, 125) },      
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(132, 95), End = new GridNode(28, 49) },      
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(66, 34), End = new GridNode(135, 81) },     
            };
        }

        private static ScenarioParams[] GetScenarioParamsForAR0013SR()
        {
            var mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", Map3ForBenchmark);
            return new ScenarioParams[]
            {
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(87, 23), End = new GridNode(81, 134) },      
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(84, 26), End = new GridNode(92, 135) },				
                new ScenarioParams() { FilePath = mapFilepath, MapType = MapTypes.Grid, Algorithm = new AStar(), Start = new GridNode(76, 10), End = new GridNode(101, 123) },			
            };
        }
    }
}
