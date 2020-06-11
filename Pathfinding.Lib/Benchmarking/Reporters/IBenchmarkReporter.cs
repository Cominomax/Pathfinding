using Pathfinding.Lib.Scenarios.Base;
using System.Collections.Generic;
using System.Diagnostics;

namespace Pathfinding.Lib.Benchmarking.Reporters
{
    public interface IBenchmarkReporter
    {
        void CreateReport(BenchmarkParameters bp, List<ScenarioResult> sortedList, Stopwatch timer);
    }
}
