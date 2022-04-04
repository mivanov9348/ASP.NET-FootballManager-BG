namespace ASP.NET_FootballManager.Services.Fixture
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class FixtureService : IFixtureService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public FixtureService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
        }
        public void GenerateLeagueFixtures(Game game)
        {
            var allLeagues = this.data.Leagues.ToList();
            var leagueDays = this.data.Days.Where(x => x.isLeagueDay == true && x.GameId == game.Id && x.Year == game.Year).ToList();

            foreach (var item in allLeagues)
            {
                var currL = this.data.Leagues.FirstOrDefault(x => x.Id == item.Id);
                var teams = this.data.VirtualTeams.Where(x => x.LeagueId == currL.Id && x.GameId == game.Id).ToList();
                ShuffleTeams(teams);
                var numOfMatches = teams.Count / 2 * (teams.Count - 1);
                int numFixt = 0;
                var round = 1;
                var dayCount = 0;

                while (numFixt < numOfMatches)
                {
                    for (int i = 0; i < teams.Count() / 2; i += 1)
                    {
                        var htId = teams[i].Id;
                        var atId = teams[(teams.Count() - 1 - i)].Id;
                        var ht = this.data.VirtualTeams.FirstOrDefault(x => x.Id == htId);
                        var at = this.data.VirtualTeams.FirstOrDefault(x => x.Id == atId);
                        var currentDay = leagueDays.Skip(dayCount).First();

                        var newFixt = new Fixture
                        {
                            GameId = game.Id,
                            Round = round,
                            HomeTeam = ht,
                            AwayTeam = at,
                            HomeTeamName = ht.Name,
                            AwayTeamName = at.Name,
                            CompetitionName = item.Name,
                            HomeTeamGoal = 0,
                            AwayTeamGoal = 0,
                            LeagueId = currL.Id,
                            League = currL,
                            HomeTeamId = htId,
                            AwayTeamId = atId,
                            Day = currentDay,
                            DayId = currentDay.Id
                        };

                        this.data.Fixtures.Add(newFixt);
                        numFixt++;
                    }

                    round++;
                    dayCount++;

                    for (int i = teams.Count - 1; i > 1; i--)
                    {
                        VirtualTeam temp = teams[i - 1];
                        teams[i - 1] = teams[i];
                        teams[i] = temp;
                    }
                }
            }
            this.data.SaveChanges();
        }
        public void GenerateEuroFixtures(Game game)
        {
            var championsCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 1);
            var euroCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 2);

            var teamsInChampionsCup = this.data.VirtualTeams.Where(x => x.IsEuroParticipant == true && x.EuropeanCup.Rank == 1 && x.EuropeanCupId != null&&x.GameId==game.Id).ToList();
            var teamsInEuroCup = this.data.VirtualTeams.Where(x => x.IsEuroParticipant == true && x.EuropeanCup.Rank == 2 && x.EuropeanCupId != null && x.GameId == game.Id).ToList();

            var currentDay = this.data.Days.OrderBy(x => x.CurrentDay).FirstOrDefault(x => x.IsPlayed == false && x.GameId == game.Id && x.isEuroCupDay == true && x.Year == game.Year);

            if (currentDay != null)
            {
                while (teamsInChampionsCup.Count > 0)
                {
                    var homeTeam = teamsInChampionsCup[rnd.Next(0, teamsInChampionsCup.Count - 1)];
                    teamsInChampionsCup.Remove(homeTeam);
                    var awayTeam = teamsInChampionsCup[rnd.Next(0, teamsInChampionsCup.Count - 1)];
                    teamsInChampionsCup.Remove(awayTeam);

                    var newFixt = new Fixture
                    {
                        Round = game.EuroCupRound,
                        GameId = game.Id,
                        HomeTeamId = homeTeam.Id,
                        AwayTeamId = awayTeam.Id,
                        HomeTeamGoal = 0,
                        AwayTeamGoal = 0,
                        HomeTeamName = homeTeam.Name,
                        AwayTeamName = awayTeam.Name,
                        EuropeanCup = championsCup,
                        EuropeanCupId = championsCup.Id,
                        CompetitionName = "Champions Cup",
                        Day = currentDay,
                        DayId = currentDay.Id,
                        IsPlayed = false
                    };

                    this.data.Fixtures.Add(newFixt);
                    this.data.SaveChanges();
                }

                while (teamsInEuroCup.Count > 0)
                {
                    var homeTeam = teamsInEuroCup[rnd.Next(0, teamsInEuroCup.Count - 1)];
                    teamsInEuroCup.Remove(homeTeam);
                    var awayTeam = teamsInEuroCup[rnd.Next(0, teamsInEuroCup.Count - 1)];
                    teamsInEuroCup.Remove(awayTeam);

                    var newFixt = new Fixture
                    {
                        Round = game.EuroCupRound,
                        GameId = game.Id,
                        HomeTeamId = homeTeam.Id,
                        AwayTeamId = awayTeam.Id,
                        HomeTeamGoal = 0,
                        AwayTeamGoal = 0,
                        HomeTeamName = homeTeam.Name,
                        AwayTeamName = awayTeam.Name,
                        CompetitionName = "Euro Cup",
                        EuropeanCup = euroCup,
                        EuropeanCupId = euroCup.Id,
                        Day = currentDay,
                        DayId = currentDay.Id,
                        IsPlayed = false
                    };

                    this.data.Fixtures.Add(newFixt);
                    this.data.SaveChanges();
                }
            }


        }
        public void GenerateCupFixtures(Game game)
        {
            var teamsInCup = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true && x.GameId == game.Id).ToList();
            var currentCup = this.data.Cups.First();
            var currentDay = this.data.Days.OrderBy(x => x.CurrentDay).FirstOrDefault(x => x.GameId == game.Id && x.IsPlayed == false && x.isCupDay && x.Year == game.Year);

            if (currentDay != null)
            {
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
                        Day = currentDay,
                        DayId = currentDay.Id,
                        IsPlayed = false
                    };

                    this.data.Fixtures.Add(newFixt);
                    this.data.SaveChanges();
                }
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
        public List<Fixture> GetFixture(int id, int round, Game CurrentGame)
        {
            if (id == 0 && round == 0 || id == 0 && round != 0)
            {
                return this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == 1 && x.GameId == CurrentGame.Id).ToList();
            }
            if (id != 0 && round == 0)
            {
                return this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == id && x.GameId == CurrentGame.Id).ToList();
            }

            return this.data.Fixtures.Where(x => x.LeagueId == id && x.Round == round && x.GameId == CurrentGame.Id).ToList();

        }
        public int GetAllRounds(int leagueId)
        {
            var allFixt = new List<Fixture>();
            if (leagueId == 0)
            {
                allFixt = this.data.Fixtures.Where(x => x.League.Level == 1).ToList();
            }
            else
            {
                allFixt = this.data.Fixtures.Where(x => x.LeagueId == leagueId).ToList();
            }
            allFixt.Reverse();
            return allFixt.First().Round;
        }
        public void AddFixtureToDay(Game game)
        {
            var fixtureList = this.data.Fixtures.Where(x => x.GameId == game.Id);
            var days = this.data.Days.Where(x => x.GameId == game.Id && x.Year == game.Year);
            var round = 0;

            var leagueFixtures = fixtureList.Where(x => x.League != null && x.Cup == null && x.EuropeanCup == null).ToList();
            var cupFixtures = fixtureList.Where(x => x.League == null && x.Cup != null && x.EuropeanCup == null).ToList();
            var euroCupFixtures = fixtureList.Where(x => x.League == null && x.Cup == null && x.EuropeanCup != null).ToList();

            foreach (var day in days.Where(x => x.isLeagueDay))
            {
                round++;

                foreach (var league in leagueFixtures.Where(x => x.Round == round))
                {
                    league.DayId = day.Id;
                }
            }

            round = 0;
            foreach (var day in days.Where(x => x.isCupDay))
            {
                round++;

                foreach (var cup in cupFixtures.Where(x => x.Round == round))
                {
                    cup.DayId = day.Id;
                }
            }

            round = 0;
            foreach (var day in days.Where(x => x.isLeagueDay))
            {
                round++;

                foreach (var euroCup in euroCupFixtures.Where(x => x.Round == round))
                {
                    euroCup.DayId = day.Id;
                }
            }
            this.data.SaveChanges();
        }
    }
}
