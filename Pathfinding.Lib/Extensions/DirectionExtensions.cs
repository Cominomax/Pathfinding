using Pathfinding.Lib.Maps.Grid;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding.Lib.Extensions
{
    public static class DirectionExtensions
    {
        /// <summary>
        /// If the calling DirectionEnum has more than one flag set, then verify that his directions are actually possible (cannot got upright if there is an obstacle on up OR right)
        /// </summary>
        /// <param name="direction">calling object</param>
        /// <param name="toCompare">possible directions (up, right, down, left) set to 1 if particular direction is possible</param>
        /// <returns>true if monodirectional or possible multidirectional. False if not possible multidirectional.</returns>
        public static bool IsPossibleDirection(this DirectionEnum direction, DirectionEnum toCompare)
        {
            if (!direction.HasMoreThan1Direction())
            {
                return true;
            }

            if ((toCompare & direction) == direction)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Verifies if the calling DirectionEnum has more than just one flag (multidirectional)
        /// </summary>
        /// <param name="direction">calling object</param>
        /// <returns>True if more than 1 flag, False if not.</returns>
        public static bool HasMoreThan1Direction(this DirectionEnum direction)
        {
            if ((direction & (direction - 1)) != 0)
            {
                return true;
            }
            return false;
        }
    }
}
