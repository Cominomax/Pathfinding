using System.Collections.Generic;
using Pathfinding.Lib.Maps.Utils;

namespace Pathfinding.Lib.Extensions
{
    public static class INodeExtensions
    {
        /// <summary>
        /// Transforms an INode into an IEnumerable starting from its further parent to itself. 
        /// </summary>
        /// <param name="node">node from which to start</param>
        /// <returns>IEnumerable starting from its further parent to the calling Node.</returns>
        public static IEnumerable<INode> ToIEnumerable(this INode node)
        {
            var s = new Stack<INode>();
            do
            {
                s.Push(node);
                node = node.Parent;
            } while (node != null);
            return s.ToArray();
        }
    }
}