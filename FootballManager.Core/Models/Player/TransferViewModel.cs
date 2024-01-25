namespace FootballManager.Core.Models.Player
{
    using FootballManager.Core.Models.Menu;
    using FootballManager.Infrastructure.Data.DataModels;
    public class TransferViewModel
    {
        public VirtualTeam CurrentTeam { get; set; }
        public Player PlayerToBuy { get; set; }
        public List<Player> CurrentTeamPlayers { get; set; } = new List<Player>();
        public List<Player> FreeAgents { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<PlayerAttribute> PlayerAttributes { get; set; } = new List<PlayerAttribute>();
        public int PositionOrder { get; set; }
        public MenuViewModel MenuViewModel { get; set; }

    }
}
