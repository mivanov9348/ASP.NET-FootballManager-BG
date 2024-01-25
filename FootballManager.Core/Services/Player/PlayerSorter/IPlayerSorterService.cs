namespace FootballManager.Core.Services.Player.PlayerSorter
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    using FootballManager.Core.Models.Sorting;
    public interface IPlayerSorterService
    {
        PlayersViewModel SortingPlayers(PlayerStatsSorting sortBy, int id, Game currentGame, int pageNum);
    }
}
