namespace ASP.NET_FootballManager.Services.Fixture
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class FixtureService : IFixtureService
    {
        private readonly FootballManagerDbContext data;
        public FixtureService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public void GenerateLeagueFixtures(Game game)
        {
            var allLeagues = this.data.Leagues.ToList();

            foreach (var item in allLeagues)
            {
                ResetLeagueFixtures(item);
                var currL = this.data.Leagues.FirstOrDefault(x => x.Id == item.Id);
                var teams = this.data.VirtualTeams.Where(x => x.LeagueId == currL.Id).ToList();
                ShuffleTeams(teams);
                var numOfMatches = teams.Count / 2 * (teams.Count - 1);
                int numFixt = 0;
                var round = 1;
                var day = 1;

                while (numFixt < numOfMatches)
                {
                    for (int i = 0; i < teams.Count() / 2; i += 1)
                    {
                        var htId = teams[i].Id;
                        var atId = teams[(teams.Count() - 1 - i)].Id;
                        var ht = this.data.VirtualTeams.FirstOrDefault(x => x.Id == htId);
                        var at = this.data.VirtualTeams.FirstOrDefault(x => x.Id == atId);

                        var newFixt = new Fixture
                        {
                            GameId = game.Id,
                            Round = round,
                            HomeTeam = ht,
                            AwayTeam = at,
                            HomeTeamName = ht.Name,
                            AwayTeamName = at.Name,
                            HomeTeamGoal = 0,
                            AwayTeamGoal = 0,
                            LeagueId = currL.Id,
                            HomeTeamId = htId,
                            AwayTeamId = atId,
                            Year = game.Year,
                            Day = day
                        };

                        this.data.Fixtures.Add(newFixt);
                        numFixt++;
                    }
                    round++;
                    day++;
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
        public void ResetLeagueFixtures(League league)
        {
            var allFixt = this.data.Fixtures.Where(x => x.LeagueId == league.Id).ToList();

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
        public List<Fixture> GetFixture(int id, int round)
        {
            if (id == 0 || round == 0)
            {
                return this.data.Fixtures.Where(x => x.Round == 1 && x.League.Level == 1).ToList();
            }
            else
            {
                return this.data.Fixtures.Where(x => x.LeagueId == id && x.Round == round).ToList();
            }
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
    }
}
