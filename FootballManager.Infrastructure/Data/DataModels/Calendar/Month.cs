namespace FootballManager.Infrastructure.Data.DataModels.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Month
    {
        public int Id { get; set; }
        public string MonthName { get; set; }    
        public int MonthOrder { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public int? YearId { get; set; }
        public Year Year { get; set; }
        public List<Week> Weeks { get; set; } = new List<Week>();
        public List<Day> Days { get; set; } = new List<Day>();

    }
}
