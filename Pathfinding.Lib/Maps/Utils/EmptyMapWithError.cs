using System.Collections.Generic;

namespace Pathfinding.Lib.Maps.Utils
{
    internal class EmptyMapWithError : IMap
    {
        private string _errorReason; 

        internal EmptyMapWithError(string errorReason)
        {
            _errorReason = errorReason;
        }

        public IEnumerable<INode> GeneratePossibleStepsFrom(INode start)
        {
            return null;
        }

        public bool IsValidInMap(INode node)
        {
            return false;
        }

        public override string ToString()
        {
            return _errorReason;
        }
    }
}