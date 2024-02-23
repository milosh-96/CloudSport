using CloudSport.Domain.Matches.ValueTypes;
using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Domain.Matches
{
    public class FootballMatch
    {
        public FootballMatch()
        {
        }
        public FootballMatch(string homeTeamName, int homeTeamScore, string awayTeamName, int awayTeamScore)
        {
            HomeTeam.Team.Name = homeTeamName;
            HomeTeam.Stats.Goals = homeTeamScore;
            AwayTeam.Team.Name = awayTeamName;
            AwayTeam.Stats.Goals = awayTeamScore;
        }

        public FootballMatchTeam HomeTeam { get; set; } = new FootballMatchTeam();
        public FootballMatchTeam AwayTeam { get; set; } = new FootballMatchTeam();

        public string DisplayOutput()
        {
            return $"{HomeTeam.Team.Name} {HomeTeam.Stats.Goals}:{AwayTeam.Stats.Goals} {AwayTeam.Team.Name}";
        }
    }
}
