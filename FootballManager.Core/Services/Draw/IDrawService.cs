namespace FootballManager.Core.Services.Draw
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public interface IDrawService
    {
        Draw CreateDraw(DrawViewModel model);
        VirtualTeam DrawTeam(DrawViewModel model);
    }
}
