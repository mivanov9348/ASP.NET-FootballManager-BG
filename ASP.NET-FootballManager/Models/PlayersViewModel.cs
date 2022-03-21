namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class PlayersViewModel
    {
        public int SortBy { get; set; }

        public string FullName { get; set; }
        public string Nation { get; set; }
        public string City { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public int Overall { get; set; }
        public int Price { get; set; }
        public int Goals { get; set; }
        public int CleanSheets { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<City> Cities { get; set; } = new List<City>();


    }
}
