namespace FootballManager.Core.Services.Player.PlayerModel
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    public interface IPlayerModelService
    {
        PlayerDetailsViewModel PlayerDetailsViewModel(Player currentPlayer, Game currentGame);
    }
}
