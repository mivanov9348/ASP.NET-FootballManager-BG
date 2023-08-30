namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System;
    
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

            // Creating new Year
            var newYear = new Year
            {
                Game = currentGame,
                GameId = currentGame.Id,
                YearOrder = currentGame.Year,
            };

            this.data.Years.Add(newYear);
            this.data.SaveChanges();

            // Creating list of months
            for (int month = 1; month <= monthsCount; month++)
            {
                var newMonth = new Month
                {
                    YearId = newYear.Id,
                    Year = newYear,
                    MonthOrder = month,
                    MonthName = ((MontsEnum)month).ToString(),
                    Game = currentGame,
                    GameId = currentGame.Id
                };

                this.data.Months.Add(newMonth);
                this.data.SaveChanges();

                var daysCount = DaysInMonth(month);
                CreateWeeks(newMonth, newYear, currentGame, daysCount);
            }

            this.data.SaveChanges();
            return newYear;
        }        

        private void CreateWeeks(Month newMonth, Year newYear, Game currentGame, int daysCount)
        {            
            var weekOrder = 1;
        
            for (int day = 1; day <= daysCount; day++)
            {
                var newWeek = new Week
                {
                    YearId = newYear.Id,
                    Year = newYear,
                    WeekOrder = weekOrder,
                    Game = currentGame,
                    GameId = currentGame.Id
                };
                this.data.Weeks.Add(newWeek);                                             
                for (int weekDayIndex = 1; weekDayIndex <= DataConstants.YearStats.DaysInWeek; weekDayIndex++)
                {
                    var newDay = new Day
                    {
                        CurrentDay = day,
                        DayName = ((DaysEnum)weekDayIndex).ToString(),
                        Game = currentGame,
                        GameId = currentGame.Id,
                        Year = newYear,
                        YearId = newYear.Id,
                        Month = newMonth,
                        MonthId = newMonth.Id,
                        Week = newWeek,
                        WeekId = newWeek.Id,
                    };
                    this.data.Days.Add(newDay);
                    newWeek.Days.Add(newDay);
                    newMonth.Days.Add(newDay);
                    newYear.Days.Add(newDay);
                }
                this.data.SaveChanges();
                weekOrder++;
            }
            this.data.SaveChanges();
        }
        private int DaysInMonth(int month)
        {
            if (month % 2 == 0)
            {
                return DataConstants.YearStats.DaysInMonth;
            }
            return DataConstants.YearStats.ThirtyOneDaysInMonth;
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
