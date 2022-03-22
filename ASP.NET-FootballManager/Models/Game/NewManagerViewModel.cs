using ASP.NET_FootballManager.Data.DataModels;
using System.ComponentModel.DataAnnotations;

namespace ASP.NET_FootballManager.Models
{
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

        public ICollection<Nation> Nations { get; set; } = new HashSet<Nation>();
        public ICollection<Team> Teams { get; set; } = new HashSet<Team>();
    }
}
