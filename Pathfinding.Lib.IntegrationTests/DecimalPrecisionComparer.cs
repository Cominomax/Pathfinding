using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Pathfinding.Lib.IntegrationTests
{
    public class DecimalPrecisionComparer : IEqualityComparer<decimal>
    {
        private readonly decimal _precision;
        public DecimalPrecisionComparer(decimal precision = 0.000001m)
        {
            _precision = precision;
        }

        public bool Equals([AllowNull] decimal x, [AllowNull] decimal y)
        {
            if (Math.Abs(x - y) < _precision)
            {
                return true;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] decimal obj)
        {
            return obj.GetHashCode();
        }
    }
}
