namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public interface IDrawService
    {
        Draw CreateDraw(DrawViewModel model);
        void DrawTeam(Draw currentDraw);
        void FinalizeDraw(Draw currentDraw);
        Draw GetDrawById(int id);
        List<VirtualTeam> GetRemainingTeams(Draw currentDraw);
        DrawViewModel GetDrawViewModel(Draw currentDraw);
    }
}
