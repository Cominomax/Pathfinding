using System;
using Xunit;

using System.IO;
using static System.Environment;

using Pathfinding.Lib;
using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;

namespace Pathfinding.Lib.IntegrationTests
{
    public class RunningAStar
    {
        [Fact]
        public void RunScenariosForMap()
        {
            var mapName = "AR0011SR.map";
            var mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", mapName);
            var scenFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Scenarios", mapName + ".scen");
            var resultFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Results", mapName + ".result");

            using var streamReader = new StreamReader(new FileStream(scenFilepath, FileMode.Open));
            using var streamWriter = new StreamWriter(new FileStream(resultFilepath, FileMode.Create));
            string line = streamReader.ReadLine();
            var scenarioParams = new ScenarioParams()
            {
                FilePath = mapFilepath,
                MapType = MapTypes.Grid,
                Algorithm = new AStar()
            };

            int i = 0;
            while (!streamReader.EndOfStream && i < 50)
            {
                var fileScenario = new FileScenario(streamReader.ReadLine().Split('\t'));
                scenarioParams.Start = new GridNode(fileScenario.StartX, fileScenario.StartY);
                scenarioParams.End = new GridNode(fileScenario.EndX, fileScenario.EndY);
                var scen = new Scenario();
                var response = scen.TrySetScenario(scenarioParams);
                if (!response.Success)
                {
                    streamWriter.WriteLine($"Scenario {i}: going from ({fileScenario.StartX},{fileScenario.StartY}) to ({fileScenario.EndX}, { fileScenario.EndY}).");
                    streamWriter.WriteLine(response.ErrorMessage);
                    streamWriter.WriteLine();
                    i++;
                    continue;
                }

                response = scen.RunScenario();
                if (!response.Success)
                {
                    streamWriter.WriteLine($"Scenario {i}: going from ({fileScenario.StartX},{fileScenario.StartY}) to ({fileScenario.EndX}, { fileScenario.EndY}).");
                    streamWriter.WriteLine(response.ErrorMessage);
                    streamWriter.WriteLine();
                    i++;
                    continue;
                }

                streamWriter.WriteLine($"Scenario {i}: going from ({fileScenario.StartX},{fileScenario.StartY}) to ({fileScenario.EndX}, { fileScenario.EndY}).");
                streamWriter.WriteLine($"The expected length of path is {fileScenario.ExpectedLength}.");
                streamWriter.WriteLine($"The result length of path is {scen.Result.PathLength}.");
                streamWriter.WriteLine(scen.Result.Path.ToCollectionString());

                int precision = (int)1e-7;
                try
                {
                    Assert.Equal(fileScenario.ExpectedLength, scen.Result.PathLength, precision);
                }
                catch (Exception ex)
                {
                    streamWriter.WriteLine(ex.ToString());
                }
                streamWriter.WriteLine();
                streamWriter.Flush();

                i++;
            }
        }
    }
}
