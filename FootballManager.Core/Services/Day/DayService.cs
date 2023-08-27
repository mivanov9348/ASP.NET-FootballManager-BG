namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public class DayService : IDayService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public DayService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }
        public void CalculateDays(Game currentGame)
        {
            var leagueFixtures = this.data.Leagues.First().Rounds;
            var cupFixtures = this.data.Cups.First().Rounds;
            var euroCupFixtures = this.data.EuropeanCups.First().Rounds;

            var year = currentGame.Year;
            var days = leagueFixtures + cupFixtures + euroCupFixtures;

            for (int i = 1; i <= days; i++)
            {
                var newDay = new Day
                {
                    Year = year,
                    CurrentDay = i,
                    GameId = currentGame.Id
                };

                this.data.Days.Add(newDay);
            }
            this.data.SaveChanges();

            var daysUsed = new List<Day>();
            var currentDays = this.data.Days.Where(x => x.GameId == currentGame.Id && x.Year == currentGame.Year).ToList();

            for (int i = 0; i < leagueFixtures; i++)
            {
                var randomDay = rnd.Next(0, currentDays.Count);
                if (!daysUsed.Contains(currentDays[randomDay]))
                {
                    currentDays[randomDay].isLeagueDay = true;
                    daysUsed.Add(currentDays[randomDay]);
                    currentDays.Remove(currentDays[randomDay]);
                    this.data.SaveChanges();
                }
            }

            for (int i = 0; i < cupFixtures; i++)
            {
                var randomDay = rnd.Next(0, currentDays.Count);
                if (!daysUsed.Contains(currentDays[randomDay]))
                {
                    currentDays[randomDay].isCupDay = true;
                    daysUsed.Add(currentDays[randomDay]);
                    currentDays.Remove(currentDays[randomDay]);
                    this.data.SaveChanges();
                }
            }

            for (int i = 0; i < euroCupFixtures; i++)
            {
                var randomDay = rnd.Next(0, currentDays.Count);
                if (!daysUsed.Contains(currentDays[randomDay]))
                {
                    currentDays[randomDay].isEuroCupDay = true;
                    daysUsed.Add(currentDays[randomDay]);
                    currentDays.Remove(currentDays[randomDay]);
                    this.data.SaveChanges();
                }
            }

        }
        public async Task<List<Day>> GetAllDays(Game currentGame) => await Task.Run(()=>this.data.Days.Where(x => x.GameId == currentGame.Id && x.Year == currentGame.Year).ToList());
        public async Task<Day> GetCurrentDay(Game currentGame) => await Task.Run(()=>this.data.Days.FirstOrDefault(x => x.GameId == currentGame.Id && x.CurrentDay == currentGame.Day && x.Year == currentGame.Year));
    }
}