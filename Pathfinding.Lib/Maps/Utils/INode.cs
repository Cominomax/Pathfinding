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
        INode Parent { get; }
        /// <summary>
        /// Distance from the origin of path
        /// </summary>
        decimal DistanceFromOrigin { get; }
        /// <summary>
        /// Value of chosen heuristic
        /// </summary>
        decimal F { get; }

        /// <summary>
        /// Set the heuristic for that node.
        /// </summary>
        /// <param name="end">ending node</param>
        void SetF(INode end);

        /// <summary>
        /// Set the parent node and calculates the distance from origin.
        /// </summary>
        /// <param name="parent">startingNode</param>
        void SetParent(INode parent);
    }
}