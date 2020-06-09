using System.Diagnostics;

namespace Pathfinding.Lib
{
    public class TimedScenario<T> : IScenario where T : IScenario 
    {
        public TimedScenario(T scenario)
        {
            ScenarioToBeTimed = scenario;
        }

        public T ScenarioToBeTimed { get; set; }
        public long ElapsedMilliseconds { get; set; }

        public ScenarioResult Result => ScenarioToBeTimed.Result;

        public MethodResult RunScenario()
        {
            var timer = new Stopwatch();
            timer.Restart();
            var methodResult = ScenarioToBeTimed.RunScenario();
            timer.Stop();
            ElapsedMilliseconds = timer.ElapsedMilliseconds;
            return methodResult;
        }

        public MethodResult TrySetScenario(ScenarioParams @params)
        {
            return ScenarioToBeTimed.TrySetScenario(@params);
        }
    }
}
