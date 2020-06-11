using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using Pathfinding.Lib.Utils;
using System;
using System.Threading.Tasks;

namespace Pathfinding.Lib.Scenarios
{
    /// <summary>
    /// Use Scenarios to run a single pathfinding scenario.
    /// </summary>
    public class ScenarioRunner
    {
        public Task<ScenarioResult> BeginRunScenario(IScenario scenario, IPathfindingAlgorithm Algorithm)
        {
            if (!scenario.IsSet)
            {
                return Task<ScenarioResult>.Factory.StartNew(() =>
                {
                    return CreateNonSetResult(scenario);
                });
            }

            var taskRun = Task<INode>.Factory.StartNew(() =>
            {
                return Algorithm.Resolve(scenario);
            });

            return taskRun.ContinueWith((prevTask) =>
            {
               return CreateResultFrom(scenario, taskRun.Result);
            });
        }

        private static ScenarioResult CreateResultFrom(IScenario scenario, INode taskResult)
        {
            var res = new ScenarioResult()
            {
                
                Name = scenario.Name,
                CorrectPathLength = scenario.ExpectedLength,
                PathLength = taskResult.DistanceFromOrigin,
                Path = taskResult.ToIEnumerable(),
            };
            res.Success = new DecimalPrecisionComparer().Equals(res.PathLength, scenario.ExpectedLength);
            if (!res.Success)
            {
                res.ErrorMessage = string.Join(Environment.NewLine,
                    $"Scenario {scenario.Name}: The path found was too different from the expected path",
                    $"The expected path length was:   {scenario.ExpectedLength}",
                    $"The calculated path length was: {res.PathLength}");
            }

            return res;
        }

        private static ScenarioResult CreateNonSetResult(IScenario scenario)
        {
            return new ScenarioResult()
            {
                Success = false,
                ErrorMessage = string.Join(Environment.NewLine,
                    $"Scenario {scenario.Name}: was not registered as Set. That could mean that the ",
                    "data of the scenario was outside of the boundaries of the map or that the map was",
                    "not found at the specified location:")
            };
        }
    }
}
