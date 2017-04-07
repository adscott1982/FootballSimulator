using System;
using System.Threading.Tasks;

namespace FootballEngine
{
    public class Match
    {
        private Team homeTeam;
        private Team awayTeam;
        private Random random;

        public Match(Team homeTeam, Team awayTeam)
        {
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.random = new Random(DateTime.Now.Millisecond * DateTime.Now.Hour / DateTime.Now.Minute);
        }

        public void Play()
        {
            var minutes = 0;
            var homeGoals = 0;
            var awayGoals = 0;
            var isHomePossession = false;

            Console.WriteLine($"{homeTeam.Name} {homeGoals} vs {awayGoals} {awayTeam.Name}\n");

            while (minutes <= 90)
            {
                Task.Delay(50).Wait();
                Console.Write($"'{minutes} - ");

                isHomePossession = this.DeterminePossession();
                var teamInPossession = isHomePossession ? this.homeTeam : this.awayTeam;

                Console.Write($"{teamInPossession.Name} have possession... ");

                if (!this.DetermineIsChance(isHomePossession))
                {
                    Console.Write("but they lose the ball.\n");
                    minutes++;
                    continue;
                }

                Console.Write("they have a chance... ");

                if (!this.DetermineIsGoal())
                {
                    Console.Write("missed!\n");
                    minutes++;
                    continue;
                }

                Console.Write("GOAL!\n");

                if (isHomePossession) homeGoals++;
                else awayGoals++;

                Console.WriteLine($"{homeTeam.Name} {homeGoals} vs {awayGoals} {awayTeam.Name}");

                minutes++;
            }

            Console.WriteLine($"{homeTeam.Name} {homeGoals} vs {awayGoals} {awayTeam.Name}\n");
        }

        private bool DeterminePossession()
        {
            var totalFractions = this.homeTeam.Midfield + this.awayTeam.Midfield;
            var result = this.random.Next(1, totalFractions + 1);
            var isHomePossession = result <= this.homeTeam.Midfield ? true : false;
            return isHomePossession;
        }

        private bool DetermineIsChance(bool isHomePossession)
        {
            var attack = isHomePossession ? this.homeTeam.Attack : this.awayTeam.Attack;
            var defence = isHomePossession ? this.awayTeam.Defence : this.homeTeam.Defence;

            var totalFractions = attack + defence;

            var result = this.random.Next(1, totalFractions + 1);
            var isChance = result <= attack ? true : false;

            return isChance;

            //// Avg 25 shots per match over 90 minutes. 27 % chance of shot per minute
            //var chance = this.random.NextDouble();
            //return chance < 0.27d;
        }

        private bool DetermineIsGoal()
        {
            // Avg 11 % chance shot will be a goal
            var chance = this.random.NextDouble();
            return chance < 0.11d;
        }
    }
}
