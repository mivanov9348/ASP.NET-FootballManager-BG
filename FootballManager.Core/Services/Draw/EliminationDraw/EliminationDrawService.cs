﻿namespace FootballManager.Core.Services.Draw.EliminationDraw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services.Calendar;
    using FootballManager.Core.Services.Draw.Common;
    using FootballManager.Infrastructure.Data.DataModels;

    public class EliminationDrawService : IEliminationDrawService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;      
        private ICalendarService calendarService;
        private ICommonDrawService commonDrawService;
        public EliminationDrawService(FootballManagerDbContext data, ICalendarService calendarService, ICommonDrawService commonDrawService)
        {
            this.data = data;
            this.rnd = new Random();
            this.calendarService = calendarService;
            this.commonDrawService = commonDrawService;
        }

        public Draw CreateContinentalCupEliminationDraw(Game currentGame, DrawViewModel model, ContinentalCup currentCup)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsEuroParticipant && x.isDrawed == false).ToList();
            var currentDay = calendarService.GetCurrentDate(currentGame);

            var draw = new Draw();

            var currentCupFixtures = commonDrawService.FillFixtures(currentDay.day, model, currentCup);
            draw = commonDrawService.CreateContinentalCupDraw(currentGame, currentCupFixtures, currentCup);

            return draw;
        }
        public Draw CreateDomesticCupEliminationDraw(Game currentGame, DrawViewModel model, Cup currentCup)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsCupParticipant && x.isDrawed == false).ToList();
            var currentDay = calendarService.GetCurrentDate(currentGame);

            var draw = new Draw();

            var currentCupFixtures = commonDrawService.FillFixtures(currentDay.day, model, currentCup);
            draw = commonDrawService.CreateDomesticCupDraw(currentGame, allTeams, currentCupFixtures, currentCup);
            return draw;
        }
        public void FillEliminationFixtures(Draw currentDraw, VirtualTeam team)
        {
            foreach (var fixture in currentDraw.Fixtures)
            {
                if (fixture.HomeTeamId == null)
                {
                    fixture.HomeTeamName = team.Name;
                    fixture.HomeTeam = team;
                    fixture.HomeTeamId = team.Id;
                    this.data.SaveChanges();
                    break;
                }
                else if (fixture.AwayTeamId == null)
                {
                    fixture.AwayTeamName = team.Name;
                    fixture.AwayTeam = team;
                    fixture.AwayTeamId = team.Id;
                    this.data.SaveChanges();
                    break;
                }
            }
        }
    }
}
