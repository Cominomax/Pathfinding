using Pathfinding.Lib.Maps.Utils;
using System.Collections.Generic;

namespace Pathfinding.Lib.Scenarios
{
    public struct ScenarioResult
    {
        public IEnumerable<INode> Path { get; internal set; }
        public decimal PathLength { get; internal set; }
    }
}
