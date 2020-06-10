using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Pathfinding.Lib.Scenarios
{
    public class ScenarioRunner
    {
        public Task<ScenarioResult> BeginRunScenario(IScenario scenario, IPathfindingAlgorithm Algorithm)
        {
            
            var taskRun = Task<INode>.Factory.StartNew(() =>
            {
                
                return Algorithm.Resolve(scenario);
            });

            return taskRun.ContinueWith((prevTask) => 
            {
                var result = prevTask.Result;
                return new ScenarioResult()
                {
                    Success = true,
                    PathLength = prevTask.Result.DistanceFromOrigin,
                    Path = result.ToIEnumerable(),
                };
            });
        }
    }
}
