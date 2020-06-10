using System;
using System.Collections.Generic;
using System.Text;

namespace Pathfinding.Lib.Scenarios.Base
{
    public interface ICanSetAScenario
    {
        bool IsSet { get; }
        MethodResult TrySetScenario(ScenarioParams @params);
    }
}
