namespace FootballManager.Core.Models.Player
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Sorting;
    using FootballManager.Infrastructure.Data.DataModels;

    public class PlayersViewModel
    {
        public int SortBy { get; set; }
        public PlayerSorting PlayerSorting { get; set; }
        public string FullName { get; set; }
        public string Nation { get; set; }
        public string City { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }       
        public double Overall { get; set; }
        public int Price { get; set; }
        public int Goals { get; set; }
        public int Passes { get; set; }
        public int GoalConceded { get; set; }      
        public int Tacklings { get; set; }      
        public PlayerAttribute currentPlAttr { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<PlayerAttribute> AllPlayerAttributes { get; set; } = new List<PlayerAttribute>();
        public List<PlayerStats> AllPlayerStats { get; set; } = new List<PlayerStats>();



    }
}
