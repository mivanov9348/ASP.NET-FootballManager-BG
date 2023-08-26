namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public interface IDrawService
    {
        Draw CreateEliminationDraw(DrawViewModel model);
        Draw CreateGroupDraw(DrawViewModel model);
        VirtualTeam DrawTeam(Draw currentDraw);
        void FillEliminationTable(Draw currentDraw, VirtualTeam team);
        void FillGroupTable();
        void AutomaticFill(Draw currentDraw);
        void DeleteDraws();
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);
        DrawViewModel GetDrawViewModel(Draw currentDraw);
    }
}
