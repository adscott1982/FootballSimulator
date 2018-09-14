// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TimeSpanExtensions.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Defines the TimeSpanExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AndyTools.Utilities
{
    using System;

    /// <summary>Extension methods for TimeSpan.</summary>
    public static class TimeSpanExtensions
    {
        /// <summary>Return a string representing a timespan in Hour/Min/Sec.</summary>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>The <see cref="string"/>.</returns>
        public static string ToHourMinSec(this TimeSpan timeSpan)
        {
            var timeSpanString = string.Empty;
            var hours = (timeSpan.Days * 24) + timeSpan.Hours;
            timeSpanString += hours > 0 ? $"{hours}h " : string.Empty;
            timeSpanString += $"{timeSpan.Minutes}m ";
            timeSpanString += $"{timeSpan.Seconds}s";

            return timeSpanString;
        }
    }
}
