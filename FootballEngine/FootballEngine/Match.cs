using System;
using System.Threading.Tasks;

namespace FootballEngine
{
    public class Match
    {
        private Random random;

        public Match(Team homeTeam, Team awayTeam, Random random)
        {
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
            this.random = random;
        }

        public Team HomeTeam { get; }
        public Team AwayTeam { get; }

        public int HomeGoals { get; private set; }
        public int AwayGoals { get; private set; }

        public bool ShareTeams(Match otherMatch)
        {
            if (
                this.HomeTeam == otherMatch.HomeTeam
                || this.HomeTeam == otherMatch.AwayTeam
                || this.AwayTeam == otherMatch.HomeTeam
                || this.AwayTeam == otherMatch.AwayTeam)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return $"{HomeTeam} {this.HomeGoals} v {this.AwayGoals} {AwayTeam}";
        }

        public void Play()
        {
            var minutes = 0;
            this.HomeGoals = 0;
            this.AwayGoals = 0;
            var isHomePossession = false;

            while (minutes <= 90)
            {
                isHomePossession = this.DeterminePossession();
                var teamInPossession = isHomePossession ? this.HomeTeam : this.AwayTeam;

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

                if (isHomePossession) this.HomeGoals++;
                else this.AwayGoals++;
                minutes++;
            }

            this.HomeTeam.AssignResult(this.HomeGoals, this.AwayGoals);
            this.AwayTeam.AssignResult(this.AwayGoals, this.HomeGoals);
        }

        private bool DeterminePossession()
        {
            var totalFractions = this.HomeTeam.Midfield + this.AwayTeam.Midfield;
            var result = this.random.Next(1, totalFractions + 1);
            var isHomePossession = result <= this.HomeTeam.Midfield + 1 ? true : false;
            return isHomePossession;
        }

        private bool DetermineIsChance(bool isHomePossession)
        {
            var attack = isHomePossession ? this.HomeTeam.Attack + 1 : this.AwayTeam.Attack;
            var defence = isHomePossession ? this.AwayTeam.Defence : this.HomeTeam.Defence + 1;

            var totalFractions = attack + defence;

            var result = this.random.Next(1, totalFractions + 1);
            var attackBoost = result <= attack ? true : false;

            var chanceOfAttack = 0.27d;
            if (attackBoost) chanceOfAttack += 0.1d;
            else chanceOfAttack -= 0.1d;

            // Avg 25 shots per match over 90 minutes. 27 % chance of shot per minute
            var chance = this.random.NextDouble();
            return chance < chanceOfAttack;
        }

        private bool DetermineIsGoal(bool isHomePossession)
        {
            var attack = isHomePossession ? this.HomeTeam.Attack + 1 : this.AwayTeam.Attack;
            var defence = isHomePossession ? this.AwayTeam.Defence : this.HomeTeam.Defence + 1;

            var totalFractions = attack + defence;

            var result = this.random.Next(1, totalFractions + 1);
            var attackBoost = result <= attack ? true : false;

            var chanceOfGoal = 0.12d;
            if (attackBoost) chanceOfGoal += 0.04d;
            else chanceOfGoal -= 0.04d;

            // Avg 11 % chance shot will be a goal
            var chance = this.random.NextDouble();
            return chance < chanceOfGoal;
        }
    }
}
