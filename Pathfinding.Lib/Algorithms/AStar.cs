using System.Collections.Generic;
using System.Linq;
using Pathfinding;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Algorithms
{
    /// <summary>
    /// Generic implementation of AStar Research Algorithm.
    /// </summary>
    public class AStar : IPathfindingAlgorithm
    {
        /// <summary>
        /// Will solve the scenario by finding the road between the start INode and the end INode on the current map.
        /// </summary>
        /// <param name="scen">Fully set scenario.</param>
        /// <returns>Node at the end of the found Path. Reconstruct Parents to obtain the collection.</returns>
        public INode Resolve(Scenario scen)
        {
            var open = new List<INode>() { scen.Start };
            var closed = new List<INode>();

            while(open.Any())
            {
                var q = open.PopMin();
                var possibleSuccessor = scen.Map.GeneratePossibleStepsFrom(q);
                foreach (INode item in possibleSuccessor)
                {
                    if (scen.IsCompleted(item))
                    {
                        return item;
                    }
                    item.SetF(scen.End);

                    if (open.Any(n => n.Equals(item) && n.F <= item.F))
                    {
                        continue;
                    }

                    if (closed.Any(n => n.Equals(item) && n.F <= item.F))
                    {
                        continue;
                    }
                    open.Add(item);
                }
                closed.Add(q);
            }
            return null;
        }
        
    }
}