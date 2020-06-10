
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Scenarios
{
    public interface IScenario 
    {
        public INode End { get; }
        public INode Start { get; }
        public IMap Map { get; }
        public ScenarioResult Result { get; }

        MethodResult TrySetScenario(ScenarioParams @params);
        MethodResult RunScenario();
        bool IsCompleted(INode item);
    }
}
