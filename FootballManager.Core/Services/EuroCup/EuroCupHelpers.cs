namespace FootballManager.Core.Services.EuroCup
{
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class EuroCupHelpers
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        private readonly Random rnd;
        public EuroCupHelpers(FootballManagerDbContext data)
        {
            this.constants = new DataConstants();
            this.data = data;
            this.rnd = new Random();
        }


        public void FillEuropeanCompetitions(EuropeanCup currentCup)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.IsEuroParticipant == true && x.EuropeanCupId == null && x.GameId == currentCup.GameId).ToList();
            var euroBgTeams = this.data.VirtualTeams.Where(x => x.GameId == currentCup.GameId && x.League.Nation.Name == "Bulgaria" && x.IsEuroParticipant == true).ToList();

            var restParticipantsCount = currentCup.Participants - euroBgTeams.Count();

            for (int i = 0; i < restParticipantsCount; i++)
            {
                allTeams = this.data.VirtualTeams.Where(x => x.IsEuroParticipant == true && x.EuropeanCupId == null && x.GameId == currentCup.GameId).ToList();
                var randomTeam = allTeams[rnd.Next(0, allTeams.Count())];

                randomTeam.IsEuroParticipant = true;
                randomTeam.EuropeanCupId = currentCup.Id;
                randomTeam.EuropeanCup = currentCup;
                this.data.SaveChanges();
            }
          
        }

        public void GetGoalScorers(VirtualTeam currentTeam, int teamGoals, Fixture fixture)
        {
            var playersWithoutGk = this.data.Players.Where(x => x.TeamId == currentTeam.Id && x.Position.Name != "Goalkeeper").ToList();

            for (int i = 0; i < teamGoals; i++)
            {
                var player = playersWithoutGk[rnd.Next(0, playersWithoutGk.Count)];
                var playerStats = this.data.PlayerStats.FirstOrDefault(x => x.PlayerId == player.Id);
                playerStats.Goals += 1;
            }

            this.data.SaveChanges();
        }
        public void WinnerCalculate(Fixture currentFixture)
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

    }
}
