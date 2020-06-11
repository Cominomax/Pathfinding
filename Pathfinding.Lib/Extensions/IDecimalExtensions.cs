using System;

namespace Pathfinding.Lib.Extensions
{
    public static class IDecimalExtensions
    {
        public static decimal PercentageDifference(this decimal @this, decimal expected)
        {
            return ( (@this - expected) / Math.Abs(expected)) * 100;
        }
    }
}
