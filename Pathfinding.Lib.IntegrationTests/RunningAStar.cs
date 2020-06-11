using Xunit;
using static System.Environment;

using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Scenarios;
using Pathfinding.Lib.Scenarios.Base;
using System.Threading.Tasks;
using Pathfinding.Lib.Scenarios.FromFile;
using System.Collections.Concurrent;
using System.Linq;
using System.Diagnostics;
using Pathfinding.Lib.Heuristic;
using System.Collections.Generic;

namespace Pathfinding.Lib.IntegrationTests
{
    public class RunningAStar
    {
        public static IEnumerable<object[]> Data => new List<object[]>
        {
            new object[] { "AR0011SR.map", 400, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile) },
            new object[] { "AR0013SR.map", 400, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile) },
            new object[] { "AR0504SR.map", 400, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile) },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void RunScenariosForMap(string mapFilename, int amountOfScenarios = int.MaxValue, ProcessorUsageEnum processorUsage = ProcessorUsageEnum.Half, string toDoAfterRun = "")
        {
            //Arrange
            var fileToScenario = new FileToScenario(mapFilename);
            var scenarios = fileToScenario.CreateActionsFromFile(amountOfScenarios);
            int numOfConcurrent = ProcessorCount / (processorUsage == ProcessorUsageEnum.Single ? ProcessorCount : (int)processorUsage);

            //Act
            var timer = new Stopwatch();
            var taskList = new BlockingCollection<Task>();
            var resultList = new BlockingCollection<ScenarioResult>();
            timer.Restart();
            Parallel.ForEach(scenarios, new ParallelOptions { MaxDegreeOfParallelism = numOfConcurrent }, scen =>
            {
                new ScenarioRunner().BeginRunScenario(scen, new AStar(new DiagonalDistanceHeuristic()))
                    .ContinueWith((prevTask) => resultList.Add(prevTask.Result)).Wait();
            });
            var sortedList = resultList.ToList();
            sortedList.Sort();
            timer.Stop();

            if (!string.IsNullOrEmpty(toDoAfterRun))
            {
                PostTestOperations.Instance.FindAndExecute(mapFilename, amountOfScenarios, toDoAfterRun, sortedList, timer, processorUsage);
            }

            //Assert
            Assert.Equal(scenarios.Select(scen => scen.ExpectedLength), sortedList.Select(res => res.PathLength), new DecimalPrecisionComparer());
        }


    }
}
