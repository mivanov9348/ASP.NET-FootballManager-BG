﻿namespace FootballManager.Infrastructure.Data.DataModels.Calendar
{
    using FootballManager.Infrastructure.Data.DataModels;
    public  class Week
    {
        public int Id { get; set; }
        public int WeekOrder { get; set; }     
        public int YearId { get; set; }
        public Year Year { get; set; }     
        public int? GameId { get; set; }
        public Game Game { get; set; }
        public List<Month> Months { get; set; } = new List<Month>();
        public List<Day> Days { get; set; } = new List<Day>();
    }
}
