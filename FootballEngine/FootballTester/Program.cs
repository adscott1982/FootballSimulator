using FootballEngine;
using System;

namespace FootballTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Football Tester\n");

            var teams = XmlDataHandler.LoadTeams("BaseTeams.xml");
            var matchDays = FixtureGenerator.GenerateFixtures(teams, true);

            var match = new Match(teams[0], teams[1]);
            match.Play();

            Console.ReadKey();
        }
    }
}
