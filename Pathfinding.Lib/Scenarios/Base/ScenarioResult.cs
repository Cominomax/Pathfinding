using Pathfinding.Lib.Maps.Utils;
using System.Collections.Generic;

namespace Pathfinding.Lib.Scenarios.Base
{
    public struct ScenarioResult
    {
        public IEnumerable<INode> Path { get; internal set; }
        public decimal PathLength { get; internal set; }
        public long ElapsedMilliseconds { get; internal set; }
        public bool Success { get; internal set; }
    }
}
