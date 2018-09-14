// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Defines the Extensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AndyTools.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>Static class containing useful extension methods.</summary>
    public static class Extensions
    {
        /// <summary>Clamp a type which implements IComparable between a min and a max value</summary>
        /// <param name="val">The value.</param>
        /// <param name="min">The min value.</param>
        /// <param name="max">The max value.</param>
        /// <typeparam name="T">The type of object.</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        public static T Clamp<T>(this T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0)
            {
                return min;
            }

            if (val.CompareTo(max) > 0)
            {
                return max;
            }

            return val;
        }

        /// <summary>Whether a given integer value is even.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        /// <summary>Whether a 2D arrays x and y values are inside the bounds of the array.</summary>
        /// <param name="array">The array.</param>
        /// <param name="x">The x index.</param>
        /// <param name="y">The y index.</param>
        /// <typeparam name="T">The type of the array.</typeparam>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool In2DArrayBounds<T>(this T[,] array, int x, int y)
        {
            var result = !(
                x < array.GetLowerBound(0) ||
                x > array.GetUpperBound(0) ||
                y < array.GetLowerBound(1) ||
                y > array.GetUpperBound(1));

            return result;
        }

        /// <summary>Shift the contents of a list by a certain amount and wrap the values.</summary>
        /// <param name="list">The list.</param>
        /// <param name="shiftCount">The shift count, must be positive.</param>
        /// <typeparam name="T">The type of the list.</typeparam>
        /// <returns>The <see cref="List{T}"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Throws if the shift count is less than zero, or exceeds the size of the list.</exception>
        public static List<T> ShiftAndWrap<T>(this List<T> list, int shiftCount)
        {
            if (shiftCount < 0 || shiftCount > list.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(shiftCount), "The shift amount must be non-negative and not exceed the size of the list.");
            }

            if (shiftCount == 0 || shiftCount == list.Count)
            {
                return list.ToList();
            }

            var newList = list.ToList();
            var tempList = newList.GetRange(newList.Count - shiftCount, shiftCount);
            newList.RemoveRange(newList.Count - shiftCount, shiftCount);
            newList.InsertRange(0, tempList);

            return newList;
        }

        /// <summary>Get a shuffled enumerable.</summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="random">The optional random object.</param>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">Throws if the source enumerable is null.</exception>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random = null)
        {
            if (random == null)
            {
                random = new Random();
            }

            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            return source.ShuffleIterator(random);
        }

        /// <summary>The shuffle iterator.</summary>
        /// <param name="source">The source enumerable.</param>
        /// <param name="rng">The random object.</param>
        /// <typeparam name="T">The type of the enumerable.</typeparam>
        /// <returns>The <see cref="IEnumerable{T}"/>.</returns>
        private static IEnumerable<T> ShuffleIterator<T>(this IEnumerable<T> source, Random rng)
        {
            var buffer = source.ToList();
            for (var i = 0; i < buffer.Count; i++)
            {
                var j = rng.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }
    }
}
