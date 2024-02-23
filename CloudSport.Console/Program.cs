using System;
using CloudSport.Application.Standings;
using CloudSport.Domain.Matches;

namespace CloudSport.Cli
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var matches = new List<FootballMatch>()
            {
                new FootballMatch("Turkey",0,"Italy",3),
                new FootballMatch("Wales",1,"Switzerland",1),

                new FootballMatch("Turkey",0,"Wales",2),
                new FootballMatch("Italy",3,"Switzerland",2),

                new FootballMatch("Switzerland",3,"Turkey",1),
                new FootballMatch("Italy",1,"Wales",0),

            };
           
            var standingsGenerator = new FootballStandingsGenerator();
            Console.WriteLine(standingsGenerator.DisplayStandings(matches));
        }
    }
}
