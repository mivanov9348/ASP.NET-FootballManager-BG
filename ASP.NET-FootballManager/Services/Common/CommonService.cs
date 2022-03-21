namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    public class CommonService : ICommonService
    {
        private readonly FootballManagerDbContext data;
        public CommonService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public List<City> GetAllCities() => this.data.Cities.ToList();
        public List<Nation> GetAllNations() => this.data.Nations.ToList();
        public List<Player> GetAllPlayers() => this.data.Players.ToList();
        public List<Position> GetAllPositions() => this.data.Positions.ToList();
        public List<Game> GetAllUsersSaves(int managerId) => this.data.Games.Where(x => x.ManagerId == managerId).ToList();
     
    }
}
