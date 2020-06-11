using System;
using Pathfinding.Lib.Extensions;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Maps.Grid
{
    /// <summary>
    /// Represents a position in a Path being calculated inside a Grid.
    /// </summary>
    public class GridNode : INode
    {
        public GridNode(int x, int y, DirectionEnum dir = DirectionEnum.Origin)
        {
            X = x;
            Y = y;
            Direction = dir;
        }

        public int X { get; }
        public int Y { get; }
        public DirectionEnum Direction { get; }
        public decimal DistanceFromOrigin { get; private set; }
        /// <summary>
        /// F is the distance between this node and the origin and the euclidian distance between this and the end. 
        /// </summary>
        public decimal F{ get; private set; }

        /// <summary>
        /// Parent of the Node.
        /// </summary>
        public INode Parent { get; private set;  }

        /// <summary>
        /// Calculates the F Value of the node. F is the distance between this node and the origin and the euclidian distance between this and the end. 
        /// </summary>
        /// <param name="start">INode implementor. Another instance of GridNode.</param>
        /// <param name="end">INode implementor. Another instance of GridNode.</param>
        public void SetF(INode end)
        {
            var distanceFromGoal = CalculateDiagonalDistance(end as GridNode);
            F = DistanceFromOrigin + distanceFromGoal;
        }

        /// <summary>
        /// Set the parent of the Node and the distanceFromOrigin
        /// </summary>
        /// <param name="parent"></param>
        public void SetParent(INode parent)
        {
            Parent = parent;
            SetDistanceFromOrigin();
        }

        /// <summary>
        /// Compares this object's F value and the passed object.
        /// </summary>
        /// <param name="obj">INode implementor. Another instance of GridNode.</param>
        /// <returns>result of decimal comparison.</returns>
        public int CompareTo(object obj) 
        {
            if (obj is GridNode otherNode)
            {
                return F.CompareTo(otherNode.F);
            }
            return 1;
        }

        /// <summary>
        /// Verifies Equality between this and the passed object.
        /// </summary>
        /// <param name="obj">INode implementor. Another instance of GridNode.</param>
        /// <returns>Equality</returns>
        public bool Equals(INode obj)
        {
            GridNode otherNode = obj as GridNode;
            if (otherNode != null)
            {    
                return X == otherNode.X && Y == otherNode.Y;
            }  
            return false;
        }

        /// <summary>
        /// NOT Using it... Just gotta because overriding Equals.
        /// </summary>
        /// <returns>X</returns>
        public override int GetHashCode()
        {
            return X;
        }

        /// <summary>
        /// Returns the string representation of this object.
        /// </summary>
        /// <returns>"{Direction}: ({X},{Y})"</returns>
        public override string ToString()
        {
            return $"({X},{Y})";
        }

        /// <summary>
        /// Calculates euclidian distance between this node and the end node
        /// </summary>
        /// <param name="end">other node whose distance we want to know</param>
        /// <returns>euclidian distance decimal</returns>
        private decimal CalculateDiagonalDistance(GridNode end)
        {
            return Math.Max( Math.Abs(X - end.X) , Math.Abs(Y - end.Y) );
        }


        /// <summary>
        /// Calculates the distance from the parent
        /// </summary>
        private void SetDistanceFromOrigin()
        {
            DistanceFromOrigin = Parent.DistanceFromOrigin +
                (Direction.HasMoreThan1Direction()
                ? DistanceConstants.BidirectionalMove
                : DistanceConstants.MonodirectionalMove);
        }

        /// <summary>
        /// Returns possible positions for the next node
        /// </summary>
        /// <returns>possible positions for the next node in order: Down, Right, Up, Left, DownRight, UpRight, UpLeft, DownLeft</returns>
        internal GridNode[] GenerateSteps()
        {
            return new GridNode[] 
            {
                new GridNode(X+1, Y,    DirectionEnum.Right),     
                new GridNode(X,   Y+1,  DirectionEnum.Down),    
                new GridNode(X-1, Y,    DirectionEnum.Left),      
                new GridNode(X,   Y-1,  DirectionEnum.Up),    

                new GridNode(X+1, Y+1,  DirectionEnum.Down  | DirectionEnum.Right),
                new GridNode(X-1, Y+1,  DirectionEnum.Down    | DirectionEnum.Left),  
                new GridNode(X-1, Y-1,  DirectionEnum.Up    | DirectionEnum.Left),   
                new GridNode(X+1, Y-1,  DirectionEnum.Up  | DirectionEnum.Right)    
            };
        }
    }
}