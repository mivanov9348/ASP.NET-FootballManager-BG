namespace ASP.NET_FootballManager.Services.EuroCup
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using FootballManager.Core.Services.EuroCup;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System.Collections.Generic;

    public class EuroCupService : IEuroCupService
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        private readonly EuroCupHelpers helpers;
        private readonly Random rnd;
        public EuroCupService(FootballManagerDbContext data)
        {
            this.data = data;
            this.helpers = new EuroCupHelpers(data);
            this.constants = new DataConstants();
            this.rnd = new Random();
        }
        public void RemoveEuropeanParticipants(Game currentGame)
        {
            var currentEuroCupTeams = this.data.VirtualTeams.Where(x => x.EuropeanCupId != null && x.GameId == currentGame.Id && x.League.Nation.Name != "Bulgaria").ToList();
            currentEuroCupTeams.ForEach(x => x.EuropeanCupId = null);
            this.data.SaveChanges();
        }
        public void CreateChampionsCup(Game game, Year year)
        {
            var newChampionsLeague = new EuropeanCup
            {
                Name = DataConstants.ChampionsCup.Name,
                Rank = DataConstants.ChampionsCup.Rank,
                Game = game,
                GameId = game.Id,
                Participants = DataConstants.ChampionsCup.Participants,
                Rounds = DataConstants.ChampionsCup.Rounds,
                Year = year.YearOrder
            };
            this.data.EuropeanCups.Add(newChampionsLeague);
            this.data.SaveChanges();
        }
        public void CreateEuroCup(Game game, Year year)
        {
            var newEuroCup = new EuropeanCup
            {
                Name = DataConstants.EuroCup.Name,
                Rank = DataConstants.EuroCup.Rank,
                Game = game,
                GameId = game.Id,
                Participants = DataConstants.EuroCup.Participants,
                Rounds = DataConstants.EuroCup.Rounds,
                Year = year.YearOrder
            };
            this.data.EuropeanCups.Add(newEuroCup);
            this.data.SaveChanges();
        }
        public void FillChampionsCupParticipants(Game game)
        {
            var championsCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 1);
            this.helpers.FillEuropeanCompetitions(championsCup);
        }
        public void FillEuroCupParticipants(Game game)
        {
            var euroCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 2);
            this.helpers.FillEuropeanCompetitions(euroCup);
        }
        public void CalculateOtherMatches(List<Fixture> dayFixtures, Fixture currentFixture)
        {
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == dayFixtures.First().GameId);

            if (currentFixture != null)
            {
                dayFixtures = dayFixtures.Where(x => x.Id != currentFixture.Id).ToList();
            }

            foreach (var fixture in dayFixtures)
            {
                var homeTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == fixture.HomeTeamId);
                var awayTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == fixture.AwayTeamId);

                var homeTeamOverall = (int)Math.Ceiling(homeTeam.Overall / 10.0 + 2);
                var awayTeamOverall = (int)Math.Ceiling(homeTeam.Overall / 10.0 + 2);

                fixture.HomeTeamGoal = rnd.Next(0, homeTeamOverall);
                fixture.AwayTeamGoal = rnd.Next(0, awayTeamOverall);
                helpers.GetGoalScorers(fixture.HomeTeam, fixture.HomeTeamGoal, fixture);
                helpers.WinnerCalculate(fixture);
            }
            currentGame.EuroCupRound += 1;
            this.data.SaveChanges();
        }
        public void CheckWinner(Fixture currentFixture)
        {
            var homeTeamGoal = currentFixture.HomeTeamGoal;
            var awayTeamGoal = currentFixture.AwayTeamGoal;
            helpers.WinnerCalculate(currentFixture);
            this.data.SaveChanges();
        }

        public async Task<List<EuropeanCup>> AllEuroCups() => await Task.Run(() => this.data.EuropeanCups.ToList());
        public async Task<EuropeanCup> GetEuropeanCup(int cupId) => await Task.Run(() => this.data.EuropeanCups.FirstOrDefault(x => x.Id == cupId));
        public async Task<VirtualTeam> GetChampionsCupWinner(Game game)
        {
            var championsCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 1);
            var finalMatch = this.data.Fixtures.OrderByDescending(x => x.Id).FirstOrDefault(x => x.GameId == game.Id && x.EuropeanCupId == championsCup.Id);
            var winner = this.data.VirtualTeams.FirstOrDefault(x => x.Id == finalMatch.WinnerTeamId);
            winner.ChampionsCup += 1;
            this.data.SaveChanges();
            return await Task.Run(() => winner);
        }
        public async Task<VirtualTeam> GetEuroCupWinner(Game game)
        {
            var euroCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 2);
            var finalMatch = this.data.Fixtures.OrderByDescending(x => x.Id).FirstOrDefault(x => x.GameId == game.Id && x.EuropeanCupId == euroCup.Id);
            var winner = this.data.VirtualTeams.FirstOrDefault(x => x.Id == finalMatch.WinnerTeamId);
            winner.EuroCups += 1;
            return await Task.Run(() => winner);
        }
        public async Task<List<Fixture>> GetEuroCupFixtures(Game CurrentGame, int euroCupRank) => await Task.Run(() => this.data.Fixtures.Where(x => x.GameId == CurrentGame.Id && x.EuropeanCupId == euroCupRank).ToList());

    }
}
