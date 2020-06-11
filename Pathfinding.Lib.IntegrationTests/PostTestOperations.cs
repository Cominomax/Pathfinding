using Pathfinding.Lib.Scenarios.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.Environment;

namespace Pathfinding.Lib.IntegrationTests
{
    public class PostTestOperations
    {
        private static PostTestOperations _instance;

        private PostTestOperations()
        {

        }

        internal static PostTestOperations Instance
        {
            get 
            {
                if (_instance == null)
                {
                    _instance = new PostTestOperations();
                }
                return _instance; 
            }
        }

        public void FindAndExecute(string mapFilename, int amountOfScenarios, string methodName, List<ScenarioResult> resultList, Stopwatch timer, ProcessorUsageEnum processorUsage)
        {
            var methodInfo = GetType().GetMethod(methodName);

            if (methodInfo != null)
            {
                methodInfo.Invoke(this, new object[] { mapFilename, amountOfScenarios, resultList, timer, processorUsage });
            }
        }

        public void WriteResultsToFile(string mapFilename, int? amountOfScenarios, List<ScenarioResult> resultList, Stopwatch timer, ProcessorUsageEnum processorUsage)
        {
            var resultFilepath = Path.Combine(GetFolderPath(SpecialFolder.MyDocuments), "PathfindingData", "BaldursGate", "Results", mapFilename + ".result");
            using var streamWriter = new StreamWriter(new FileStream(resultFilepath, FileMode.Create));
            streamWriter.WriteLine($"Running date: {DateTime.Now}");
            streamWriter.WriteLine($"Runtime: {timer.Elapsed}");
            streamWriter.WriteLine($"Amount of scenario: {amountOfScenarios}");
            streamWriter.WriteLine($"Max number of processors used for scenario: {Environment.ProcessorCount / (int)processorUsage} ({processorUsage})");
            streamWriter.WriteLine();
            foreach (var result in resultList)
            {
                streamWriter.WriteLine($"{result.Description}");
                streamWriter.WriteLine();
            }

            streamWriter.Flush();
            streamWriter.Close();
        }
    }
}
