namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public interface IDrawService
    {
        Draw CreateEliminationDraw(DrawViewModel model);
        Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame);
        VirtualTeam DrawTeam(Draw currentDraw);
        void FillEliminationTable(Draw currentDraw, VirtualTeam team);
        void FillGroupTable(Draw currentDraw, VirtualTeam team);
        void AutomaticFill(Draw currentDraw);
        void DeleteDraws();
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);
        DrawViewModel GetDrawViewModel(Draw currentDraw);
        GroupDrawViewModel GetGroupDrawViewModel(Draw currentDraw);

    }
}
