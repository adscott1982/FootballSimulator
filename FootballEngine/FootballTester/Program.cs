using FootballEngine;
using System;

namespace FootballTester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Football Tester\n");

            var team1 = new Team("Forest", 4, 5, 4);
            var team2 = new Team("United", 7, 7, 7);

            var match = new Match(team1, team2);
            match.Play();

            Console.ReadKey();
        }
    }
}
