using Pathfinding.Lib.Maps.Utils;
using System;

namespace Pathfinding.Lib.Heuristic
{
    public class ManhattanDistanceHeuristic : IHeuristicCalculator
    {
        /// <summary>
        /// Calculates the F Value of the node. F is the distance between this node and the origin and the manhattan distance between this and the end. 
        /// </summary>
        /// <param name="start">INode implementor. Another instance of GridNode.</param>
        /// <param name="end">INode implementor. Another instance of GridNode.</param>
        public decimal CalculateHeuristic(INode start, INode end)
        {
            var distanceFromGoal = Math.Abs(start.X - end.X) + Math.Abs(start.Y - end.Y);
            return start.DistanceFromOrigin + distanceFromGoal;
        }

        public string ToNiceString()
        {
            return "Manhattan Distance Heuristic";
        }
    }
}
