using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps;

namespace Pathfinding.Lib.Scenarios
{
    /// <summary>
    /// Represents a running scenario of the algorithm.
    /// </summary>
    public class Scenario : IScenario
    {
        private ScenarioParams _params = new ScenarioParams();

        public bool IsSet { get; private set; } = false;
        public ScenarioResult Result { get; private set; }
        public IMap Map { get; private set; }
        public INode End => _params.End;
        public INode Start => _params.Start;

        public virtual MethodResult TrySetScenario(ScenarioParams @params)
        {
            IsSet = false;
            _params = @params;
            Map = MapSingleton.Instance.GetMap(_params.FilePath, _params.MapType);
            if (Map is EmptyMapWithError)
            {
                return new MethodResult(false, Map.ToString());
            }
            if (!Map.IsValidInMap(Start))
            {
                return new MethodResult(false, "Invalid Start Point. Is either on a position which it cannot be or out of the map");
            }
            if (!Map.IsValidInMap(End))
            {
                return new MethodResult(false, "Invalid Stop Point. Is either on a position which it cannot be or out of the map");
            }
            IsSet = true;
            return MethodResult.WithSuccess;
        }

        public virtual MethodResult RunScenario()
        {
            if (!IsSet)
            {
                return new MethodResult(false, "The scenario has not been set yet. Please call \'TrySetScenario()\' first.");
            }
            var resultNode = _params.Algorithm.Resolve(this);

            Result = new ScenarioResult()
            {
                PathLength = resultNode.DistanceFromOrigin,
                Path = resultNode.ToIEnumerable(),
            };
            return MethodResult.WithSuccess;
        }

        public virtual bool IsCompleted(INode node)
        {
            if (node.Equals(_params.End))
            {
                return true;
            }
            return false;
        }
    }
}