using FootballManager.Infrastructure.Data.DataModels;

namespace FootballManager.Core.Models.Match
{
    public class TacticsViewModel
    {
        public int PlayerId { get; set; }
        public VirtualTeam CurrentTeam { get; set; }
        public List<Infrastructure.Data.DataModels.Player> StartingEleven { get; set; } = new List<Infrastructure.Data.DataModels.Player>();
        public List<Infrastructure.Data.DataModels.Player> Substitutes { get; set; } = new List<Infrastructure.Data.DataModels.Player>();
        public List<Position> Positions { get; set; } = new List<Position>();

    }
}
