﻿using System;
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

                if (!this.DetermineIsGoal(isHomePossession))
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
            var isHomePossession = result <= this.homeTeam.Midfield + 1 ? true : false;
            return isHomePossession;
        }

        private bool DetermineIsChance(bool isHomePossession)
        {
            var attack = isHomePossession ? this.homeTeam.Attack + 1 : this.awayTeam.Attack;
            var defence = isHomePossession ? this.awayTeam.Defence : this.homeTeam.Defence + 1;

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
            var attack = isHomePossession ? this.homeTeam.Attack + 1 : this.awayTeam.Attack;
            var defence = isHomePossession ? this.awayTeam.Defence : this.homeTeam.Defence + 1;

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
