// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FixtureGenerator.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Static class containing methods to generate fixtures.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FootballEngine
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AndyTools.Utilities;

    /// <summary>Static class containing methods to generate fixtures.</summary>
    public static class FixtureGenerator
    {
        /// <summary>Generate the fixtures for a list of teams.</summary>
        /// <param name="teams">The teams.</param>
        /// <param name="homeAndAway">Whether fixtures should be home and away, or just single.</param>
        /// <param name="random">The random object to use.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        public static List<MatchDay> GenerateFixtures(List<Team> teams, bool homeAndAway, Random random)
        {
            // Shuffle the list of teams
            teams = teams.Shuffle(random).ToList();
            var matchDays = GetFixtures(teams, true, random);

            return matchDays;
        }

        /// <summary>Get all the fixtures.</summary>
        /// <param name="teams">The teams.</param>
        /// <param name="homeAndAway">The home and away.</param>
        /// <param name="random">The random.</param>
        /// <returns>The <see cref="List{T}"/>.</returns>
        private static List<MatchDay> GetFixtures(List<Team> teams, bool homeAndAway, Random random)
        {
            var teamCount = teams.Count;
            var totalMatchDays = teamCount - 1;
            var matchesPerRound = teamCount / 2;

            var matchDays = new List<MatchDay>();

            for (var matchDay = 0; matchDay < totalMatchDays; matchDay++)
            {
                var matches = new List<Match>();

                for (var match = 0; match < matchesPerRound; match++)
                {
                    var home = (matchDay + match) % (teamCount - 1);
                    var away = (teamCount - 1 - match + matchDay) % (teamCount - 1);

                    if (match == 0)
                    {
                        away = teamCount - 1;
                    }

                    var matchObject = new Match(teams[home], teams[away], random);
                    matches.Add(matchObject);
                }

                matchDays.Add(new MatchDay(matchDay + 1, matches));
            }

            if (!homeAndAway)
            {
                return matchDays;
            }

            var reverseMatchDays = new List<MatchDay>();

            foreach (var matchDay in matchDays)
            {
                var reverseMatches = new List<Match>();

                foreach (var originalMatch in matchDay.Matches)
                {
                    var homeTeam = originalMatch.AwayTeam;
                    var awayTeam = originalMatch.HomeTeam;

                    reverseMatches.Add(new Match(homeTeam, awayTeam, random));
                }

                reverseMatchDays.Add(new MatchDay(matchDay.Number + matchDays.Count, reverseMatches));
            }

            matchDays.AddRange(reverseMatchDays);

            return matchDays;
        }
    }
}
