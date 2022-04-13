namespace ASP.NET_FootballManager.Services.Manager
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Models;

    public class ManagerService : IManagerService
    {
        public readonly FootballManagerDbContext data;
        public ManagerService(FootballManagerDbContext data)
        {
            this.data = data;
        }
        public Manager CreateNewManager(NewManagerViewModel ngvm, string userId)
        {
            var currentNation = this.data.Nations.FirstOrDefault(x => x.Id == ngvm.NationId);
            var currentTeam = this.data.Teams.FirstOrDefault(x => x.Id == ngvm.TeamId);
            var currentUser = this.data.Users.FirstOrDefault(x => x.Id == userId);

            var newManager = new Manager
            {
                FirstName = ngvm.FirstName,
                CurrentTeam = currentTeam,
                CurrentTeamId = currentTeam.Id,
                LastName = ngvm.LastName,
                BornDate = ngvm.BornDate,
                Nation = currentNation,
                NationId = currentNation.Id,
                User = currentUser,
                UserId = currentUser.Id
            };

            this.data.Managers.Add(newManager);
            this.data.SaveChanges();
            return newManager;
        }
        public void DeleteCurrentManager(string UserId) => this.data.Managers.Remove(this.data.Managers.FirstOrDefault(x => x.UserId == UserId));
        public Manager GetCurrentManager(string userId) => this.data.Managers.FirstOrDefault(x => x.UserId == userId);
    }
}
