using System.Collections.Generic;
using System.Linq;

namespace FootballEngine
{
    public class League
    {
        private List<Team> teams;

        public string Name { get; }

        public League(string name, List<Team> teams)
        {
            this.Name = name;
            this.teams = teams;
        }

        public IOrderedEnumerable<Team> Table
        {
            get { return this.teams
                    .OrderByDescending(t => t.Points)
                    .ThenByDescending(t => t.GoalDifference)
                    .ThenByDescending(t => t.GoalsScored)
                    .ThenBy(t => t.Name);
            }
        }

        public override string ToString()
        {
            var result = $"{this.Name} Table\n\n";

            var header = $"{"Team",-20}{"Pld",6}{"W",6}{"D",6}{"L",6}{"F",6}{"A",6}{"GD",6}{"Pts",6}\n";

            result += header;

            foreach(var team in this.Table)
            {
                result += $"{team.Name,-20}{team.GamesPlayed,6}{team.GamesWon,6}{team.GamesDrawn,6}{team.GamesLost,6}{team.GoalsScored,6}{team.GoalsConceded,6}{team.GoalDifference,6}{team.Points,6}\n";
            }

            return result;
        }
    }
}
