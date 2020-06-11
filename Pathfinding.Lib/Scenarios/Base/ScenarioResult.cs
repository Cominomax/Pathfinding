using Pathfinding.Lib.Maps.Utils;
using System;
using System.Collections.Generic;

namespace Pathfinding.Lib.Scenarios.Base
{
    public class ScenarioResult : IComparable
    {
        public string Name { get; internal set; }
        public IEnumerable<INode> Path { get; internal set; }
        public decimal PathLength { get; internal set; }
        public bool Success { get; internal set; }
        public string Description { get; internal set; }

        public int CompareTo(object obj)
        {
            if (obj is ScenarioResult otherNode)
            {
                if (int.TryParse(Name, out int numericName) && int.TryParse(otherNode.Name, out int otherNumericName))
                {
                    return numericName.CompareTo(otherNumericName);
                }
                else
                {
                    return Name.CompareTo(otherNode.Name);
                }
            }
            return 1;
        }
    }
}
