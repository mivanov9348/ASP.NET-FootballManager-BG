namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class TransferViewModel
    {
        public VirtualTeam CurrentTeam { get; set; }
        public Player PlayerToBuy { get; set; }
        public List<Player> CurrentTeamPlayers { get; set; } = new List<Player>();
        public List<Player> FreeAgents { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<Position> Positions { get; set; } = new List<Position>();
    }
}
