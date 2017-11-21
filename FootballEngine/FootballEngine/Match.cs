// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Match.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Used to perform matches between teams.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace FootballEngine
{
    using System;

    /// <summary>Used to perform matches between teams.</summary>
    public class Match
    {
        /// <summary>Random object for generating random numbers.</summary>
        private readonly Random random;

        /// <summary>Initializes a new instance of the <see cref="Match"/> class.</summary>
        /// <param name="homeTeam">The home team.</param>
        /// <param name="awayTeam">The away team.</param>
        /// <param name="random">The instance of the random object to use.</param>
        public Match(Team homeTeam, Team awayTeam, Random random)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.random = random;
        }

        /// <summary>Gets the home team.</summary>
        public Team HomeTeam { get; }

        /// <summary>Gets the away team.</summary>
        public Team AwayTeam { get; }

        /// <summary>Gets the home goals.</summary>
        public int HomeGoals { get; private set; }

        /// <summary>Gets the away goals.</summary>
        public int AwayGoals { get; private set; }

        /// <summary>Provides a string representation of the match, including goals scored.</summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return $"{this.HomeTeam} {this.HomeGoals} v {this.AwayGoals} {this.AwayTeam}";
        }

        /// <summary>Play the match.</summary>
        public void Play()
        {
            var minutes = 0;
            this.HomeGoals = 0;
            this.AwayGoals = 0;

            while (minutes <= 90)
            {
                var isHomePossession = this.DeterminePossession();

                if (!this.DetermineIsChance(isHomePossession))
                {
                    minutes++;
                    continue;
                }

                if (!this.DetermineIsGoal(isHomePossession))
                {
                    minutes++;
                    continue;
                }

                if (isHomePossession)
                {
                    this.HomeGoals++;
                }
                else
                {
                    this.AwayGoals++;
                }

                minutes++;
            }

            this.HomeTeam.AssignResult(this.HomeGoals, this.AwayGoals);
            this.AwayTeam.AssignResult(this.AwayGoals, this.HomeGoals);
        }

        /// <summary>Determine the team currently in possession.</summary>
        /// <returns>The <see cref="bool"/>, true if the home team.</returns>
        private bool DeterminePossession()
        {
            var totalFractions = this.HomeTeam.Midfield + this.AwayTeam.Midfield;
            var result = this.random.Next(1, totalFractions + 1);
            var isHomePossession = result <= this.HomeTeam.Midfield + 1;

            return isHomePossession;
        }

        /// <summary>Determine whether there is a chance.</summary>
        /// <param name="isHomePossession">Whether the possession is currently with the home team.</param>
        /// <returns>The <see cref="bool"/>, returns true if it is a chance.</returns>
        private bool DetermineIsChance(bool isHomePossession)
        {
            // Give the home team a bonus to their stats for attack if attacking, defence if defending
            var attack = isHomePossession ? this.HomeTeam.Attack + 1 : this.AwayTeam.Attack;
            var defence = isHomePossession ? this.AwayTeam.Defence : this.HomeTeam.Defence + 1;

            // The combined values for attack and defence in the current chance calculation
            var totalFractions = attack + defence;

            // Get a random value in the range of the combined values and determine if there is an attack boost
            var result = this.random.Next(1, totalFractions + 1);
            var attackBoost = result <= attack;

            // Avg 25 shots per match over 90 minutes. 27 % chance of shot per minute
            var chanceOfAttack = 0.27d;
            chanceOfAttack = attackBoost ? chanceOfAttack + 0.1d : chanceOfAttack - 0.1d;

            // If the random value lands within the chance of attack return true for a chance to occur
            var chance = this.random.NextDouble();
            return chance < chanceOfAttack;
        }

        /// <summary>Determine if a chance is converted to a goal.</summary>
        /// <param name="isHomePossession">Whether it is currently a home possession.</param>
        /// <returns>The <see cref="bool"/>, returns true if it is a goal.</returns>
        private bool DetermineIsGoal(bool isHomePossession)
        {
            // Give a boost to the home team if attacking or defending
            var attack = isHomePossession ? this.HomeTeam.Attack + 1 : this.AwayTeam.Attack;
            var defence = isHomePossession ? this.AwayTeam.Defence : this.HomeTeam.Defence + 1;

            // The combined values for attack and defence in the current goal calculation
            var totalFractions = attack + defence;

            // Get a random value in the range of the combined values and determine if there is an attack boost
            var result = this.random.Next(1, totalFractions + 1);
            var attackBoost = result <= attack;

            // Avg 11 % chance shot will be a goal
            var chanceOfGoal = 0.12d;
            chanceOfGoal = attackBoost ? chanceOfGoal + 0.04d : chanceOfGoal - 0.04d;

            // If the random value lands within the chance of a goal return true that a goal has been scored
            var chance = this.random.NextDouble();
            return chance < chanceOfGoal;
        }
    }
}
