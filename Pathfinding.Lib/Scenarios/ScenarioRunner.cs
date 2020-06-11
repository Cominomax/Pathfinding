using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using System;
using System.Threading.Tasks;

namespace Pathfinding.Lib.Scenarios
{
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
                Success = true,
                PathLength = taskResult.DistanceFromOrigin,
                Path = taskResult.ToIEnumerable(),
            };
            res.Description = string.Join(Environment.NewLine,
                    $"Scenario {scenario.Name}: going from {scenario.Start} to {scenario.End}.",
                    $"The result length of path is: {res.PathLength}.",
                    res.Path.ToCollectionString());
            return res;
        }

        private static ScenarioResult CreateNonSetResult(IScenario scenario)
        {
            return new ScenarioResult()
            {
                Description = string.Join(Environment.NewLine,
                    $"Scenario {scenario.Name}: was not registered as Set. That could mean that the ",
                    "data of the scenario was outside of the boundaries of the map or that the map was",
                    "not found at the specified location:")
            };
        }
    }
}
