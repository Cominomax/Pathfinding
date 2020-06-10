using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Scenarios.Base
{
    public interface IGotAStartAndAnEndOnAMap
    {
        public INode End { get; }
        public INode Start { get; }
        public IMap Map { get; }
        bool IsCompleted(INode item);
    }
}
