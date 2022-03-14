namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using System.Collections.Generic;

    public class LeagueService : ILeagueService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public LeagueService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
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
                var day = 1;

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
                return this.data.VirtualTeams.Where(x => x.LeagueId == 1).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x=>x.GoalScored).ToList();
            }
            else
            {
                return this.data.VirtualTeams.Where(x => x.LeagueId == id).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalScored).ToList();

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
        public void CalculateOtherMatches(List<Fixture> fixtures, Fixture fixture)
        {
            fixtures.Remove(fixture);

            foreach (var fixt in fixtures)
            {
                var homeTeamGoal = GetTeamGoal(fixt.HomeTeamId);
                var awayTeamGoal = GetTeamGoal(fixt.AwayTeamId);
                SetFixtureGoal(fixt,homeTeamGoal,awayTeamGoal);
                GetGoalScorers(homeTeamGoal, fixt);
                GetGoalScorers(awayTeamGoal, fixt);
                CleanSheets(homeTeamGoal,awayTeamGoal,fixt);
                CheckWinner(homeTeamGoal, awayTeamGoal, fixt);
            }
        }
        private void SetFixtureGoal(Fixture fixt, int homeTeamGoal, int awayTeamGoal)
        {
            fixt.HomeTeamGoal = homeTeamGoal;
            fixt.AwayTeamGoal = awayTeamGoal;
            this.data.SaveChanges();
        }
        private void CleanSheets(int homeTeamGoal, int awayTeamGoal, Fixture fixt)
        {
            var htGk = this.data.Players.FirstOrDefault(x => x.PositionId == 1 && x.TeamId == fixt.HomeTeamId);
            var atGk = this.data.Players.FirstOrDefault(x => x.PositionId == 1 && x.TeamId == fixt.AwayTeamId);

            if (homeTeamGoal == 0)
            {
                atGk.CleanSheets += 1;
            }
            else
            {
                htGk.CleanSheets+=1;
            }
            this.data.SaveChanges();
        }
        private int GetTeamGoal(int teamId)
        {
            var currTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == teamId);
            var goals = rnd.Next(0, currTeam.Overall / 10);
            return goals;
        }
        public void CheckWinner(int homeGoals, int awayGoals, Fixture currentFixt)
        {
            var homeTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixt.HomeTeamId);
            var awayTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixt.AwayTeamId);

            homeTeam.Matches += 1;
            homeTeam.GoalScored += homeGoals;
            homeTeam.GoalAgainst += awayGoals;
            homeTeam.GoalDifference = homeTeam.GoalScored - homeTeam.GoalAgainst;

            awayTeam.Matches += 1;
            awayTeam.GoalScored += awayGoals;
            awayTeam.GoalAgainst += homeGoals;
            awayTeam.GoalDifference = awayTeam.GoalScored - awayTeam.GoalAgainst;

            if (homeGoals > awayGoals)
            {
                homeTeam.Wins += 1;
                homeTeam.Points += 3;
                awayTeam.Loses += 1;
            }

            if (homeGoals == awayGoals)
            {
                homeTeam.Draws += 1;
                homeTeam.Points += 1;
                awayTeam.Draws += 1;
                awayTeam.Points += 1;
            }

            if (homeGoals < awayGoals)
            {
                awayTeam.Wins += 1;
                awayTeam.Points += 3;
                homeTeam.Loses += 1;
            }

            this.data.SaveChanges();
        }
        private void GetGoalScorers(int Goals, Fixture currentFixt)
        {
            var playersWithoutGk = this.data.Players.Where(x => x.TeamId == currentFixt.HomeTeamId && x.PositionId != 1).ToList();
            var goalkeeper = this.data.Players.FirstOrDefault(x => x.TeamId == currentFixt.HomeTeamId && x.PositionId == 1);
           
            for (int i = 0; i < Goals; i++)
            {
                var player = playersWithoutGk[rnd.Next(0, playersWithoutGk.Count)];
                player.Goals += 1;
            }            

            this.data.SaveChanges();
        }
    }
}
