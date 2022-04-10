namespace ASP.NET_FootballManager.Services.Common
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class CommonService : ICommonService
    {
        private readonly FootballManagerDbContext data;
        public CommonService(FootballManagerDbContext data)
        {
            this.data = data;
        }
        public async Task<List<City>> GetAllCities() => await Task.Run(() => this.data.Cities.ToList());
        public async Task<List<Nation>> GetAllNations() => await Task.Run(() => this.data.Nations.ToList());
        public async Task<List<Player>> GetAllPlayers() => await Task.Run(() => this.data.Players.ToList());
        public async Task<List<Position>> GetAllPositions() => await Task.Run(() => this.data.Positions.ToList());

    }
}
