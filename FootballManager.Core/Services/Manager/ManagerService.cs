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

        public void AddImageToManager(NewManagerViewModel model, string userId)
        {
            var currentManager = GetCurrentManager(userId);
            currentManager.ImageId = model.ImageId;
            this.data.SaveChanges();
        }

        public Manager CreateNewManager(NewManagerViewModel ngvm, string userId)
        {
            var currentTeam = this.data.Teams.FirstOrDefault(x=>x.Id==ngvm.TeamId);
            var currentUser = this.data.Users.FirstOrDefault(x => x.Id == userId);

            var newManager = new Manager
            {
                FirstName = ngvm.FirstName,
                LastName = ngvm.LastName,
                BornDate = ngvm.BornDate,             
                User = currentUser,
                UserId = currentUser.Id,
                CurrentTeam = currentTeam,
                CurrentTeamId = currentTeam.Id,  
            };

            this.data.Managers.Add(newManager);
            this.data.SaveChanges();
            return newManager;
        }
        public void DeleteCurrentManager(string UserId) => this.data.Managers.Remove(this.data.Managers.FirstOrDefault(x => x.UserId == UserId));
        public Manager GetCurrentManager(string userId) => this.data.Managers.FirstOrDefault(x => x.UserId == userId);
    }
}
