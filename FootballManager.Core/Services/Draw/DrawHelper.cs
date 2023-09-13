namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices.ObjectiveC;

    internal class DrawHelper
    {
        private FootballManagerDbContext data;
        private Random rnd;
        public DrawHelper(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }
        internal Draw CreateContinentalCupDraw(Game currentGame, List<Fixture> currentFixtures, ContinentalCup currentCup)
        {
            var currentTeams = this.data.VirtualTeams.Where(x => x.EuropeanCupId == currentCup.Id).ToList();
            var newDraw = new Draw
            {
                Teams = currentTeams,
                Game = currentGame,
                GameId = currentGame.Id,
                Fixtures = currentFixtures,
                ContinentalCupId = currentCup.Id,
                IsDrawStarted = true
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }
        internal Draw CreateDomesticCupDraw(Game currentGame, List<VirtualTeam> currentCupTeams, List<Fixture> currentFixtures, Cup currentCup)
        {
            var currentRound = GetCurrentRound(currentCup.Participants);

            var newDraw = new Draw
            {
                Teams = currentCupTeams,
                Game = currentGame,
                GameId = currentGame.Id,
                Fixtures = currentFixtures,
                CupId = currentCup.Id,
                Round = currentRound,               
                IsDrawStarted = true
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }
        internal List<Fixture> FillFixtures(Day currentDay, DrawViewModel model, Object currentCup)
        {
            var fixtures = new List<Fixture>();
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == currentDay.GameId);

            for (int i = 0; i < model.NumberOfTeams / 2; i++)
            {
                var newFixture = new Fixture
                {
                    GameId = currentGame.Id,
                    Day = currentDay,
                    DayId = currentDay.Id
                };
                fixtures.Add(newFixture);

                if (currentCup.GetType() == typeof(ContinentalCup))
                {
                    var continentalCup = (ContinentalCup)currentCup;
                    newFixture.EuropeanCupId = continentalCup.Id;
                }
                if (currentCup.GetType() == typeof(Cup))
                {
                    var domesticCup = (Cup)currentCup;
                    newFixture.CupId = domesticCup.Id;
                }
            }
            this.data.Fixtures.AddRange(fixtures);
            this.data.SaveChanges();
            return fixtures;
        }
        internal List<VirtualTeam> FillTeams(List<VirtualTeam> allTeams, DrawViewModel model)
        {
            var teams = new List<VirtualTeam>();

            for (int i = 0; i < model.NumberOfTeams; i++)
            {
                var team = allTeams[rnd.Next(0, allTeams.Count)];
                while (IsExist(team, teams))
                {
                    team = allTeams[rnd.Next(0, allTeams.Count)];

                }

                team.isDrawed = false;
                teams.Add(team);
            }
            this.data.SaveChanges();
            return teams;
        }
        internal bool IsExist(VirtualTeam team, List<VirtualTeam> teams)
        {
            if (teams.Contains(team))
            {
                return true;
            }

            return false;
        }
        internal int GetCurrentRound(int participants)
        {
            int rounds = 0;
            int remainingParticipants = participants;

            while (remainingParticipants > 1)
            {
                remainingParticipants = (remainingParticipants + 1) / 2; // Divide by 2, rounding up
                rounds++;
            }

            return rounds;
        }

    }
}
