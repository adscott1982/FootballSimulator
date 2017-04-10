namespace FootballEngine
{
    public class Team
    {
        public Team(string name, int defence, int midfield, int attack)
        {
            this.Name = name;
            this.Defence = defence;
            this.Midfield = midfield;
            this.Attack = attack;
        }

        public void AssignResult(int goalsScored, int goalsConceded)
        {
            this.GoalsScored += goalsScored;
            this.GoalsConceded += goalsConceded;

            int points;

            if (goalsScored > goalsConceded)
            {
                points = 3;
                this.GamesWon++;
            }
            else if (goalsScored == goalsConceded)
            {
                points = 1;
                this.GamesDrawn++;
            }
            else
            {
                points = 0;
                this.GamesLost++;
            }

            this.Points += points;

            this.GamesPlayed++;
        }

        public override string ToString()
        {
            return this.Name;
        }

        public string Name { get; }
        public int Defence { get; }
        public int Midfield { get; }
        public int Attack { get; }

        public int GamesPlayed { get; private set; }
        public int GamesWon { get; private set; }
        public int GamesDrawn { get; private set; }
        public int GamesLost { get; private set; }
        public int GoalsScored { get; private set; }
        public int GoalsConceded { get; private set; }
        public int GoalDifference
        {
            get
            {
                return this.GoalsScored - this.GoalsConceded;
            }
        }

        public int Points { get; private set; }
    }
}
