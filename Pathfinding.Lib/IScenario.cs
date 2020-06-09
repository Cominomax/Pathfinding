
namespace Pathfinding.Lib
{
    public interface IScenario 
    {
        public ScenarioResult Result { get; }

        MethodResult TrySetScenario(ScenarioParams @params);
        MethodResult RunScenario();
    }
}
