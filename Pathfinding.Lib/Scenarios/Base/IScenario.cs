
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Scenarios.Base
{
    public interface IScenario : IGotAStartAndAnEndOnAMap, ICanSetAScenario
    {
        public string Name { get; }
        public decimal ExpectedLength { get; }
    }
}
