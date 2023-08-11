using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
using FootballManager.Core.Models.Player;
using FootballManager.Core.Models.Sorting;

namespace FootballManager.Core.Services.Player.PlayerSorter
{
    public interface IPlayerSorterService
    {
        PlayersViewModel SortingPlayers(PlayerStatsSorting sortBy, int id, Game currentGame);
    }
}
