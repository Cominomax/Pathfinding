using System;

namespace Pathfinding.Lib.Maps.Utils
{
    /// <summary>
    /// Interface representation of a node in a Map. Must be Comparable and Equatable.
    /// </summary>
    public interface INode : IComparable, IEquatable<INode>
    {
        /// <summary>
        /// Previous step in path. 
        /// </summary>
        INode Parent { get; set; }
        /// <summary>
        /// Distance from the origin of path
        /// </summary>
        decimal DistanceFromOrigin { get; set; }
        /// <summary>
        /// Value of chosen heuristic
        /// </summary>
        decimal F { get; }

        /// <summary>
        /// Set the heuristic for that node.
        /// </summary>
        /// <param name="start">starting node</param>
        /// <param name="end">ending node</param>
        void SetF(INode start, INode end);
    }
}