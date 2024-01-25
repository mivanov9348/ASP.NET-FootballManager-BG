namespace FootballManager.Core.Services.Draw.GroupDraw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Infrastructure.Data.DataModels;

    public class GroupDrawService : IGroupDrawService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
       
        public GroupDrawService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }

       // public Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame)
       // {
       //     var allTeams = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true || x.IsEuroParticipant == true).ToList();
       //     var numOfTeams = model.TeamsPerGroup * model.NumberOfGroups;
       //     var teams = new List<VirtualTeam>();
       //
       //     for (int i = 0; i < numOfTeams; i++)
       //     {
       //         var team = allTeams[rnd.Next(0, allTeams.Count)];
       //         while (helper.IsExist(team, teams))
       //         {
       //             team = allTeams[rnd.Next(0, allTeams.Count)];
       //         }
       //         teams.Add(team);
       //     }
       //
       //     var newDraw = new Draw
       //     {
       //         Teams = teams,
       //         TeamsPergroup = model.TeamsPerGroup,
       //         NumOfGroups = model.NumberOfGroups,
       //         IsDrawStarted = true
       //     };
       //     this.data.Draws.Add(newDraw);
       //
       //     var allDrawLeagues = new List<League>();
       //
       //     for (int i = 1; i <= model.NumberOfGroups; i++)
       //     {
       //         var newLeague = new League
       //         {
       //             Name = $"Group {i}",
       //             DrawId = newDraw.Id
       //         };
       //         allDrawLeagues.Add(newLeague);
       //     }
       //
       //     newDraw.Leagues = allDrawLeagues;
       //
       //     this.data.SaveChanges();
       //     return newDraw;
       // }
       //
       // public (string, string) FillGroupTable(Draw currentDraw, VirtualTeam team)
       // {
       //     var allDrawLeagues = this.data.Leagues.Where(x => x.DrawId == currentDraw.Id).ToList();
       //     var currentLeagueName = "";
       //     foreach (var league in allDrawLeagues)
       //     {
       //         if (league.VirtualTeams.Count < currentDraw.TeamsPergroup)
       //         {
       //             league.VirtualTeams.Add(team);
       //             team.isDrawed = true;
       //             currentLeagueName = league.Name;
       //             break;
       //         }
       //     }
       //     this.data.SaveChanges();
       //     return (team.Name, currentLeagueName);
       // }
    }
}
