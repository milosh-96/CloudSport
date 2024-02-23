using CloudSport.Domain.Matches;
using CloudSport.Domain.Matches.ValueTypes;
using CloudSport.Domain.Teams;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudSport.Tests
{
    // todo: setup match single match and fixtures
    // 
    public class FootballMatchTests
    {
        [Fact]
        public void FootballMatch_DisplayMatch_HomeTeamScoresAwayTeamIsDisplayedWithCorrectScore()
        {
            // arrange
            FootballMatch match = new FootballMatch("Inter", 4, "AC Milan", 1);
            string expected = "Inter 4:1 AC Milan";
            // act
            var actual = match.DisplayOutput();
            // assert
            Assert.Equal(expected, actual);
        }
    }
}
