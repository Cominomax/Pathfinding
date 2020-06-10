using Pathfinding.Lib.Algorithms;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Scenarios.Base
{
    public struct ScenarioParams
    {
        public string ScenarioName { get; set; }
        public string FilePath { get; set; }
        public MapTypes MapType { get; set; }
        public INode Start { get; set; }
        public INode End { get; set; }
        
    }
}
