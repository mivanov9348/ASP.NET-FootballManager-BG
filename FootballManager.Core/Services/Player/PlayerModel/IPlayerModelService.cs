namespace FootballManager.Core.Services.Player.PlayerModel
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    public interface IPlayerModelService
    {
        PlayerDetailsViewModel PlayerDetailsViewModel(Player currentPlayer, Game currentGame);
    }
}
