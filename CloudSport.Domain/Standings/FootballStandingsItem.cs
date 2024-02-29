using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Domain.Standings
{
    public class FootballStandingsItem
    {
        public FootballTeam Team { get; set; }

        public int Points { get; set; } = 0;
        public int MatchesPlayed { get; set; } = 0;
        public int Wins { get; set; } = 0;
        public int Losses { get; set; } = 0;
        public int Draws { get; set; } = 0;
        public int GoalsScored { get; set; } = 0;
        public int GoalsConceded { get; set; } = 0;
        public int GoalsDifference { get; set; } = 0;
    }
}
