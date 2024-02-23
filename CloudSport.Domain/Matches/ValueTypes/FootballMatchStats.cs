using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Domain.Matches.ValueTypes
{
    public class FootballMatchStats
    {
        public int Goals { get; set; } = 0;
        public int Shots { get; set; } = 0;
    }
}
