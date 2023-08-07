namespace FootballManager.Infrastructure.Data.DataModels
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;

    public class GameOption
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public int TimeInterval { get; set; }
        public int StartingCoins { get; set; }
        public int WinCoins { get; set; }
        public int DrawCoins { get; set; }
        public int FirstPlaceCoins { get; set; }
        public int SecondPlaceCoins { get; set; }
        public int ThirdPlaceCoins { get; set; }
        public int PlayerMinimumAge { get; set; }
        public int PlayerMaximumAge { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();

    }
}
