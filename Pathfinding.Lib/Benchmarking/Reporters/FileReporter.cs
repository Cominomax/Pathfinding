using Pathfinding.Lib.Scenarios.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Pathfinding.Lib.Extensions;

namespace Pathfinding.Lib.Benchmarking.Reporters
{
    public class FileReporter : IBenchmarkReporter
    {
        public void CreateReport(BenchmarkParameters bp, List<ScenarioResult> resultList, Stopwatch timer)
        {
            var resultFilepath = Path.Combine(bp.DestinationFolder, Path.GetFileName(bp.MapFilepath) + $"{DateTime.Now:yyyyMMddHHmmss}" + ".result");
            using var streamWriter = new StreamWriter(new FileStream(resultFilepath, FileMode.Create));

            if (!resultList.Any())
            {
                streamWriter.WriteLine($"{bp}");
                streamWriter.WriteLine($"Did not find any scenarios in: {bp.ScenarioFilepath}");
                streamWriter.Flush();
                streamWriter.Close();
                return;
            }

            var actualLength = resultList.Select(res => res.PathLength).Sum();
            var expectedLength = resultList.Select(res => res.CorrectPathLength).Sum();
            streamWriter.WriteLine($"{bp}");
            streamWriter.WriteLine($"The overall % increase of the result path length is: {actualLength.PercentageDifference(expectedLength)}%");
            streamWriter.WriteLine($"Runtime: {timer.Elapsed}");
            
            if (bp.WriteResultIntoReport)
            {
                streamWriter.WriteLine();
                foreach (var result in resultList)
                {
                    streamWriter.WriteLine($"{result}");
                    streamWriter.WriteLine();
                }
            }
            
            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
