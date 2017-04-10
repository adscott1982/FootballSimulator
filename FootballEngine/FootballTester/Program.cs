using FootballEngine;
using System;

namespace FootballTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Football Tester\n");

            var random = new Random(DateTime.Now.Millisecond * DateTime.Now.Day);
            var teams = XmlDataHandler.LoadTeams("BaseTeams.xml");
            var league = new League("Premiership", teams);
            var matchDays = FixtureGenerator.GenerateFixtures(teams, true, random);

            foreach(var matchDay in matchDays)
            {
                //// League table before matchday
                //Console.WriteLine(league);
                //Console.ReadKey();
                //Console.Clear();

                // Matchday fixtures
                //Console.WriteLine(matchDay);
                PlayMatchDay(matchDay);
                //Console.ReadKey();
                //Console.Clear();

                //// Matchday results
                //Console.WriteLine(matchDay);
                //Console.ReadKey();
                //Console.Clear();
            }

            // final league table
            Console.WriteLine(league);
            Console.ReadKey();
            Console.Clear();
        }

        static void PlayMatchDay(MatchDay matchDay)
        {
            foreach(var match in matchDay.Matches)
            {
                match.Play();
            }
        }
    }
}
