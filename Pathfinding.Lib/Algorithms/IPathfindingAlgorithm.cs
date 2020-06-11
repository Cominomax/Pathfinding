using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Scenarios.Base;
using Pathfinding.Lib.Utils;

namespace Pathfinding.Lib.Algorithms
{
    /// <summary>
    /// Represents a Pathfinding algorithm. (A*, Dijkstra's, ...)
    /// </summary>
    public interface IPathfindingAlgorithm : ICanBeANiceString
    {
        INode Resolve(IGotAStartAndAnEndOnAMap scen);
    }
}