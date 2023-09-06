namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    public interface IDrawService
    {
        Draw CreateEliminationDraw(DrawViewModel model);
        Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame);
        VirtualTeam DrawTeam(Draw currentDraw);
        void FillEliminationTable(Draw currentDraw, VirtualTeam team);
        (string,string) FillGroupTable(Draw currentDraw,VirtualTeam team);
     //   void AutoCompleteElimination(Draw currentDraw);
        void AutoCompleteGroup(Draw currentDraw);
        void DeleteDraws();
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);

    }
}
