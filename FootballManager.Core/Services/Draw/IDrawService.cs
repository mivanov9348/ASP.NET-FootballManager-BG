namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IDrawService
    {
        (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) GetCurrentDrawDay(Game currentGame);
        Draw CreateContinentalCupEliminationDraw(Game currentGame,DrawViewModel model, ContinentalCup currentCup);
        Draw CreateDomesticCupEliminationDraw(Game currentGame, DrawViewModel model, Cup currentCup);
        Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame);
        VirtualTeam DrawTeam(Draw currentDraw);
        (string,string) FillGroupTable(Draw currentDraw,VirtualTeam team);
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);
        void FillEliminationFixtures(Draw currentDraw, VirtualTeam drawedTeam);
        void SaveDraw(Draw currentDraw);
    }
}
