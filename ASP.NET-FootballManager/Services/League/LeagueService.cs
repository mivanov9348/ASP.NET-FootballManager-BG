namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using System.Collections.Generic;

    public class LeagueService : ILeagueService
    {
        private readonly FootballManagerDbContext data;
        public LeagueService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public void GenerateFixtures(Game game)
        {
            var allLeagues = this.data.Leagues.ToList();

            foreach (var item in allLeagues)
            {
                var currL = this.GetLeague(item.Id);
                Shuffle(currL.Teams);
                var numOfMatches = currL.Teams.Count / 2 * (currL.Teams.Count - 1);
                int numFixt = 0;
                var round = 1;

                while (numFixt < numOfMatches)
                {
                    for (int i = 0; i < currL.Teams.Count() / 2; i += 1)
                    {
                        var htId = currL.Teams[i].Id;
                        var atId = currL.Teams[(currL.Teams.Count() - 1 - i)].Id;
                        var ht = this.data.VirtualTeams.FirstOrDefault(x => x.Id == htId);
                        var at = this.data.VirtualTeams.FirstOrDefault(x => x.Id == atId);

                        var newFixt = new Fixture
                        {
                            GameId = game.Id,
                            Round = round,
                            HomeTeamName = ht.Name,
                            AwayTeamName = at.Name,
                            HomeTeamGoal = 0,
                            AwayTeamGoal = 0,
                            LeagueId = currL.Id,
                            HomeTeamId = htId,
                            AwayTeamId = atId,
                            Year = game.Year

                        };

                        this.data.Fixtures.Add(newFixt);
                        numFixt++;
                    }
                    round++;
                    for (int i = currL.Teams.Count - 1; i > 1; i--)
                    {
                        VirtualTeam temp = currL.Teams[i - 1];
                        currL.Teams[i - 1] = currL.Teams[i];
                        currL.Teams[i] = temp;
                    }
                }
            }
            this.data.SaveChanges();
        }
        public List<League> GetAllLeagues() => this.data.Leagues.ToList();
        public LeagueViewModel GetLeague(int id)
        {
            var currL = this.data.Leagues.FirstOrDefault(x => x.Id == id);

            var l = new LeagueViewModel
            {
                Id = currL.Id,
                Name = currL.Name,
                Fixtures = this.data.Fixtures.ToList(),
                Teams = this.data.VirtualTeams.Where(x => x.LeagueId == id).ToList()
            };

            return l;
        }

        public List<VirtualTeam> GetStandingsByLeague(int id)
        {
            if (id == 0)
            {
                return this.data.VirtualTeams.Where(x => x.LeagueId == 1).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ToList();
            }
            else
            {
                return this.data.VirtualTeams.Where(x => x.LeagueId == id).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ToList();

            }

        }

        public void Shuffle(List<VirtualTeam> currl)
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
    }
}
