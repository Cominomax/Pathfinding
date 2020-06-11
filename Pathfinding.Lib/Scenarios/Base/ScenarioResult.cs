using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pathfinding.Lib.Scenarios.Base
{
    public class ScenarioResult : IComparable
    {
        public string Name { get; internal set; }
        public IEnumerable<INode> Path { get; internal set; }
        public decimal PathLength { get; internal set; }
        public decimal CorrectPathLength { get; internal set; }
        public bool Success { get; internal set; }
        public string ErrorMessage{ get; internal set; }

        public override string ToString()
        {
            if (Success)
            {
                return string.Join(Environment.NewLine,
                    $"Scenario {Name}: going from {Path.First()} to {Path.Last()}.",
                    $"The result length of path is:   {PathLength}.",
                    $"The expected length of path is: {CorrectPathLength}",
                    $"The % increase of the result length: {PathLength.PercentageDifference(CorrectPathLength)}%",
                    Path.ToCollectionString());
            }
            return ErrorMessage;
        }

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
