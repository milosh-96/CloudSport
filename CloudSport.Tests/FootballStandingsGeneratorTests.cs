using CloudSport.Application.Standings;
using CloudSport.Domain.Matches;
using CloudSport.Domain.Sports;
using CloudSport.Domain.Standings;
using CloudSport.Domain.Teams;
using System.Text;

namespace CloudSport.Tests
{
    public class FootballStandingsGeneratorTests
    {
        private readonly FootballStandingsGenerator _service;
        private readonly List<FootballMatch> _footballMatches;
        public FootballStandingsGeneratorTests()
        {
            _service = new FootballStandingsGenerator();
            _footballMatches = new List<FootballMatch>()
            {
                new FootballMatch("Inter",4,"AC Milan",1),
                new FootballMatch("Torino",2,"Genoa",3),

                new FootballMatch("Genoa",1,"Inter",1),
                new FootballMatch("Torino",2,"AC Milan",2),

                new FootballMatch("Inter",3,"Torino",1),
                new FootballMatch("AC Milan",4,"Genoa",1)
            };
        }


        [Fact]
        public void StandingsGenerator_DisplayStandings_CorrectFormattedTableIsReturned()
        {
            // arrange
            StringBuilder sb = new StringBuilder();
            sb.Append("Inter".PadRight(13)+"3\t7\n");
            sb.Append("AC Milan".PadRight(13)+"3\t4\n");
            sb.Append("Genoa".PadRight(13) + "3\t4\n");
            sb.Append("Torino".PadRight(13) + "3\t1\n");
            string expected = sb.ToString();

            // act
            string actual = _service.DisplayStandings(_footballMatches);

            // assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void StandingsGenerator_GetAboutSport_DisplaysCorrectMessage()
        {
            // arrange
            string expected = "Football has 11 players in each team.";
            // act
            string actual = _service.GetAboutSport();
            // assert
            Assert.Equal(expected,actual);
        }

        [Fact]
        public void StandingsGenerator_ExtractTeamsFromMatches_ExtractsCorrectNumberOfTeams()
        {
            // arrange
            int expected = 4;
            // act
            List<FootballTeam> actual = _service.ExtractTeamsFromMatches(_footballMatches);
            // assert
            Assert.Equal(expected, actual.Count);
        }

        [Theory]
        [InlineData("Inter",3)]
        [InlineData("AC Milan",3)]
        [InlineData("Torino",3)]
        [InlineData("Genoa",3)]
        public void StandingsGenerator_CalculatePlayedMatches_CountIsCorrect(string name, int expected)
        {
            // arrange
            // act
            int actual = _service.CalculatePlayedMatches(name, _footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Inter", 7)]
        [InlineData("AC Milan", 4)]
        [InlineData("Torino", 1)]
        [InlineData("Genoa", 4)]
        public void StandingsGenerator_CalculatePoints_CountIsCorrect(string name, int expected)
        {
            // arrange
            // act
            int actual = _service.CalculatePoints(name, _footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(3,2,3)]
        [InlineData(2,3,0)]
        [InlineData(2,2,1)]
        [InlineData(0,0,1)]
        public void StandingsGenerator_GivePoints_CorrectNumberOfPointsIsGivenBasedOnResultOfMatch(int teamScore, int opponentScore, int expected)
        {
            // arrange
            // act
            int actual = _service.GivePoints(teamScore, opponentScore);
            // assert
            Assert.Equal(expected, actual);
        }
    }
}