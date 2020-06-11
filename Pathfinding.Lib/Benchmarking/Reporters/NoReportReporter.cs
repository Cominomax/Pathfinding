using Pathfinding.Lib.Scenarios.Base;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.Lib.Benchmarking.Reporters
{
    public class NoReportReporter : IBenchmarkReporter
    {
        public void CreateReport(BenchmarkParameters bp, List<ScenarioResult> sortedList, Stopwatch timer)
        {
        }
    }
}
