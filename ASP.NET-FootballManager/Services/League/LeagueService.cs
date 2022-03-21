namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using System.Collections.Generic;
    using Data.Constant;

    public class LeagueService : ILeagueService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public LeagueService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
        }     
        public List<League> GetAllLeagues() => this.data.Leagues.ToList(); 
        public League GetLeague(int id)
        {
            if (id == 0)
            {
                return this.data.Leagues.FirstOrDefault(x => x.Id == 1);
            }
            else
            {
                return this.data.Leagues.FirstOrDefault(x => x.Id == id);
            }

        }
        public List<VirtualTeam> GetStandingsByLeague(int id)
        {
            if (id == 0)
            {
                return this.data.VirtualTeams.Where(x => x.LeagueId == 1).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalScored).ToList();
            }
            else
            {
                return this.data.VirtualTeams.Where(x => x.LeagueId == id).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalScored).ToList();

            }
        }      
        public void CalculateOtherMatches(List<Fixture> fixtures, Fixture fixture)
        {
            fixtures.Remove(fixture);

            foreach (var fixt in fixtures)
            {
                var homeTeamGoal = GetTeamGoal(fixt.HomeTeamId);
                var awayTeamGoal = GetTeamGoal(fixt.AwayTeamId);
                SetFixtureGoal(fixt, homeTeamGoal, awayTeamGoal);
                GetGoalScorers(homeTeamGoal, fixt);
                GetGoalScorers(awayTeamGoal, fixt);
                CleanSheets(homeTeamGoal, awayTeamGoal, fixt);
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
                htGk.CleanSheets += 1;
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
                homeTeam.Budget += DataConstants.Prize.WinCoins;
                awayTeam.Loses += 1;
            }

            if (homeGoals == awayGoals)
            {
                homeTeam.Draws += 1;
                homeTeam.Points += 1;
                homeTeam.Budget += DataConstants.Prize.DrawCoins;
                awayTeam.Draws += 1;
                awayTeam.Points += 1;
                awayTeam.Budget += DataConstants.Prize.DrawCoins;
            }

            if (homeGoals < awayGoals)
            {
                awayTeam.Wins += 1;
                awayTeam.Points += 3;
                awayTeam.Budget += DataConstants.Prize.WinCoins;
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
        public void PromotedRelegated(Game CurrentGame)
        {
            var leagues = this.data.Leagues.ToList();

            foreach (var league in leagues)
            {
                var standings = GetStandingsByLeague(league.Id);
                var nextLeagueLevel = league.Level += 1;
                var nextLeague = this.data.Leagues.FirstOrDefault(x => x.Level == nextLeagueLevel && x.NationId == league.NationId);
                var upLeagueLevel = league.Level -= 1;
                var upLeague = this.data.Leagues.FirstOrDefault(x => x.Level == upLeagueLevel && x.NationId == league.NationId);

                if (league.Level == 1)
                {
                    var champion = standings.First();
                    champion.Titles += 1;

                    standings.Reverse();
                    var firstRelegated = standings.First();
                    var secondRelegated = standings.Skip(1).First();

                    if (nextLeague != null)
                    {
                        firstRelegated.LeagueId = nextLeague.Id;
                        secondRelegated.LeagueId = nextLeague.Id;
                    }

                }
                else
                {
                    if (standings.Count > 0)
                    {
                        var firstPromoted = standings.First();
                        var secondPromoted = standings.Skip(1).First();
                        firstPromoted.LeagueId = upLeague.Id;
                        secondPromoted.LeagueId = upLeague.Id;

                        if (nextLeague != null)
                        {
                            standings.Reverse();
                            var firstRelegated = standings.First();
                            var secondRelegated = standings.Skip(1).First();
                            firstRelegated.LeagueId = nextLeague.Id;
                            secondRelegated.LeagueId = nextLeague.Id;
                        }
                    }
                }
            }
            this.data.SaveChanges();

        }
      
    }
}
