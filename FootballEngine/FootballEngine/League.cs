// --------------------------------------------------------------------------------------------------------------------
// <copyright file="League.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   League class consisting of a name and a list of teams.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FootballEngine
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>League class consisting of a name and a list of teams.</summary>
    public class League
    {
        /// <summary>The teams in this league.</summary>
        private List<Team> teams;

        /// <summary>Initializes a new instance of the <see cref="League"/> class.</summary>
        /// <param name="name">The name of the league.</param>
        /// <param name="teams">The teams in the league.</param>
        public League(string name, List<Team> teams)
        {
            this.Name = name;
            this.teams = teams;
        }

        /// <summary>Gets the name of the league.</summary>
        public string Name { get; }

        /// <summary>Gets the league table.</summary>
        public IOrderedEnumerable<Team> Table
        {
            get
            {
                return this.teams
                    .OrderByDescending(t => t.Points)
                    .ThenByDescending(t => t.GoalDifference)
                    .ThenByDescending(t => t.GoalsScored)
                    .ThenBy(t => t.Name);
            }
        }

        /// <summary>Returns a string representation of the league, the league table.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            var result = $"{this.Name} Table\n\n";

            var header = $"{"Team",-20}{"Pld",6}{"W",6}{"D",6}{"L",6}{"F",6}{"A",6}{"GD",6}{"Pts",6}\n";

            result += header;

            foreach (var team in this.Table)
            {
                result += $"{team.Name,-20}{team.GamesPlayed,6}{team.GamesWon,6}{team.GamesDrawn,6}{team.GamesLost,6}{team.GoalsScored,6}{team.GoalsConceded,6}{team.GoalDifference,6}{team.Points,6}\n";
            }

            return result;
        }
    }
}
