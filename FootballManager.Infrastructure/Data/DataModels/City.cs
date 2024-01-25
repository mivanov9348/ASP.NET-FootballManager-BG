namespace FootballManager.Infrastructure.Data.DataModels
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class City
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        public int NationId { get; set; }
        public Nation Nation { get; set; }
        public List<Team> Teams { get; set; } = new List<Team>();
        public List<Player> Players { get; set; } = new List<Player>();
    }
}