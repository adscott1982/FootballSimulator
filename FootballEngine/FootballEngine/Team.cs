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

        public override string ToString()
        {
            return this.Name;
        }

        public string Name { get; }
        public int Defence { get; }
        public int Midfield { get; }
        public int Attack { get; }
        public int GoalsScored { get; }
        public int GoalsConceded { get; }
        public int Points { get; private set; }
    }
}
