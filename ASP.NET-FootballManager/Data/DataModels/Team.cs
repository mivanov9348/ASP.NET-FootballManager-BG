using System.ComponentModel.DataAnnotations;

namespace ASP.NET_FootballManager.Data.DataModels
{
    public class Team
    {

        public int Id { get; set; }

        [StringLength(30)]
        [Required]
        public string Name { get; set; }  
                 
        public int? CityId { get; set; }
        public City City { get; set; }
        public int? NationId { get; set; }
        public Nation Nation { get; set; }
        public int? LeagueId { get; set; }
        public League League { get; set; }
        public List<Manager> Managers { get; set; } = new List<Manager>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();


    }
}
