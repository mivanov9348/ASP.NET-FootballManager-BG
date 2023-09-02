namespace FootballManager.Core.Services.Calendar
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System.Linq;
    using System.Reflection.Metadata.Ecma335;

    public class CalendarHelper
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        public CalendarHelper(FootballManagerDbContext data)
        {
            this.data = data;
            this.constants = new DataConstants();
        }

        //DayHelpers

       
        //WeekHelpers
        public int CalculateWeeksInMonth(int daysCount) => (int)Math.Ceiling((double)daysCount / DataConstants.YearStats.DaysInWeek);

        public Week GetCurrentWeek(Game currentGame, Month previousMonth)
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

        //MonthHelpers      
        public int DaysInMonth(int month)
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
        public Month GetPreviousMonth(Month newMonth)
        {
            if (newMonth.MonthOrder != 1)
            {
                var nextMonthOrder = newMonth.MonthOrder - 1;
                return this.data.Months.FirstOrDefault(x => x.GameId == newMonth.GameId && x.MonthOrder == nextMonthOrder);
            }
            return null;
        }
    }
}
