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

namespace Pathfinding.Lib.IntegrationTests
{
    public class RunningAStar
    {
        [Theory]
        [InlineData("AR0011SR.map", 300, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile))]
        [InlineData("AR0013SR.map", 300, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile))]
        [InlineData("AR0504SR.map", 300, ProcessorUsageEnum.Half, nameof(PostTestOperations.WriteResultsToFile))]
        public void RunScenariosForMap(string mapFilename, int amountOfScenarios = int.MaxValue, ProcessorUsageEnum processorUsage = ProcessorUsageEnum.Half, string toDoAfterRun = "")
        {
            //Arrange
            var fileToScenario = new FileToScenario(mapFilename);
            var scenarios = fileToScenario.CreateActionsFromFile(amountOfScenarios);

            //Act
            var timer = new Stopwatch();
            var taskList = new BlockingCollection<Task>();
            var resultList = new BlockingCollection<ScenarioResult>();
            timer.Restart();
            Parallel.ForEach(scenarios, new ParallelOptions { MaxDegreeOfParallelism = (ProcessorCount / (int)processorUsage) }, scen =>
            {
                new ScenarioRunner().BeginRunScenario(scen, new AStar())
                    .ContinueWith((prevTask) => resultList.Add(prevTask.Result)).Wait();
            });
            var sortedList = resultList.ToList();
            sortedList.Sort();
            timer.Stop();

            //Assert
            Assert.Equal(scenarios.Select(scen => scen.ExpectedLength), sortedList.Select(res => res.PathLength), new DecimalPrecisionComparer());
            if (!string.IsNullOrEmpty(toDoAfterRun))
            {
                PostTestOperations.Instance.FindAndExecute(mapFilename, amountOfScenarios, toDoAfterRun, sortedList, timer, processorUsage);
            }
        }
    }
}
