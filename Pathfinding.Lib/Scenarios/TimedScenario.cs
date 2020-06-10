using System.Diagnostics;

namespace Pathfinding.Lib.Scenarios
{
    public class TimedScenario : Scenario
    {
        public TimedScenario()
        {
        }

        public long ElapsedMilliseconds { get; set; }

        public override MethodResult RunScenario()
        {
            var timer = new Stopwatch();
            timer.Restart();
            var methodResult = base.RunScenario();
            timer.Stop();
            ElapsedMilliseconds = timer.ElapsedMilliseconds;
            return methodResult;
        }
    }
}
