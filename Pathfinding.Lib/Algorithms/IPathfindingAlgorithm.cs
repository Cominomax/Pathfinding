using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios;

namespace Pathfinding.Lib.Algorithms
{
    /// <summary>
    /// Represents a Pathfinding algorithm. (A*, Dijkstra's, ...)
    /// </summary>
    public interface IPathfindingAlgorithm
    {
        INode Resolve(IScenario scen);
    }
}