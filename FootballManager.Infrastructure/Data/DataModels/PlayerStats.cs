namespace FootballManager.Infrastructure.Data.DataModels
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System.ComponentModel.DataAnnotations;
    public class PlayerStats
    {
        public int Id { get; set; }
        [Required]
        public int PlayerId { get; set; }
        public Player Player { get; set; }
        [MinLength(0)]
        public int Appearance { get; set; }
        [MinLength(0)]
        public int Goals { get; set; }
        [MinLength(0)]
        public int Passes { get; set; }
        [MinLength(0)]
        public int GoalsConceded { get; set; }
        [MinLength(0)]
        public int Tacklings { get; set; }
    }
}
