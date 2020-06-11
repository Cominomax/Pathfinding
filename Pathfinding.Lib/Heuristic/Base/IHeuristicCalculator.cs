using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Heuristic
{
    public interface IHeuristicCalculator
    {
        decimal CalculateHeuristic(INode start, INode end);
    }
}
