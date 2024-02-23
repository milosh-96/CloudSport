using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Domain.Matches.ValueTypes
{
    public class FootballMatchTeam
    {
        public FootballTeam Team { get; set; } = new FootballTeam();
        public FootballMatchStats Stats { get; set; } = new FootballMatchStats();
    }
}
