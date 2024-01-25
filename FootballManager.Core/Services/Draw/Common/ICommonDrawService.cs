namespace FootballManager.Core.Services.Draw.Common
{
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System;
    using System.Collections.Generic;
    public interface ICommonDrawService
    {
        (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) GetCurrentDrawDay(Game currentGame);
        VirtualTeam DrawTeam(Draw currentDraw);
        Draw GetDrawById(int id);
        void SaveDraw(Draw currentDraw);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);
        Draw CreateContinentalCupDraw(Game currentGame, List<Fixture> currentFixtures, ContinentalCup currentCup);
        Draw CreateDomesticCupDraw(Game currentGame, List<VirtualTeam> currentCupTeams, List<Fixture> currentFixtures, Cup currentCup);
        List<Fixture> FillFixtures(Day currentDay, DrawViewModel model, Object currentCup);
    }
}
