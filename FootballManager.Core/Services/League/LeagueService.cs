namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Services;
    using FootballManager.Core.Services.Player.PlayerStats;
    using System.Collections.Generic;

    public class LeagueService : ILeagueService
    {
        private readonly FootballManagerDbContext data;
        private readonly IPlayerStatsService playerStatsService;
        private Random rnd;
        public LeagueService(FootballManagerDbContext data, IPlayerStatsService playerStatsService)
        {
            this.rnd = new Random();
            this.playerStatsService = playerStatsService;
            this.data = data;
        }
        public async Task<List<League>> GetAllLeagues() => await Task.Run(() => this.data.Leagues.ToList());
        public async Task<League> GetLeague(int id)
        {
            if (id == 0)
            {
                return await Task.Run(() => this.data.Leagues.FirstOrDefault(x => x.Id == 1));
            }
            else
            {
                return await Task.Run(() => this.data.Leagues.FirstOrDefault(x => x.Id == id));
            }

        }
        public async Task<List<VirtualTeam>> GetStandingsByLeague(int id, Game CurrentGame)
        {
            if (id == 0)
            {
                return await Task.Run(() => this.data.VirtualTeams.Where(x => x.LeagueId == 1 && x.GameId == CurrentGame.Id).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalScored).ToList());
            }
            else
            {
                return await Task.Run(() => this.data.VirtualTeams.Where(x => x.LeagueId == id && x.GameId == CurrentGame.Id).OrderByDescending(x => x.Points).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalDifference).ThenByDescending(x => x.GoalScored).ToList());

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
                GetGoalScorers(fixt.HomeTeam, homeTeamGoal, fixt);
                GetGoalConceded(fixt.AwayTeam, homeTeamGoal);
                GetGoalScorers(fixt.AwayTeam, awayTeamGoal, fixt);
                GetGoalConceded(fixt.HomeTeam, awayTeamGoal);
                CheckWinner(homeTeamGoal, awayTeamGoal, fixt);
            }
        }

        private void GetGoalConceded(VirtualTeam team, int goals)
        {
            var currentGoalkeeper = this.data.Players.FirstOrDefault(x => x.TeamId == team.Id && x.Position.Order == 1 && x.IsStarting11 == true);
            currentGoalkeeper.PlayerStats.GoalsConceded += goals;
            this.data.SaveChanges();
        }

        private void SetFixtureGoal(Fixture fixt, int homeTeamGoal, int awayTeamGoal)
        {
            fixt.HomeTeamGoal = homeTeamGoal;
            fixt.AwayTeamGoal = awayTeamGoal;
            this.data.SaveChanges();
        }
        private int GetTeamGoal(int? teamId)
        {
            var currTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == teamId);
            var currTeamPlayers = this.data.Players.Where(x => x.TeamId == teamId).ToList();
            double averageAttacking = 0;

            foreach (var player in currTeamPlayers)
            {
                var currentPlAttr = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == player.Id);
                double currentAttStats = currentPlAttr.Finishing + currentPlAttr.BallControll + currentPlAttr.Dribbling + currentPlAttr.Stamina + currentPlAttr.Strength;
                averageAttacking += currentAttStats;
            }
            averageAttacking /= 11;
            var goals = (averageAttacking + currTeam.Overall) / 10;
            var randomGoal = rnd.Next(0, Math.Min(10, (int)Math.Ceiling(goals)));
            return randomGoal;
        }
        public void CheckWinner(int homeGoals, int awayGoals, Fixture currentFixt)
        {
            var homeTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixt.HomeTeamId);
            var awayTeam = this.data.VirtualTeams.FirstOrDefault(x => x.Id == currentFixt.AwayTeamId);
            var currentDay = this.data.Days.FirstOrDefault(x => x.DayOrder == currentFixt.Day.DayOrder && x.Year == currentFixt.Day.Year);
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == currentFixt.GameId);
            var currentOptions = this.data.GameOptions.FirstOrDefault(x => x.Id == currentGame.GameOptionId);

            currentFixt.IsPlayed = true;
            currentDay.IsPlayed = true;

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
                homeTeam.Budget += currentOptions.WinCoins;
                awayTeam.Loses += 1;
            }

            if (homeGoals == awayGoals)
            {
                homeTeam.Draws += 1;
                homeTeam.Points += 1;
                homeTeam.Budget += currentOptions.DrawCoins;
                awayTeam.Draws += 1;
                awayTeam.Points += 1;
                awayTeam.Budget += currentOptions.DrawCoins;
            }

            if (homeGoals < awayGoals)
            {
                awayTeam.Wins += 1;
                awayTeam.Points += 3;
                awayTeam.Budget += currentOptions.WinCoins;
                homeTeam.Loses += 1;
            }

            this.data.SaveChanges();
        }
        private void GetGoalScorers(VirtualTeam currentTeam, int Goals, Fixture currentFixt)
        {
            var playersWithoutGk = this.data.Players.Where(x => x.TeamId == currentTeam.Id && x.Position.Name != "Goalkeeper").ToList();

            for (int i = 0; i < Goals; i++)
            {
                var player = playersWithoutGk[rnd.Next(0, playersWithoutGk.Count)];
                var currentPlayerStats = this.playerStatsService.GetPlayerStatsByPlayer(player);
                currentPlayerStats.Goals += 1;
            }

            this.data.SaveChanges();
        }
        public async Task PromotedRelegated(Game CurrentGame)
        {
            var leagues = this.data.Leagues.ToList();
            var championsCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 1);
            var euroCup = this.data.EuropeanCups.FirstOrDefault(x => x.Rank == 2);

            foreach (var league in leagues)
            {
                var standings = await GetStandingsByLeague(league.Id, CurrentGame);
                var nextLeagueTeams = new List<VirtualTeam>();
                var nextLeagueLevel = league.Level + 1;
                var nextLeague = this.data.Leagues.FirstOrDefault(x => x.Level == nextLeagueLevel && x.NationId == league.NationId);

                if (nextLeague != null)
                {
                    nextLeagueTeams = this.data.VirtualTeams.Where(x => x.LeagueId == nextLeague.Id).ToList();
                }

                var upLeagueLevel = league.Level - 1;
                var upLeague = this.data.Leagues.FirstOrDefault(x => x.Level == upLeagueLevel && x.NationId == league.NationId);

                if (league.Level == 1)
                {
                    var champion = standings.First();
                    var secondChampionsCupParticipant = standings.Skip(1).First();
                    var firstEuroCupParticipant = standings.Skip(2).First();
                    var secondEuroCupParticipant = standings.Skip(3).First();
                    standings.Reverse();
                    var firstRelegated = standings.First();
                    var secondRelegated = standings.Skip(1).First();

                    champion.Titles += 1;
                    champion.EuropeanCupId = championsCup.Id;
                    champion.IsEuroParticipant = true;
                    secondChampionsCupParticipant.EuropeanCupId = championsCup.Id;
                    secondChampionsCupParticipant.IsEuroParticipant = true;
                    firstEuroCupParticipant.EuropeanCupId = euroCup.Id;
                    firstEuroCupParticipant.IsEuroParticipant = true;
                    secondEuroCupParticipant.EuropeanCupId = euroCup.Id;
                    secondEuroCupParticipant.IsEuroParticipant = true;

                    if (nextLeague != null && nextLeagueTeams.Count > 0)
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

                        if (nextLeague != null && nextLeagueTeams.Count > 0)
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
