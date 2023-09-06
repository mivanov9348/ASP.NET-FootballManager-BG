namespace FootballManager.Core.Models.Player
{
    using FootballManager.Core.Models.Sorting;
    using FootballManager.Infrastructure.Data.DataModels;
    using System.ComponentModel.DataAnnotations;

    public class PlayerDetailsViewModel
    {

        public string FullName { get; set; }
        public string Nation { get; set; }
        public string City { get; set; }
        public string Position { get; set; }
        public string ImageUrl { get; set; }
        public string Team { get; set; }
        public int Age { get; set; }
        public double Overall { get; set; }
        public int Price { get; set; }
        public int Goals { get; set; }
        public int CleanSheets { get; set; }
        [Range(1, 20)]
        public int Finishing { get; set; }       
        [Range(1, 20)]
        public int Passing { get; set; }  
        [Range(1, 20)]
        public int Heading { get; set; }    
        [Range(1, 20)]
        public int Tackling { get; set; }      
        [Range(1, 20)]
        public int Positioning { get; set; }   
        [Range(1, 20)]
        public int Pace { get; set; }
        [Range(1, 20)]
        public int Stamina { get; set; }
        [Range(1, 20)]
        public int Strength { get; set; }
        [Range(1, 20)]
        public int Dribbling { get; set; }    
        [Range(1, 20)]
        public int BallControll { get; set; }       
        [Range(1, 20)]
        public int OneOnOne { get; set; }        
        [Range(1, 20)]
        public int Reflexes { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Nation> Nations { get; set; } = new List<Nation>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();
        public List<Position> Positions { get; set; } = new List<Position>();
        public List<City> Cities { get; set; } = new List<City>();
        public List<PlayerAttribute> PlayerAttributes { get; set; } = new List<PlayerAttribute>();


    }
}
