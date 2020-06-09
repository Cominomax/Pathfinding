using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib
{
    public struct ScenarioParams
    {
        public string FilePath { get; set; }
        public MapTypes MapType { get; set; }
        public IPathfindingAlgorithm Algorithm { get; set; }
        public INode Start { get; set; }
        public INode End { get; set; }
    }
}
