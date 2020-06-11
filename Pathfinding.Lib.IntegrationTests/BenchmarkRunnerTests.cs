using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Benchmarking;
using Pathfinding.Lib.Benchmarking.Reporters;
using Pathfinding.Lib.Heuristic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xunit;
using static System.Environment;

namespace Pathfinding.Lib.IntegrationTests
{
    public class BenchmarkRunnerTests
    {
        public string MapName { get; set; } = "AR0201SR.map";

        [Fact]
        public void BenchmarkRunner_WithAStarDiagonalDistance_ShouldRunAllScenariosCorrectly()
        {
            //Arrange
            var bp = new BenchmarkParameters
            {
                MapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", MapName),
                ScenarioFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Scenarios", MapName + ".scen"),
                AmountOfScenarios = 300,
                WriteResultIntoReport = true,
                Heuristic = new DiagonalDistanceHeuristic(),
                Reporter = new FileReporter()
            };
            bp.Algorithm = new AStar(bp.Heuristic);

            //Act
            var testResult = new BenchmarkRunner().RunBenchmark(bp);

            //Assert
            Assert.True(testResult.All(res => res.Success));
        }
    }
}
