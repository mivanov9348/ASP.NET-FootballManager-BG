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
                var weeksInMonth = (int)Math.Ceiling((double)daysCount / DataConstants.YearStats.DaysInWeek);
                var previousMonth = GetPreviousMonth(newMonth);

                GenerateWeeks(newMonth, previousMonth, currentYear, currentGame, weeksInMonth);
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

        public void GenerateWeeks(Month newMonth, Month previousMonth, Year newYear, Game currentGame, int weeksInMonth)
        {
            var weeksOrder = 1;
            

            var dayOrder = 0;
            var daysLeft = DaysInMonth(newMonth.MonthOrder);
            var weekDays = -0;

            for (int weekIndex = 1; weekIndex <= weeksInMonth; weekIndex++)
            {
                Week currentWeek = GetCurrentWeek(currentGame, previousMonth);
                if (newYear.Weeks.Count > 0)
                {
                    weeksOrder = newYear.Weeks.OrderByDescending(x => x.WeekOrder).FirstOrDefault(x => x.YearId == newYear.Id).WeekOrder + 1;
                }

                if (currentWeek == null)
                {
                    currentWeek = new Week
                    {
                        YearId = newYear.Id,
                        Year = newYear,
                        WeekOrder = weeksOrder,
                        Game = currentGame,
                        GameId = currentGame.Id
                    };
                    newMonth.Weeks.Add(currentWeek);

                    weekDays = 1;
                }

                weekDays = currentWeek.Days.Count() + 1;

                for (int dayOfWeekIndex = weekDays; dayOfWeekIndex <= DataConstants.YearStats.DaysInWeek; dayOfWeekIndex++)
                {
                    dayOrder++;

                    GenerateDays(currentGame, dayOrder, dayOfWeekIndex, newYear, newMonth, currentWeek);

                    if (dayOrder == daysLeft)
                    {
                        break;
                    }
                }
                this.data.SaveChanges();                              
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
                    return DataConstants.YearStats.JanuaryDays;
                case 2:
                    return DataConstants.YearStats.FebruaryDays;
                case 3:
                    return DataConstants.YearStats.MarchDays;
                case 4:
                    return DataConstants.YearStats.AprilDays;
                case 5:
                    return DataConstants.YearStats.MayDays;
                case 6:
                    return DataConstants.YearStats.JuneDays;
                case 7:
                    return DataConstants.YearStats.JulyDays;
                case 8:
                    return DataConstants.YearStats.AugustDays;
                case 9:
                    return DataConstants.YearStats.SeptemberDays;
                case 10:
                    return DataConstants.YearStats.OctoberDays;
                case 11:
                    return DataConstants.YearStats.NovemberDays;
                case 12:
                    return DataConstants.YearStats.DecemberDays;
                default:
                    return 30;

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
