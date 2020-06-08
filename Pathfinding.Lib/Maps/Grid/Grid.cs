using System;
using System.Collections.Generic;
using System.Linq;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Maps.Grid
{
    /// <summary>
    /// Class representation of a Grid Map (x, y axes) on which we are currently running a PathFinding Algorithm. 
    /// </summary>
    public class Grid : IMap
    {
        internal Grid(string filePath, int height, int width)
        {
            Origin = filePath;
            Height = height;
            Width = width;
            GridMap = new char[height][];
        }

        internal string Origin { get; }
        internal int Height { get; }
        internal int Width { get; }
        internal char[][] GridMap { get; }

        /// <summary>
        /// Possible steps for a grid include up, up-right, righ, down-right, down, down-left, left, up-left. Cannot move oblically if the parallel or perpendicular movement is blocked.
        /// </summary>
        /// <param name="s">starting node from which to start. Would be GridNode here.</param>
        /// <returns>IEnumerable of the possible steps.</returns>
        public IEnumerable<INode> GeneratePossibleStepsFrom(INode s)
        {
            var start = s as GridNode;
            var possibleSteps = new List<GridNode>();
            var allStepsFromStart = start.GenerateSteps();
            DirectionEnum possibleMoves = 0;
            foreach (var step in allStepsFromStart)
            {
                if (IsWithinGrid(step) && IsWalkable(step) && step.Direction.IsPossibleDirection(possibleMoves))
                {
                    possibleMoves |= step.Direction;
                    step.SetParent(start);
                    possibleSteps.Add(step);
                }
            }
            return possibleSteps;
        }

        /// <summary>
        /// Validates a node in the map (Is in the map and can walk on it)
        /// </summary>
        /// <param name="node">node to validate</param>
        /// <returns>True if valid, false if not.</returns>
        public bool IsValidInMap(INode node)
        {
            if (!(node is GridNode gridNode))
            {
                return false;
            }
            return IsWithinGrid(gridNode) && IsWalkable(gridNode);
        }

        private bool IsWithinGrid(GridNode item)
        {
            if (item.X >= 0 && item.X < Height && item.Y >= 0 && item.Y < Width)
            {
                return true;
            }
            return false;
        }

        private bool IsWalkable(GridNode item)
        {
            if (GridMap[item.X][item.Y] == '.')
            {
                return true;
            }
            return false;
        }

        public override string ToString() 
        {
            return $"File: {Origin} contains an grid of ({Height}x{Width})";
        }
    }
}
