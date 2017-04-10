using System.Collections.Generic;
using System.Linq;

namespace FootballEngine
{
    public class MatchDay
    {
        public MatchDay(int number, List<Match> matches)
        {
            this.Number = number;
            this.Matches = matches.OrderBy(m => m.ToString()).ToList();
        }

        public List<Match> Matches { get; }

        public int Number { get; }

        public override string ToString()
        {
            var result = $"Matchday Number {this.Number}\n\n";
            
            foreach(var match in this.Matches)
            {
                result += match.ToString() + "\n";
            }

            return result;
        }
    }
}
