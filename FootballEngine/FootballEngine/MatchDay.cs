// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatchDay.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   A match day containing a collection of matches for a league.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FootballEngine
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>A match day containing a collection of matches for a league.</summary>
    public class MatchDay
    {
        /// <summary>Initializes a new instance of the <see cref="MatchDay"/> class.</summary>
        /// <param name="number">The match day number with regard to the total number of match days for a league.</param>
        /// <param name="matches">The matches to be played.</param>
        public MatchDay(int number, IEnumerable<Match> matches)
        {
            this.Number = number;
            this.Matches = matches.OrderBy(m => m.ToString()).ToList();
        }

        /// <summary>Gets the matches.</summary>
        public List<Match> Matches { get; }

        /// <summary>Gets the number for this match day.</summary>
        public int Number { get; }

        /// <summary>Gets a string representation of the match day, with all fixtures listed.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var result = $"Matchday Number {this.Number}\n\n";
            
            foreach (var match in this.Matches)
            {
                result += match + "\n";
            }

            return result;
        }
    }
}
