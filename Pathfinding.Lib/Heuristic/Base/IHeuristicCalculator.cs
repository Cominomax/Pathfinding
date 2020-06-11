using Pathfinding.Lib.Maps.Utils;
using Pathfinding.Lib.Utils;

namespace Pathfinding.Lib.Heuristic
{
    public interface IHeuristicCalculator : ICanBeANiceString
    {
        decimal CalculateHeuristic(INode start, INode end);
    }
}
