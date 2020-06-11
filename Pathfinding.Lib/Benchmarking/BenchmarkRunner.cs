using static System.Environment;

using Pathfinding.Lib.Scenarios;
using Pathfinding.Lib.Scenarios.Base;
using System.Threading.Tasks;
using Pathfinding.Lib.Scenarios.FromFile;
using System.Collections.Concurrent;
using System.Linq;
using System.Diagnostics;
using Pathfinding.Lib.Utils;
using System.Collections.Generic;

namespace Pathfinding.Lib.Benchmarking
{
    /// <summary>
    /// Use the BenchmarkRunner to run and measure multiple scenario
    /// runs of the chosen algorithm with the chosen Heuristic on the chosen map
    /// </summary>
    public class BenchmarkRunner
    {
        public List<ScenarioResult> RunBenchmark(BenchmarkParameters bp)
        {
            var scenarios = new FileToScenarios().Convert(bp.MapFilepath, bp.ScenarioFilepath, bp.AmountOfScenarios);
            bp.AmountOfScenarios = scenarios.Count();
            int numOfConcurrent = ProcessorCount / (bp.ProcessorUsage == ProcessorUsageEnum.Single ? ProcessorCount : (int)bp.ProcessorUsage);
            var timer = new Stopwatch();
            var taskList = new BlockingCollection<Task>();
            var resultList = new BlockingCollection<ScenarioResult>();

            timer.Restart();
            Parallel.ForEach(scenarios, new ParallelOptions { MaxDegreeOfParallelism = numOfConcurrent }, scen =>
            {
                new ScenarioRunner().BeginRunScenario(scen, bp.Algorithm)
                    .ContinueWith((prevTask) => resultList.Add(prevTask.Result)).Wait();
            });
            var sortedList = resultList.ToList();
            sortedList.Sort();
            timer.Stop();

            bp.Reporter.CreateReport(bp, sortedList, timer);
            return sortedList;
        }
    }
}
