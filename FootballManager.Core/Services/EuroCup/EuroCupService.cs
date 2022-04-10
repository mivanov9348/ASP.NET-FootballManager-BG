namespace ASP.NET_FootballManager.Services.EuroCup
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.Collections.Generic;

    public class EuroCupService : IEuroCupService
    {
        private readonly FootballManagerDbContext data;
        private readonly Random rnd;
        public EuroCupService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }
        public void DistributionEuroParticipant(Game game)
        {
            RemoveAllEuroParticipants(game);
            var euroBgTeams = this.data.VirtualTeams.Where(x => x.GameId == game.Id && x.League.Nation.Name == "Bulgaria" && x.IsEuroParticipant == true).ToList();

            var championsCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 1);
            var euroCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 2);

            var championsCupParticipants = 0;
            var euroCupParticipants = 0;

            if (euroBgTeams.Count > 0)
            {
                championsCupParticipants = 30;
                euroCupParticipants = 30;
            }
            else
            {
                championsCupParticipants = 32;
                euroCupParticipants = 32;
            }

            var allEuroTeams = this.data.VirtualTeams.Where(x => x.IsEuroParticipant == true && x.GameId == game.Id && x.Name != "FreeAgents" && x.League.Nation.Name != "Bulgaria").ToList();

            for (int i = 0; i < championsCupParticipants; i++)
            {
                var team = allEuroTeams[rnd.Next(0, allEuroTeams.Count)];
                team.EuropeanCupId = championsCup.Id;
                allEuroTeams.Remove(team);
            }

            for (int i = 0; i < euroCupParticipants; i++)
            {
                var team = allEuroTeams[rnd.Next(0, allEuroTeams.Count)];
                team.EuropeanCupId = euroCup.Id;
                allEuroTeams.Remove(team);
            }

            this.data.SaveChanges();
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

                fixture.HomeTeamGoal = rnd.Next(0, homeTeam.Overall / 10);
                fixture.AwayTeamGoal = rnd.Next(0, awayTeam.Overall / 10);
                GetGoalScorers(fixture.HomeTeam, fixture.HomeTeamGoal, fixture);
                WinnerCalculate(fixture);
            }
            currentGame.EuroCupRound += 1;
            this.data.SaveChanges();
        }
        public void CheckWinner(Fixture currentFixture)
        {
            var homeTeamGoal = currentFixture.HomeTeamGoal;
            var awayTeamGoal = currentFixture.AwayTeamGoal;
            WinnerCalculate(currentFixture);
            this.data.SaveChanges();
        }
        private void GetGoalScorers(VirtualTeam currentTeam, int teamGoals, Fixture fixture)
        {
            var playersWithoutGk = this.data.Players.Where(x => x.TeamId == currentTeam.Id && x.Position.Name != "Goalkeeper").ToList();

            for (int i = 0; i < teamGoals; i++)
            {
                var player = playersWithoutGk[rnd.Next(0, playersWithoutGk.Count)];
                player.Goals += 1;
            }

            this.data.SaveChanges();
        }
        private void WinnerCalculate(Fixture currentFixture)
        {
            var homeTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixture.HomeTeamId);
            var awayTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixture.AwayTeamId);
            var currentDay = this.data.Days.FirstOrDefault(x => x.Id == currentFixture.DayId);

            var homeTeamGoal = currentFixture.HomeTeamGoal;
            var awayTeamGoal = currentFixture.AwayTeamGoal;

            currentFixture.IsPlayed = true;
            currentDay.IsPlayed = true;

            if (homeTeamGoal > awayTeamGoal)
            {
                currentFixture.WinnerTeamId = currentFixture.HomeTeamId;
                awayTeam.EuropeanCupId = null;
            }
            if (homeTeamGoal < awayTeamGoal)
            {
                currentFixture.WinnerTeamId = currentFixture.AwayTeamId;
                homeTeam.EuropeanCupId = null;
            }
            if (homeTeamGoal == awayTeamGoal)
            {
                var goal = rnd.Next(0, 2);
                if (goal == 0)
                {
                    currentFixture.HomeTeamGoal += 1;
                    currentFixture.WinnerTeamId = currentFixture.HomeTeamId;
                    awayTeam.EuropeanCupId = null;
                }
                else
                {
                    currentFixture.AwayTeamGoal += 1;
                    currentFixture.WinnerTeamId = currentFixture.AwayTeamId;
                    homeTeam.EuropeanCupId = null;
                }
            }
            this.data.SaveChanges();
        }
        private void RemoveAllEuroParticipants(Game game)
        {
            var allEuroTeams = this.data.VirtualTeams.Where(x => x.GameId == game.Id && x.IsEuroParticipant == true && x.League.Nation.Name != "Bulgaria").ToList();
            allEuroTeams.ForEach(x => x.EuropeanCupId = null);
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
