using System.Collections.Generic;
using AndyTools.Utilities;
using System;
using System.Linq;

namespace FootballEngine
{
    public static class FixtureGenerator
    {
        public static List<MatchDay> GenerateFixtures (List<Team> teams, bool homeAndAway)
        {
            // Shuffle the list of teams
            teams = teams.Shuffle(new Random(DateTime.Now.Millisecond * DateTime.Now.Month)).ToList();

            var matchDays = GetFixtures(teams, true);

            return matchDays;
        }

        public static List<MatchDay> GetFixtures(List<Team> teams, bool homeAndAway)
        {
            var teamCount = teams.Count;
            int totalMatchDays = teamCount - 1;
            int matchesPerRound = teamCount / 2;

            var matchDays = new List<MatchDay>();

            for (int matchDay = 0; matchDay < totalMatchDays; matchDay++)
            {
                var matches = new List<Match>();

                for (int match = 0; match < matchesPerRound; match++)
                {
                    int home = (matchDay + match) % (teamCount - 1);
                    int away = (teamCount - 1 - match + matchDay) % (teamCount - 1);
                    if (match == 0)
                    {
                        away = teamCount - 1;
                    }

                    var matchObject = new Match(teams[home], teams[away]);
                    matches.Add(matchObject);
                }

                matchDays.Add(new MatchDay(matches));
            }

            if (homeAndAway)
            {
                var reverseMatchDays = new List<MatchDay>();

                foreach (var matchDay in matchDays)
                {
                    var reverseMatches = new List<Match>();

                    foreach (var originalMatch in matchDay.Matches)
                    {
                        var homeTeam = originalMatch.AwayTeam;
                        var awayTeam = originalMatch.HomeTeam;

                        reverseMatches.Add(new Match(homeTeam, awayTeam));
                    }

                    reverseMatchDays.Add(new MatchDay(reverseMatches));
                }

                matchDays.AddRange(reverseMatchDays);
            }

            return matchDays;
        }
    }
}
