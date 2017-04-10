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

            var header = "Team\t\t\t\tPld\tGS\tGC\tGD\tPts\n";
            header += $"----\t\t\t--\t--\t--\t---\n";

            result += header;

            foreach(var team in this.Table)
            {
                result += $"{team.Name}\t\t\t{team.GamesPlayed}\t{team.GoalsScored}\t{team.GoalsConceded}\t{team.GoalDifference}\t{team.Points}\n";
            }

            return result;
        }
    }
}
