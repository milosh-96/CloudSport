using CloudSport.Domain.Matches;
using CloudSport.Domain.Sports;
using CloudSport.Domain.Standings;
using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Application.Standings
{
    public class FootballStandingsGenerator
    {
        public Football Sport { get; } = new Football();

        public string DisplayStandings(List<FootballMatch> matches)
        {
            var standings = GenerateStandings(matches);
            StringBuilder output = new StringBuilder();
            foreach(var standing in standings)
            {
                output.Append($"{standing.Team.Name.PadRight(13)}{standing.MatchesPlayed}\t{standing.Points}\n");
            }
            return output.ToString();
        }
        public List<FootballStandingsItem> GenerateStandings(List<FootballMatch> matches)
        {
            List<FootballStandingsItem> standings = new List<FootballStandingsItem>();
            var teams = ExtractTeamsFromMatches(matches);
            foreach(var team in teams)
            {
                var standingsItem = new FootballStandingsItem()
                {
                    Team = team,
                    MatchesPlayed = CalculatePlayedMatches(team.Name, matches),
                    Points = CalculatePoints(team.Name, matches)
                };
                standings.Add(standingsItem);
            }
         
            return standings.OrderByDescending(x => x.Points).ThenBy(x => x.Team.Name).ToList();
        }

        public List<FootballTeam> ExtractTeamsFromMatches(List<FootballMatch> matches)
        {
            List<FootballTeam> teams = new List<FootballTeam>();
            foreach (var match in matches)
            {
                if (!teams.Any(x => x.Name == match.HomeTeam.Team.Name))
                {
                    teams.Add(match.HomeTeam.Team);
                }
                else if (!teams.Any(x => x.Name == match.AwayTeam.Team.Name))
                {
                    teams.Add(match.AwayTeam.Team);
                }
            }
            return teams;
        }

        public string GetAboutSport()
        {
            try
            {
                
                return $"{Sport.Name} has {Sport.NumberOfPlayers.Count} players in each team.";
            }
            catch
            {
                return "invalid object";
            }
        }

        public int CalculatePlayedMatches(string name, List<FootballMatch> footballMatches)
        {
            int count = 0;
            foreach (var match in footballMatches)
            {
                if (match.HomeTeam.Team.Name == name)
                {
                    count++;
                }
                else if (match.AwayTeam.Team.Name == name)
                {
                    count++;
                }
            }
            return count;
        }

        public int CalculatePoints(string name, List<FootballMatch> footballMatches)
        {
            int count = 0;
            foreach (var match in footballMatches)
            {
                var homeTeam = match.HomeTeam;
                var awayTeam = match.AwayTeam;
                if (homeTeam.Team.Name == name)
                {
                    count += GivePoints(homeTeam.Stats.Goals, awayTeam.Stats.Goals);
                }
                else if (match.AwayTeam.Team.Name == name)
                {
                    count += GivePoints(awayTeam.Stats.Goals, homeTeam.Stats.Goals);

                }
            }
            return count;
        }
        public int GivePoints(int teamScore, int opponentScore)
        {
            int points = 0;
            if(teamScore > opponentScore)
            {
                points = 3;
            }
            else if(teamScore == opponentScore)
            {
                points = 1;
            }
            return points;
        }
    }
}
