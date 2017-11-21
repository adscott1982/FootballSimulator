// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Team.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Class representing a team.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace FootballEngine
{
    /// <summary>Class representing a team.</summary>
    public class Team
    {
        /// <summary>Initializes a new instance of the <see cref="Team"/> class.</summary>
        /// <param name="name">The name of the team.</param>
        /// <param name="defence">The defense value.</param>
        /// <param name="midfield">The midfield value.</param>
        /// <param name="attack">The attack value.</param>
        public Team(string name, int defence, int midfield, int attack)
        {
            this.Name = name;
            this.Defence = defence;
            this.Midfield = midfield;
            this.Attack = attack;
        }

        /// <summary>Gets the name of the team.</summary>
        public string Name { get; }

        /// <summary>Gets the defense value for the team.</summary>
        public int Defence { get; }

        /// <summary>Gets the midfield value for the team.</summary>
        public int Midfield { get; }

        /// <summary>Gets the attack value for the team.</summary>
        public int Attack { get; }

        /// <summary>Gets the games played.</summary>
        public int GamesPlayed { get; private set; }

        /// <summary>Gets the games won.</summary>
        public int GamesWon { get; private set; }

        /// <summary>Gets the games drawn.</summary>
        public int GamesDrawn { get; private set; }

        /// <summary>Gets the games lost.</summary>
        public int GamesLost { get; private set; }

        /// <summary>Gets the goals scored.</summary>
        public int GoalsScored { get; private set; }

        /// <summary>Gets the goals conceded.</summary>
        public int GoalsConceded { get; private set; }

        /// <summary>Gets the goal difference.</summary>
        public int GoalDifference => this.GoalsScored - this.GoalsConceded;

        /// <summary>Gets the points.</summary>
        public int Points { get; private set; }

        /// <summary>Assign a result to the team, which will update the stats.</summary>
        /// <param name="goalsScored">The goals scored.</param>
        /// <param name="goalsConceded">The goals conceded.</param>
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

        /// <summary>Get the team name.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return this.Name;
        }
    }
}
