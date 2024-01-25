namespace FootballManager.Core.Services.Draw.Common
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;

    public class CommonDrawService : ICommonDrawService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public CommonDrawService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }

        public VirtualTeam DrawTeam(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);
            var randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];
            randomDrawTeam.isDrawed = true;
            this.data.SaveChanges();
            return randomDrawTeam;
        }
        public Draw GetDrawById(int id) => this.data.Draws
                                           .Include(draw => draw.Teams)
                                           .Include(draw => draw.Fixtures)
                                           .FirstOrDefault(x => x.Id == id);
        public List<VirtualTeam> GetRemainingTeams(Draw currentDraw)
        {
            var currentDrawTeams = currentDraw.Teams;

            if (currentDraw.CupId != null)
            {
                currentDrawTeams = currentDrawTeams.Where(x => x.isDrawed == false && x.CupId == currentDraw.CupId).ToList();
            }
            if (currentDraw.ContinentalCupId != null)
            {
                currentDrawTeams = currentDrawTeams.Where(x => x.isDrawed == false && x.EuropeanCupId == currentDraw.ContinentalCupId).ToList();
            }
            return currentDrawTeams;
        }
        public (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) GetCurrentDrawDay(Game currentGame)
        {
            var europeanTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsEuroParticipant != false).ToList();
            var bulgarianCupTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsCupParticipant != false).ToList();

            var championsCup = this.data.ContinentalCups.FirstOrDefault(x => x.Rank == 1);
            var europeanCup = this.data.ContinentalCups.FirstOrDefault(x => x.Rank == 2);

            var areChampionsCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false && x.EuropeanCupId == championsCup.Id);
            var areEuropeanCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false && x.EuropeanCupId == europeanCup.Id);
            var areCupNotDrawedTeams = bulgarianCupTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false);

            return (areChampionsCupNotDrawedTeams, areEuropeanCupNotDrawedTeams, areCupNotDrawedTeams);
        }
        public void SaveDraw(Draw currentDraw)
        {
            currentDraw.IsDrawStarted = false;
            this.data.SaveChanges();
        }
        public Draw CreateContinentalCupDraw(Game currentGame, List<Fixture> currentFixtures, ContinentalCup currentCup)
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
        public Draw CreateDomesticCupDraw(Game currentGame, List<VirtualTeam> currentCupTeams, List<Fixture> currentFixtures, Cup currentCup)
        {


            var newDraw = new Draw
            {
                Teams = currentCupTeams,
                Game = currentGame,
                GameId = currentGame.Id,
                Fixtures = currentFixtures,
                CupId = currentCup.Id,       
                IsDrawStarted = true
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }
        public List<Fixture> FillFixtures(Day currentDay, DrawViewModel model, Object currentCup)
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

    }
}
