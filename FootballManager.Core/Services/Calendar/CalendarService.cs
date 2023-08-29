namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    public class CalendarService : ICalendarService
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        public CalendarService(FootballManagerDbContext data)
        {
            this.data = data;
            this.constants = new DataConstants();
        }
        public Year CreateYear(Game currentGame)
        {
            var monthsCount = DataConstants.YearStats.MonthsCount;
            var daysCount = DataConstants.YearStats.DaysCount;

            var newYear = new Year
            {
                Game = currentGame,
                GameId = currentGame.Id,
                YearOrder = currentGame.Year,
            };

            this.data.Years.Add(newYear);
            this.data.SaveChanges();

            var allMonths = new List<Month>();
            for (int i = 1; i <= monthsCount; i++)
            {
                var newMonth = new Month
                {
                    YearId = newYear.Id,
                    Year = newYear,
                    MonthOrder = i,
                    MonthName = ((MontsEnum)i).ToString(),
                    Game = currentGame,
                    GameId = currentGame.Id
                };

                this.data.Months.Add(newMonth);
                this.data.SaveChanges();

                for (int day = 1; day <= daysCount; day++)
                {
                    var newDay = new Day
                    {
                        CurrentDay = day,                        
                        Game = currentGame,
                        GameId = currentGame.Id,
                        Year = newYear,
                        YearId = newYear.Id,
                        Month = newMonth,
                        MonthId = newMonth.Id,
                    };

                    newMonth.Days.Add(newDay);
                    newYear.Days.Add(newDay);
                    this.data.SaveChanges();
                }
                allMonths.Add(newMonth);
                this.data.SaveChanges();
            }

            this.data.SaveChanges();
            return newYear;
        }

        public Task<List<Day>> GetAllDays(Game currentGame)
        {
            throw new NotImplementedException();
        }

        public Task<Day> GetCurrentDay(Game currentGame)
        {
            throw new NotImplementedException();
        }

        public Month GetCurrentMonth()
        {
            throw new NotImplementedException();
        }

        public Year GetCurrentYear()
        {
            throw new NotImplementedException();
        }

        private string NameOfDay(int day)
        {
            if (day > 7)
            {
                day = day - 7;
            }

            return ((DaysEnum)day).ToString();
        }
    }
}
