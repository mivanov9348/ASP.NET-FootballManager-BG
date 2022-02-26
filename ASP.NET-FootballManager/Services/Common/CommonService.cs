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

        public List<Nation> GetAllNations() => this.data.Nations.ToList();
        public List<Team> GetAllTeams() => this.data.Teams.ToList();
        public Manager GetCurrentManager(string userId) => this.data.Managers.FirstOrDefault(x => x.UserId == userId);
    }
}
