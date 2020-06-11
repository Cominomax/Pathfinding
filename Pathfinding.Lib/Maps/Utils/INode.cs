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
        /// X position in the GRID
        /// </summary>
        int X { get; }
        /// <summary>
        /// Y position in the GRID
        /// </summary>
        int Y { get; }
        /// <summary>
        /// Distance from the origin of path
        /// </summary>
        decimal DistanceFromOrigin { get; }
        /// <summary>
        /// Value of chosen heuristic
        /// </summary>
        decimal F { get; set; }

        /// <summary>
        /// Set the parent node and calculates the distance from origin.
        /// </summary>
        /// <param name="parent">startingNode</param>
        void SetParent(INode parent);
    }
}