namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class Nation
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(4)]
        public string Abbr  { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();       
        public List<City> Cities { get; set; } = new List<City>();
        public List<Manager> Managers { get; set; } = new List<Manager>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<League> Leagues { get; set; } = new List<League>();
        public List<Cup> Cups { get; set; } = new List<Cup>();       
    }
}
