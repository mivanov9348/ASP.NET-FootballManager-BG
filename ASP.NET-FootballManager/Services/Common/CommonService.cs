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

        public void DeleteCurrentManager(string UserId) => this.data.Managers.Remove(this.data.Managers.FirstOrDefault(x => x.UserId == UserId));
        public List<Manager> GetAllManagers() => this.data.Managers.ToList();
        public List<Nation> GetAllNations() => this.data.Nations.ToList();
        public List<Team> GetAllTeams() => this.data.Teams.ToList();
        public List<Game> GetAllUsersSaves(int managerId) => this.data.Games.Where(x => x.ManagerId == managerId).ToList();
        public Game GetCurrentGame(int id) => this.data.Games.FirstOrDefault(x => x.Id == id);
        public Manager GetCurrentManager(string userId) => this.data.Managers.FirstOrDefault(x => x.UserId == userId);
        public List<Inbox> GetInboxMessages(int gameId) => this.data.Inboxes.Where(x => x.GameId == gameId).ToList();
        public bool isExistGame(string UserId)
        {
            var currManager = this.data.Managers.FirstOrDefault(x => x.UserId == UserId);
            if (currManager != null)
            {
                return true;
            }
            return false;
        }
    }
}
