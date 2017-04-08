using System.Collections.Generic;

namespace FootballEngine
{
    public class MatchDay
    {
        public MatchDay(List<Match> matches)
        {
            this.Matches = matches;
        }

        public List<Match> Matches { get; }
    }
}
