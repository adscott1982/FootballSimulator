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

            var points = 0;

            if (goalsScored > goalsConceded) points = 3;
            else if (goalsScored == goalsConceded) points = 1;

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
