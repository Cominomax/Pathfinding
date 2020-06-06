using System;
using System.Linq;
using System.Collections.Generic;
namespace Pathfinding.Lib.Extensions
{
    public static  class ICollectionExtensions
    {
        /// <summary>
        /// Gets and removes the minimum value from the list.
        /// </summary>
        /// <param name="collection">The collection of data on which to do the operation.</param>
        /// <typeparam name="T">Must implement IComparable.</typeparam>
        /// <returns>Minimum value of the list.</returns>
        public static T PopMin<T>(this ICollection<T> collection) where T : IComparable
        {
            T q = collection.Min();
            collection.Remove(q);
            return q;
        }

        /// <summary>
        /// Gets and removes the maximum value from the list.
        /// </summary>
        /// <param name="collection">The collection of data on which to do the operation.</param>
        /// <typeparam name="T">Must implement IComparable.</typeparam>
        /// <returns>Maximum value of the list.</returns>
        public static T PopMax<T>(this ICollection<T> collection) where T : IComparable
        {
            var q = collection.Max();
            collection.Remove(q);
            return q;
        }
    }
}