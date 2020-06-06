using System.Collections.Generic;

namespace Pathfinding.Lib.Maps.Utils
{
    /// <summary>
    /// Represents a Map on which a pathfinding algorithm can run
    /// </summary>
    public interface IMap
    {
        /// <summary>
        /// Returns the left of possible steps from the start node
        /// </summary>
        /// <param name="start">start node</param>
        /// <returns>possible steps from start node</returns>
        IEnumerable<INode> GeneratePossibleStepsFrom(INode start);

        /// <summary>
        /// Validates a node in the map (Is in the map and can walk on it)
        /// </summary>
        /// <param name="node">node to validate</param>
        /// <returns>True if valid, false if not.</returns>
        bool IsValidInMap(INode node);
    }
}