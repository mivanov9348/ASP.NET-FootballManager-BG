namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    public class Manager
    {
        public int Id { get; set; }

        [StringLength(20)]
        [Required]
        public string FirstName { get; set; }

        [StringLength(20)]
        [Required]
        public string LastName { get; set; }
        public DateTime BornDate { get; set; }
        public string ImageUrl { get; set; }

        public int? CurrentTeamId { get; set; }
        public Team CurrentTeam { get; set; }

        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public List<VirtualTeam> VirtualTeams { get; set; } = new List<VirtualTeam>();
        public List<Game> Games { get; set; } = new List<Game>();
      
   

    }

}
