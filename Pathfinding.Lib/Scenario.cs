using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps;

namespace Pathfinding.Lib
{
    /// <summary>
    /// Represents a running scenario of the algorithm.
    /// </summary>
    public class Scenario : IScenario
    {
        public ScenarioParams Params { get; private set; }
        public bool IsSet { get; private set; } = false;
        public IMap Map { get; private set; }
        public ScenarioResult Result { get; set; }

        public MethodResult TrySetScenario(ScenarioParams @params)
        {
            IsSet = false;
            Params = @params;
            Map = MapSingleton.Instance.GetMap(Params.FilePath, Params.MapType);
            if (Map is EmptyMapWithError)
            {
                return new MethodResult(false, Map.ToString());
            }
            if (!Map.IsValidInMap(Params.Start))
            {
                return new MethodResult(false, "Invalid Start Point. Is either on a position which it cannot be or out of the map");
            }
            if (!Map.IsValidInMap(Params.End))
            {
                return new MethodResult(false, "Invalid Stop Point. Is either on a position which it cannot be or out of the map");
            }
            IsSet = true;
            return MethodResult.WithSuccess;
        }

        public MethodResult RunScenario()
        {
            if (!IsSet)
            {
                return new MethodResult(false, "The scenario has not been set yet. Please call \'TrySetScenario()\' first.");
            }
            var resultNode = Params.Algorithm.Resolve(this);

            Result = new ScenarioResult()
            {
                PathLength = resultNode.DistanceFromOrigin,
                Path = resultNode.ToIEnumerable(),
            };
            return MethodResult.WithSuccess;
        }

        public bool IsCompleted(INode node)
        {
            if (node.Equals(Params.End))
            {
                return true;
            }
            return false;
        }
    }
}