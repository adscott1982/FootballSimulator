using System.Collections.Generic;
using AndyTools.Utilities;
using System;

namespace FootballEngine
{
    public static class FixtureGenerator
    {
        public static List<MatchDay> GenerateMatchDays (List<Team> teams, bool homeAndAway)
        {
            // Initialize list that will be returned
            var matchDays = new List<MatchDay>();

            // Shuffle the list of teams
            teams.Shuffle(new Random(DateTime.Now.Millisecond * DateTime.Now.Month));

            // Assign teams into left side and right side lists
            var leftSide = new List<Team>();
            var rightSide = new List<Team>();

            for (var i = 0; i < teams.Count; i++)
            {
                if (i.IsEven())
                {
                    rightSide.Add(teams[i]);
                }
                else
                {
                    leftSide.Add(teams[i]);
                }
            }

            // if there is an uneven number of teams add a null team to the right side, when fixtures are generated
            // the team playing against the null team will have a rest week

            if (rightSide.Count < leftSide.Count)
            {
                rightSide.Add(null);
            }

            return matchDays;
        }
    }
}
