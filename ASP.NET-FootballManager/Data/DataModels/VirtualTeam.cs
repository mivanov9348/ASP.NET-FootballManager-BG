namespace ASP.NET_FootballManager.Data.DataModels
{
    using Microsoft.AspNetCore.Identity;
    public class VirtualTeam
    {

        public int Id { get; set; }
        public int TeamId { get; set; }
        public Team Team { get; set; }
        public int ManagerId { get; set; }
        public Manager Manager { get; set; }
        public int Matches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Loses { get; set; }
        public int GoalScored { get; set; }
        public int GoalAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public int Titles { get; set; }
        public int EuroCups { get; set; }        

    }
}
