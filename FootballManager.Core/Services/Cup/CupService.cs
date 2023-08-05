namespace ASP.NET_FootballManager.Services.Cup
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.Collections.Generic;

    public class CupService : ICupService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public CupService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }
        public void GenerateCupParticipants(Game curentGame)
        {
            RemoveAllTeamsFromCup(curentGame);
            var currentCup = this.data.Cups.First();
            var teamsParticipants = this.data.VirtualTeams.Where(x => x.League.Nation.Name == "Bulgaria" && x.GameId == curentGame.Id).ToList();

            foreach (var team in teamsParticipants)
            {
                team.IsCupParticipant = true;
            }
            this.data.SaveChanges();
        }
        public void CheckWinner(Fixture currentFixture)
        {
            var homeTeamGoal = currentFixture.HomeTeamGoal;
            var awayTeamGoal = currentFixture.AwayTeamGoal;
            WinnerCalculate(currentFixture);
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

                var homeTeamOverall = (int)Math.Ceiling(homeTeam.Overall / 10.0 + 2);
                var awayTeamOverall = (int)Math.Ceiling(homeTeam.Overall / 10.0 + 2);

                fixture.HomeTeamGoal = rnd.Next(0, homeTeamOverall);
                fixture.AwayTeamGoal = rnd.Next(0, awayTeamOverall);

                GetGoalScorers(fixture.HomeTeam, fixture.HomeTeamGoal, fixture);
                WinnerCalculate(fixture);
            }
            currentGame.CupRound += 1;
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
                awayTeam.IsCupParticipant = false;
            }
            if (homeTeamGoal < awayTeamGoal)
            {
                currentFixture.WinnerTeamId = currentFixture.AwayTeamId;
                homeTeam.IsCupParticipant = false;
            }
            if (homeTeamGoal == awayTeamGoal)
            {
                var goal = rnd.Next(0, 2);
                if (goal == 0)
                {
                    currentFixture.HomeTeamGoal += 1;
                    currentFixture.WinnerTeamId = currentFixture.HomeTeamId;
                    awayTeam.IsCupParticipant = false;
                }
                else
                {
                    currentFixture.AwayTeamGoal += 1;
                    currentFixture.WinnerTeamId = currentFixture.AwayTeamId;
                    homeTeam.IsCupParticipant = false;
                }
            }
            this.data.SaveChanges();
        }
        private void RemoveAllTeamsFromCup(Game curentGame)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == curentGame.Id).ToList();
            allTeams.ForEach(x => x.IsCupParticipant = false);
            this.data.SaveChanges();
        }

        public async Task<Cup> GetCurrentCup() => await Task.Run(() => this.data.Cups.First());
        public async Task<VirtualTeam> GetWinner(Game game)
        {
            var cup = this.data.Cups.FirstOrDefault();
            var finalMatch = this.data.Fixtures.OrderByDescending(x => x.Id).FirstOrDefault(x => x.GameId == game.Id && x.CupId == cup.Id);
            var winner = this.data.VirtualTeams.FirstOrDefault(x => x.Id == finalMatch.WinnerTeamId);
            winner.Cups += 1;
            this.data.SaveChanges();    
            return await Task.Run(() => winner);
        }
        public async Task<List<Fixture>> GetCupFixtures(Game CurrentGame) => await Task.Run(() => this.data.Fixtures.Where(x => x.GameId == CurrentGame.Id && x.CupId != null).ToList());
    }
}
