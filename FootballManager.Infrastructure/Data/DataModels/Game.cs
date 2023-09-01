namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    public class Game
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int GameOptionId { get; set; }
        public GameOption GameOption { get; set; }
        public int Season { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public int LeagueRound { get; set; }
        public int EuroCupRound { get; set; }
        public int CupRound { get; set; }
        public List<Inbox> Inboxes { get; set; } = new List<Inbox>();
        public List<EuropeanCup> EuropeanCups { get; set; } = new List<EuropeanCup>();
        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Year> Years { get; set; } = new List<Year>();
        public List<Month> Months { get; set; } = new List<Month>();
        public List<Week> Weeks { get; set; } = new List<Week>();
        public List<Day> Days { get; set; } = new List<Day>();   
        public ICollection<Match> Matches { get; set; } = new HashSet<Match>();
    }
}
