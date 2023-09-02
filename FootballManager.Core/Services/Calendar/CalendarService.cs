namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.Constant;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using Microsoft.Net.Http.Headers;
    using System;
    using System.Collections.Generic;

    public class CalendarService : ICalendarService
    {
        private readonly FootballManagerDbContext data;
        private readonly CalendarHelper helper;
        private readonly DataConstants constants;
        public CalendarService(FootballManagerDbContext data)
        {
            this.data = data;
            helper = new CalendarHelper(data);
            this.constants = new DataConstants();
        }

        public Year GenerateYear(Game currentGame)
        {
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

                var daysCount = helper.DaysInMonth(month);
                var weeksInMonth = helper.CalculateWeeksInMonth(daysCount);
                var previousMonth = helper.GetPreviousMonth(newMonth);

                GenerateWeeks(newMonth, previousMonth, currentYear, currentGame, weeksInMonth);
            }
        }
        public void GenerateWeeks(Month newMonth, Month previousMonth, Year newYear, Game currentGame, int weeksInMonth)
        {
            var weeksOrder = 1;

            var dayOrder = 0;
            var daysLeft = helper.DaysInMonth(newMonth.MonthOrder);
            var weekDays = -0;

            for (int weekIndex = 1; weekIndex <= weeksInMonth; weekIndex++)
            {
                Week currentWeek = helper.GetCurrentWeek(currentGame, previousMonth);
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
                IsPlayed = false,
                IsCupDay = false,
                IsLeagueDay = false,
                IsEuroCupDay = false,
                IsMatchDay = false,
                IsDrawDay = false
            };

            currentWeek.Days.Add(newDay);
            currentMonth.Days.Add(newDay);
            currentYear.Days.Add(newDay);
        }
        public async Task SetWeekPlan(Game currentGame, Year currentYear)
        {
            var allWeeks = currentYear.Weeks;
            var leagueWeeksCounter = 0;
            var cupWeeksCounter = 7;
            var euroCupWeeksCounter = 7;

            foreach (var week in allWeeks)
            {
                var allWeekDays = week.Days;

                foreach (var day in allWeekDays)
                {
                    if (day.WeekDayOrder == 7)
                    {
                        if ((leagueWeeksCounter == 0 || week.WeekOrder % 3 == 0) && leagueWeeksCounter < 15)
                        {
                            day.IsLeagueDay = true;
                            day.IsMatchDay = true;
                            leagueWeeksCounter++;
                        }
                    }
                    if (day.WeekDayOrder == 3)
                    {
                        if (cupWeeksCounter == 7 || week.WeekOrder % 11 == 0)
                        {
                            day.IsCupDay = true;
                            day.IsMatchDay = true;
                            cupWeeksCounter++;
                        }
                    }
                    if (day.WeekDayOrder == 4)
                    {
                        if (euroCupWeeksCounter == 7 || week.WeekOrder % 10 == 0)
                        {
                            day.IsEuroCupDay = true;
                            day.IsMatchDay = true;
                            euroCupWeeksCounter++;
                        }
                    }
                    if (day.WeekDayOrder == 5)
                    {
                        day.IsDrawDay = true;
                    }
                }

                this.data.SaveChanges();
            }
        }
        public Task<Day> GetCurrentDay(Game currentGame)
        {
            throw new NotImplementedException();
        }
        public Task<List<Day>> GetAllDays(Game currentGame)
        {
            throw new NotImplementedException();
        }
    }
}
