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
        public void StandingsGenerator_CountPlayedMatches_CountIsCorrect(string name, int expected)
        {
            // arrange
            // act
            int actual = _service.CountPlayedMatches(name, _footballMatches);
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
        
        [Theory]
        [InlineData("Inter", 5)]
        [InlineData("AC Milan", 0)]
        [InlineData("Torino", -3)]
        [InlineData("Genoa", -2)]

        public void StandingsGenerator_CalculateGoalDifference_CorrectGoalDifferenceIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CalculateGoalDifference(teamName,_footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("Inter", 8)]
        [InlineData("AC Milan", 7)]
        [InlineData("Torino", 5)]
        [InlineData("Genoa", 5)]

        public void StandingsGenerator_CountGoalsScored_CorrectCountIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CountGoalsScored(teamName,_footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("Inter", 3)]
        [InlineData("AC Milan", 7)]
        [InlineData("Torino", 8)]
        [InlineData("Genoa", 7)]

        public void StandingsGenerator_CountGoalsConceded_CorrectCountIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CountGoalsConceded(teamName,_footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData(4,5,-1)]
        [InlineData(4,2,2)]
        public void StandingsGenerator_GoalDifferenceLogic(int scored,int conceded,int expected)
        {
            Assert.Equal(expected, scored - conceded);
        }


        [Theory]
        [InlineData("Inter", 2)]
        [InlineData("AC Milan", 1)]
        [InlineData("Torino", 0)]
        [InlineData("Genoa", 1)]

        public void StandingsGenerator_CountWins_CorrectCountIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CountWins(teamName, _footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("Inter", 1)]
        [InlineData("AC Milan", 1)]
        [InlineData("Torino", 1)]
        [InlineData("Genoa", 1)]

        public void StandingsGenerator_CountDraws_CorrectCountIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CountDraws(teamName, _footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }
        
        [Theory]
        [InlineData("Inter", 0)]
        [InlineData("AC Milan", 1)]
        [InlineData("Torino", 2)]
        [InlineData("Genoa", 1)]

        public void StandingsGenerator_CountLosses_CorrectCountIsGivenBasedOnAllResultsOfTheTeam(string teamName, int expected)
        {
            // arrange
            // act
            int actual = _service.CountLosses(teamName, _footballMatches);
            // assert
            Assert.Equal(expected, actual);
        }
    }
}