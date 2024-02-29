using CloudSport.Domain.Matches;
using CloudSport.Domain.Sports;
using CloudSport.Domain.Standings;
using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            //headers//
            output.Append("Team".PadRight(13));
            output.Append("MP\t\t");
            output.Append("W\t\t");
            output.Append("D\t\t");
            output.Append("L\t\t");
            output.Append("GS\t");
            output.Append("GC\t");
            output.Append("GD\t");
            output.Append("Pts\n");
            output.AppendLine("".PadRight(13*9,'-'));
            foreach(var standing in standings)
            {
                output.Append(standing.Team.Name.PadRight(13));
                output.Append($"{standing.MatchesPlayed}\t\t");
                output.Append($"{standing.Wins}\t\t");
                output.Append($"{standing.Draws}\t\t");
                output.Append($"{standing.Losses}\t\t");
                output.Append($"{standing.GoalsScored}\t");
                output.Append($"{standing.GoalsConceded}\t");
                output.Append($"{standing.GoalsDifference}\t");
                output.Append($"{standing.Points}\n");
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
                    MatchesPlayed = CountPlayedMatches(team.Name, matches),
                    Wins = CountWins(team.Name, matches),
                    Draws = CountDraws(team.Name, matches),
                    Losses = CountLosses(team.Name, matches),
                    Points = CalculatePoints(team.Name, matches),
                    GoalsScored = CountGoalsScored(team.Name, matches),
                    GoalsConceded = CountGoalsConceded(team.Name, matches),
                    GoalsDifference = CalculateGoalDifference(team.Name, matches)
                };
                standings.Add(standingsItem);
            }
         
            return standings.OrderByDescending(x => x.Points)
                            .ThenByDescending(x=>x.GoalsDifference)
                            .ThenBy(x => x.Team.Name)
                            .ToList();
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

        public int CountPlayedMatches(string name, List<FootballMatch> footballMatches)
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

        public int CalculateGoalDifference(string teamName, List<FootballMatch> matches)
        {
            int scored = 0;
            int conceded = 0;
            int count = 0;
            foreach(var match in matches)
            {
                if(match.HomeTeam.Team.Name == teamName)
                {
                    scored += match.HomeTeam.Stats.Goals;
                    conceded += match.AwayTeam.Stats.Goals;
                }
                else if(match.AwayTeam.Team.Name == teamName)
                {
                    scored += match.AwayTeam.Stats.Goals;
                    conceded += match.HomeTeam.Stats.Goals;
                }
            }
            count = scored - conceded;
            return count;
        }
        
        public int CountGoalsScored(string teamName, List<FootballMatch> matches)
        {
            int count = 0;
            foreach(var match in matches)
            {
                if(match.HomeTeam.Team.Name == teamName)
                {
                    count += match.HomeTeam.Stats.Goals;
                }
                else if(match.AwayTeam.Team.Name == teamName)
                {
                    count += match.AwayTeam.Stats.Goals;
                }
            }
            return count;
        }
        public int CountGoalsConceded(string teamName, List<FootballMatch> matches)
        {
            int count = 0;
            foreach(var match in matches)
            {
                if(match.HomeTeam.Team.Name == teamName)
                {
                    count += match.AwayTeam.Stats.Goals;
                }
                else if(match.AwayTeam.Team.Name == teamName)
                {
                    count += match.HomeTeam.Stats.Goals;
                }
            }
            return count;
        } 
        
        public int CountWins(string teamName, List<FootballMatch> matches)
        {
            int count = 0;
            foreach(var match in matches)
            {
                var homeTeam = match.HomeTeam;
                var awayTeam = match.AwayTeam;
                if (homeTeam.Team.Name == teamName)
                {
                    if (homeTeam.Stats.Goals > awayTeam.Stats.Goals)
                    {
                        count += 1;
                    }
                }
                else if(awayTeam.Team.Name == teamName)
                {
                    if (awayTeam.Stats.Goals > homeTeam.Stats.Goals)
                    {
                        count += 1;
                    }
                }
            }
            return count;
        }

        public int CountLosses(string teamName, List<FootballMatch> matches)
        {
            int count = 0;
            foreach (var match in matches)
            {
                var homeTeam = match.HomeTeam;
                var awayTeam = match.AwayTeam;
                if (homeTeam.Team.Name == teamName)
                {
                    if (homeTeam.Stats.Goals < awayTeam.Stats.Goals)
                    {
                        count += 1;
                    }
                }
                else if (awayTeam.Team.Name == teamName)
                {
                    if (awayTeam.Stats.Goals < homeTeam.Stats.Goals)
                    {
                        count += 1;
                    }
                }
            }
            return count;
        } 
        
        public int CountDraws(string teamName, List<FootballMatch> matches)
        {
            int count = 0;
            foreach (var match in matches)
            {
                var homeTeam = match.HomeTeam;
                var awayTeam = match.AwayTeam;
                if (homeTeam.Team.Name == teamName || awayTeam.Team.Name == teamName)
                {
                    if (homeTeam.Stats.Goals == awayTeam.Stats.Goals)
                    {
                        count += 1;
                    }
                }
            }
            return count;
        }
    }
}
