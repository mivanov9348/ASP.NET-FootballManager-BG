namespace ASP.NET_FootballManager.Services.League
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.DataModels;
    using System.Collections.Generic;

    public class LeagueService : ILeagueService
    {
        private readonly FootballManagerDbContext data;
        public LeagueService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public List<VirtualTeam> CurrentGameTeams(Game currentGame) => this.data.VirtualTeams.ToList();
    }
}
