using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;

namespace Pathfinding.Lib.Algorithms
{
    /// <summary>
    /// Represents a Pathfinding algorithm. (A*, Dijkstra's, ...)
    /// </summary>
    public interface IPathfindingAlgorithm
    {
        INode Resolve(IGotAStartAndAnEndOnAMap scen);
    }
}