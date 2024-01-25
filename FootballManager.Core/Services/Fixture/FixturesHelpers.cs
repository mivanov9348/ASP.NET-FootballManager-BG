namespace FootballManager.Core.Services.Fixture
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System;
    public class FixturesHelpers
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        private readonly Random rnd;
        public FixturesHelpers(FootballManagerDbContext data)
        {
            this.constants = new DataConstants();
            this.data = data;
            this.rnd = new Random();
        }

        public Day GetLeagueDay(List<Day> currentLeagueDays)
        {
            return null;
        }



    }
}
