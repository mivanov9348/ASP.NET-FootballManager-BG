namespace FootballManager.Infrastructure.Data.DataModels.Calendar
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Year
    {
        public int Id { get; set; }
        public int YearOrder { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public List<Month> Months { get; set; } = new List<Month>();
        public List<Week> Weeks { get; set; } = new List<Week>();
        public List<Day> Days { get; set; } = new List<Day>();
    }
}
