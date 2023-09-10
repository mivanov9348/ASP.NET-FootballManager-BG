namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IDrawService
    {
        (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) GetCurrentDrawDay(Game currentGame);
        Draw CreateEliminationDraw(Game currentGame,DrawViewModel model);
        Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame);
        VirtualTeam DrawTeam(Draw currentDraw);
        (string,string) FillGroupTable(Draw currentDraw,VirtualTeam team);
        void DeleteDraws();
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);

    }
}
