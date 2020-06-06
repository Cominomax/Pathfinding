using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Algorithms
{
    /// <summary>
    /// Represents a Pathfinding algorithm. (A*, Dijkstra's, ...)
    /// </summary>
    public interface IPathfindingAlgorithm
    {
        INode Resolve(Scenario scen);
    }
}