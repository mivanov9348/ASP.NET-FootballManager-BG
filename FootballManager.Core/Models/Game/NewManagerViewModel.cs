namespace ASP.NET_FootballManager.Models
{
    using System.ComponentModel.DataAnnotations;
    using FootballManager.Infrastructure.Data.DataModels;

    public class NewManagerViewModel
    {
        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string FirstName { get; set; }

        [MinLength(3)]
        [MaxLength(20)]
        [Required]
        public string LastName { get; set; }

        [Range(typeof(DateTime), "1/1/1985", "1/1/2005")]
        public DateTime BornDate { get; set; }

        [Required]
        public int NationId { get; set; }

        [Required]
        public int TeamId { get; set; }
        public int ImageId { get; set; }

        public ICollection<Nation> Nations { get; set; } = new HashSet<Nation>();
        public List<Team> Teams { get; set; } = new List<Team>();
    }
}
