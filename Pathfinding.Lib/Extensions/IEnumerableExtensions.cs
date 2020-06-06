using System.Collections.Generic;
using System.Text;

namespace Pathfinding.Lib.Extensions
{
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Transforms a IEnumerable into a '->' joined string of its content
        /// </summary>
        /// <param name="collection">The collection of data on which to do the operation.</param>
        /// <typeparam name="T">Must implement ToString() for better result.</typeparam>
        /// <returns>'->' connected string of IEnumerable content</returns>
        public static string ToCollectionString<T>(this IEnumerable<T> collection)
        {
            var sb = new StringBuilder();
            foreach (var item in collection)
            {
                sb.Append(item).Append(" -> ");
            }
            return sb.Remove(sb.Length - 4, 4).ToString();
        }
    }
}