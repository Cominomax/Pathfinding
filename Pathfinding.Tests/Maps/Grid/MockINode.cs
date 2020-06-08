using Pathfinding.Lib.Maps.Utils;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Pathfinding.Lib.UnitTests.Maps.Grid
{
    public class MockINode : INode
    {
        public INode Parent => throw new NotImplementedException();

        public decimal DistanceFromOrigin => throw new NotImplementedException();

        public decimal F => throw new NotImplementedException();

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] INode other)
        {
            throw new NotImplementedException();
        }

        public void SetF(INode end)
        {
            throw new NotImplementedException();
        }

        public void SetParent(INode parent)
        {
            throw new NotImplementedException();
        }
    }
}
