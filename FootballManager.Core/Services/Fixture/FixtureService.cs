namespace ASP.NET_FootballManager.Services.Fixture
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Services.Fixture;

    public class FixtureService : IFixtureService
    {
        private readonly FootballManagerDbContext data;
        private readonly FixturesHelpers helpers;
        private Random rnd;
        public FixtureService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
            this.helpers = new FixturesHelpers(data);
        }
        public void GenerateLeagueFixtures(Game game)
        {
            var allLeagues = data.Leagues.ToList();
            var leagueDays = data.Days
                .Where(x => x.IsLeagueDay && x.GameId == game.Id && x.Year.YearOrder == game.CurrentYearOrder)
                .ToList();

            foreach (var league in allLeagues)
            {
                var teams = data.VirtualTeams
                    .Where(x => x.LeagueId == league.Id && x.GameId == game.Id)
                    .ToList();
                ShuffleTeams(teams);

                int teamCount = teams.Count;
                int totalMatches = teamCount / 2 * (teamCount - 1);
                int fixtureCount = 0;
                int round = 1;

                while (fixtureCount < totalMatches)
                {
                    for (int i = 0; i < teamCount / 2; i++)
                    {
                        var homeTeam = teams[i];
                        var awayTeam = teams[teamCount - 1 - i];

                        var newFixture = new Fixture
                        {
                            GameId = game.Id,
                            Round = round,
                            HomeTeam = homeTeam,
                            AwayTeam = awayTeam,
                            HomeTeamName = homeTeam.Name,
                            AwayTeamName = awayTeam.Name,
                            CompetitionName = league.Name,
                            HomeTeamGoal = 0,
                            AwayTeamGoal = 0,
                            LeagueId = league.Id,
                            League = league,
                            HomeTeamId = homeTeam.Id,
                            AwayTeamId = awayTeam.Id
                        };

                        data.Fixtures.Add(newFixture);
                        fixtureCount++;
                    }

                    round++;
                    RotateTeams(teams);
                }
            }

            data.SaveChanges();
        }

        private void RotateTeams(List<VirtualTeam> teams)
        {
            if (teams.Count <= 2) return;

            var lastTeam = teams[^1];
            for (int i = teams.Count - 1; i > 1; i--)
            {
                teams[i] = teams[i - 1];
            }
            teams[1] = lastTeam;
        }
        public void GenerateCupFixtures(Game game)
        {
            var teamsInCup = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true && x.GameId == game.Id).ToList();
            var currentCup = this.data.Cups.First();


            while (teamsInCup.Count > 0)
            {
                var homeTeam = teamsInCup[rnd.Next(0, teamsInCup.Count - 1)];
                teamsInCup.Remove(homeTeam);
                var awayTeam = teamsInCup[rnd.Next(0, teamsInCup.Count - 1)];
                teamsInCup.Remove(awayTeam);

                var newFixt = new Fixture
                {
                    Round = game.CupRound,
                    GameId = game.Id,
                    HomeTeamName = homeTeam.Name,
                    AwayTeamName = awayTeam.Name,
                    CompetitionName = "Cup",
                    HomeTeamGoal = 0,
                    AwayTeamGoal = 0,
                    CupId = currentCup.Id,
                    HomeTeamId = homeTeam.Id,
                    AwayTeamId = awayTeam.Id,
                    IsPlayed = false
                };

                this.data.Fixtures.Add(newFixt);
                this.data.SaveChanges();
            }
        }
        public void DeleteFixtures(Game game)
        {
            var allFixt = this.data.Fixtures.Where(x => x.GameId == game.Id).ToList();

            foreach (var fixt in allFixt)
            {
                this.data.Fixtures.Remove(fixt);
            }
            this.data.SaveChanges();
        }
        public void ShuffleTeams(List<VirtualTeam> currl)
        {
            Random rnd = new Random();
            int n = currl.Count;

            for (int i = n - 1; i > 1; i--)
            {
                int random = rnd.Next(i + 1);

                var value = currl[random];
                currl[random] = currl[i];
                currl[i] = value;
            }
        }
        public async Task<List<Fixture>> GetFixture(int id, int round, Game CurrentGame)
        {
            if (id == 0 && round == 0 || id == 0 && round != 0)
            {
                return await Task.Run(() => this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == 1 && x.GameId == CurrentGame.Id).ToList());
            }
            if (id != 0 && round == 0)
            {
                return await Task.Run(() => this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == id && x.GameId == CurrentGame.Id).ToList());
            }

            return this.data.Fixtures.Where(x => x.LeagueId == id && x.Round == round && x.GameId == CurrentGame.Id).ToList();

        }
        public async Task<int> GetAllRounds(int? leagueId)
        {
            var allFixt = new List<Fixture>();
            if (leagueId == 0)
            {
                allFixt = await Task.Run(() => this.data.Fixtures.Where(x => x.League.Level == 1).ToList());
            }
            else
            {
                allFixt = await Task.Run(() => this.data.Fixtures.Where(x => x.LeagueId == leagueId).ToList());
            }
            allFixt.Reverse();
            int round = allFixt.First().Round;
            return round;
        }
        public void AddLeagueFixtureToDay(Game game)
        {
            var leagueFixtures = this.data.Fixtures.Where(x => x.League != null && x.Cup == null && x.EuropeanCup == null).ToList();
            var days = this.data.Days.Where(x => x.GameId == game.Id && x.Year.YearOrder == game.CurrentYearOrder && x.IsLeagueDay == true);

            var round = 0;
            foreach (var day in days)
            {
                round++;

                foreach (var league in leagueFixtures.Where(x => x.Round == round))
                {
                    league.DayId = day.Id;
                }
            }
            
            this.data.SaveChanges();
        }
    }
}
