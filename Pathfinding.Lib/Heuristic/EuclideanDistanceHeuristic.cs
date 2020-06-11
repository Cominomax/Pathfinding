using Pathfinding.Lib.Maps.Utils;
using System;

namespace Pathfinding.Lib.Heuristic
{
    public class EuclideanDistanceHeuristic : IHeuristicCalculator
    {
        /// <summary>
        /// Calculates the F Value of the node. F is the distance between this node and the origin and the euclidian distance between this and the end. 
        /// </summary>
        /// <param name="start">INode implementor. Another instance of GridNode.</param>
        /// <param name="end">INode implementor. Another instance of GridNode.</param>
        public decimal CalculateHeuristic(INode start, INode end)
        {
            var distanceFromGoal = (decimal) Math.Sqrt((Math.Abs(start.X - end.X)*2) + (Math.Abs(start.Y - end.Y)*2));
            return start.DistanceFromOrigin + distanceFromGoal;
        }

        public string ToNiceString()
        {
            return "Euclidean Distance Heuristic";
        }
    }
}
