namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using FootballManager.Infrastructure.Migrations;
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
        public Year GenerateYear(Game currentGame)
        {
            ResetAllYears();
            currentGame = this.data.Games.FirstOrDefault(x => x.Id == 6);

            var newYear = new Year
            {
                Game = currentGame,
                GameId = currentGame.Id,
                YearOrder = currentGame.Year,
            };

            this.data.Years.Add(newYear);
            this.data.SaveChanges();

            return newYear;
        }

        public void GenerateMonths(Game currentGame, Year currentYear)
        {
            var monthsCount = DataConstants.YearStats.MonthsCount;

            for (int month = 1; month <= monthsCount; month++)
            {
                var newMonth = new Month
                {
                    YearId = currentYear.Id,
                    Year = currentYear,
                    MonthOrder = month,
                    MonthName = ((MontsEnum)month).ToString(),
                    Game = currentGame,
                    GameId = currentGame.Id
                };

                this.data.Months.Add(newMonth);
                this.data.SaveChanges();

                var daysCount = DaysInMonth(month);
                var weeksCount = (int)Math.Ceiling((double)daysCount / DataConstants.YearStats.DaysInWeek);
                var previousMonth = GetPreviousMonth(newMonth);

                GenerateWeeks(newMonth, previousMonth, currentYear, currentGame, weeksCount);
            }
        }

        private Month GetPreviousMonth(Month newMonth)
        {
            if (newMonth.MonthOrder != 1)
            {
                var nextMonthOrder = newMonth.MonthOrder - 1;
                return this.data.Months.FirstOrDefault(x => x.GameId == newMonth.GameId && x.MonthOrder == nextMonthOrder);
            }
            return null;
        }

        public void GenerateWeeks(Month newMonth, Month previousMonth, Year newYear, Game currentGame, int weeksCount)
        {
            var weekOrder = 1;
            var dayOrder = 0;
            var daysLeft = DaysInMonth(newMonth.MonthOrder);

            Week currentWeek = GetCurrentWeek(currentGame, previousMonth);

            for (int weekIndex = 1; weekIndex <= weeksCount; weekIndex++)
            {
                if (currentWeek == null)
                {
                    currentWeek = new Week
                    {
                        YearId = newYear.Id,
                        Year = newYear,
                        WeekOrder = weekOrder,
                        Game = currentGame,
                        GameId = currentGame.Id
                    };
                
                    newMonth.Weeks.Add(currentWeek);
                }

                for (int dayOfWeekIndex = 1; dayOfWeekIndex <= DataConstants.YearStats.DaysInWeek; dayOfWeekIndex++)
                {
                    dayOrder++;
                   
                        GenerateDays(currentGame, dayOrder, dayOfWeekIndex, newYear, newMonth, currentWeek);

                    if (dayOrder == daysLeft)
                    {
                        break;
                    }
                }
                this.data.SaveChanges();

                if (dayOrder > daysLeft)
                {
                    dayOrder = 0;
                    currentWeek = null;
                }

                weekOrder++;
            }

            this.data.SaveChanges();
        }

        private Week GetCurrentWeek(Game currentGame, Month previousMonth)
        {
            if (previousMonth != null)
            {
                var lastWeek = previousMonth.Weeks.Last();
                if (lastWeek.Days.Count() < 7)
                {
                    return lastWeek;
                }
            }            
            return null;
        }

        public void GenerateDays(Game currentGame, int dayOrder, int dayOfWeekIndex, Year currentYear, Month currentMonth, Week currentWeek)
        {
            var newDay = new Day
            {
                DayOrder = dayOrder,
                WeekDayOrder = dayOfWeekIndex,
                DayName = ((DaysEnum)dayOfWeekIndex).ToString(),
                Game = currentGame,
                GameId = currentGame.Id,
                Year = currentYear,
                YearId = currentYear.Id,
                Month = currentMonth,
                MonthId = currentMonth.Id,
                Week = currentWeek,
                WeekId = currentWeek.Id,
            };

            currentWeek.Days.Add(newDay);
            currentMonth.Days.Add(newDay);
            currentYear.Days.Add(newDay);
        }

        private void ResetAllYears()
        {
            this.data.Days.RemoveRange(this.data.Days.ToList());
            this.data.Weeks.RemoveRange(this.data.Weeks.ToList());
            this.data.Months.RemoveRange(this.data.Months.ToList());
            this.data.Years.RemoveRange(this.data.Years.ToList());

        }


        private int DaysInMonth(int month)
        {
            switch (month)
            {
                case 1:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 2:
                    return DataConstants.YearStats.DaysInMonth;
                case 3:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 4:
                    return DataConstants.YearStats.DaysInMonth;
                case 5:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 6:
                    return DataConstants.YearStats.DaysInMonth;
                case 7:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 8:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 9:
                    return DataConstants.YearStats.DaysInMonth;
                case 10:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                case 11:
                    return DataConstants.YearStats.DaysInMonth;
                case 12:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;
                default:
                    return DataConstants.YearStats.ThirtyOneDaysInMonth;

            }
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
