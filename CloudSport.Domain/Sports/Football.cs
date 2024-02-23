using CloudSport.Domain.Teams.ValueTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Domain.Sports
{
    public class Football
    {
        public string Name { get; set; } = "Football";
        public NumberOfPlayers NumberOfPlayers { get; set; } = new NumberOfPlayers() { Count = 11 };
    }
}
