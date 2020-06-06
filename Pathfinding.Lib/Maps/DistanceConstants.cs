using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding.Lib.Maps
{
    internal static class DistanceConstants
    {
        public static decimal MonodirectionalMove { get; } = 1m;

        public static decimal BidirectionalMove { get; } = (decimal)Math.Sqrt(2);
    }
}
