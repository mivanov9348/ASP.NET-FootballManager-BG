namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    using System.ComponentModel.DataAnnotations;
    public class Position
    {

        public int Id { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        [Required]
        [StringLength(2)]
        public string Abbr { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();

    }
}
