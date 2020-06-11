using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Benchmarking.Reporters;
using Pathfinding.Lib.Heuristic;
using Pathfinding.Lib.Utils;
using System;
using System.IO;

namespace Pathfinding.Lib.Benchmarking
{
    public class BenchmarkParameters
    {
        private string _destinationDirectory;

        public string MapFilepath { get; set; }
        public string ScenarioFilepath { get; set; }
        public IPathfindingAlgorithm Algorithm { get; set; } 
        public IHeuristicCalculator Heuristic { get; set; }
        public int AmountOfScenarios { get; set; } = int.MaxValue;
        public ProcessorUsageEnum ProcessorUsage { get; set; } = ProcessorUsageEnum.Half;
        public IBenchmarkReporter Reporter { get; set; } = new NoReportReporter();
     
        public string DestinationFolder
        {
            get 
            {
                if (string.IsNullOrEmpty(_destinationDirectory))
                {
                    return Path.Combine(new DirectoryInfo(MapFilepath).Parent.Parent.FullName, "Results");
                }
                return _destinationDirectory; 
            }
            set { _destinationDirectory = value; }
        }

        public bool WriteResultIntoReport { get; set; } = true;

        public override string ToString()
        {
            return string.Join(Environment.NewLine,
                $"Benchmark run on the {DateTime.Now}:",
                $"Consisting of {AmountOfScenarios} scenarios ran with {Algorithm.ToNiceString()} set with the {Heuristic.ToNiceString()}",
                $"on the map: {MapFilepath}",
                $"Max number of processors used for scenario: {Environment.ProcessorCount / (int)ProcessorUsage} ({ProcessorUsage})"
                ) ;
        }
    }
}
