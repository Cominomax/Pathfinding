using System;
using Xunit;

using System.IO;
using static System.Environment;

using Pathfinding.Lib.Maps.Grid;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Scenarios;
using Pathfinding.Lib.Scenarios.Base;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pathfinding.Lib.IntegrationTests
{
    public class RunningAStar
    {
        [Fact]
        public void RunScenariosForMap()
        {
            SetMapFileNames(out string mapFilepath, out string scenFilepath, out string resultFilepath);

            using var streamWriter = new StreamWriter(new FileStream(resultFilepath, FileMode.Create));
            List<Task> taskList = CreateActionsFromFile(scenFilepath, mapFilepath, streamWriter);
            Parallel.ForEach(taskList, new ParallelOptions { MaxDegreeOfParallelism = 10 }, i =>
            {
                i.Start();
            });
            Task.WaitAll(taskList.ToArray());
        }

        private List<Task> CreateActionsFromFile(string scenFilepath, string mapFilepath, StreamWriter streamWriter)
        {
            using var streamReader = new StreamReader(new FileStream(scenFilepath, FileMode.Open));
            string line = streamReader.ReadLine(); //skip first line of scenario file as useless.
            var scenarioParams = new ScenarioParams()
            {
                FilePath = mapFilepath,
                MapType = MapTypes.Grid
            };
            int i = 0;
            var taskList = new List<Task>();
            var _lock = new object();
            while (!streamReader.EndOfStream && i < 500)
            {
                var fileScenario = new FileScenario(streamReader.ReadLine().Split('\t'));
                scenarioParams.ScenarioName = i.ToString();
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

                var task = new ScenarioRunner().BeginRunScenario(scen, new AStar())
                    .ContinueWith((prevTask) => AssertResult(prevTask, fileScenario.ExpectedLength))
                    .ContinueWith((prevTask) => WriteResultsToFile(prevTask, scen, fileScenario.ExpectedLength, streamWriter, _lock));
                taskList.Add(task);
                i++;
            }

            return taskList;
        }

        private static void SetMapFileNames(out string mapFilepath, out string scenFilepath, out string resultFilepath)
        {
            var mapName = "AR0011SR.map";
            mapFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Maps", mapName);
            scenFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Scenarios", mapName + ".scen");
            resultFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Results", mapName + ".result");
        }

        private ScenarioResult WriteResultsToFile(Task<ScenarioResult> task, Scenario scen, decimal expectedLength, StreamWriter streamWriter, object @lock)
        {
            var result = task.Result;
            lock (@lock)
            { 
                streamWriter.WriteLine($"Scenario {scen.Name}: going from {scen.Start} to {scen.End}.");
                streamWriter.WriteLine($"The expected length of path is: {expectedLength}.");
                streamWriter.WriteLine($"The result length of path is:   {result.PathLength}.");
                streamWriter.WriteLine(result.Path.ToCollectionString());
                streamWriter.WriteLine();
                streamWriter.Flush();
            }
            return result;
        }

        private ScenarioResult AssertResult(Task<ScenarioResult> task, decimal expectedLength)
        {
            var result = task.Result;
            int precision = (int)1e-6;
            Assert.Equal(expectedLength, result.PathLength, precision);
            return result;
        }
    }
}
