namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using System.Runtime.CompilerServices;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;

    public class DrawService : IDrawService
    {
        private Random rnd;
        private FootballManagerDbContext data;
        public DrawService(FootballManagerDbContext data)
        {
            this.rnd = new Random();
            this.data = data;
        }

        public Draw CreateDraw(DrawViewModel model)
        {
            DeleteDraws();

            var allTeams = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true || x.IsEuroParticipant == true).ToList();
            var teams = new List<VirtualTeam>();
            var fixtures = new List<Fixture>();

            for (int i = 0; i < model.NumberOfTeams; i++)
            {
                var team = allTeams[rnd.Next(0, allTeams.Count)];
                teams.Add(team);
            }

            for (int i = 0; i < model.NumberOfTeams / 2; i++)
            {
                var newFixture = new Fixture();
                fixtures.Add(newFixture);
            }

            var newDraw = new Draw
            {
                Teams = teams,
                Fixtures = fixtures,
                RemainingTeams = teams
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }
        public VirtualTeam DrawTeam(DrawViewModel model)
        {
            var currentDraw = this.data.Draws.FirstOrDefault(x => x.Id == model.CurrentDrawId);

            var allRemainingTeams = currentDraw.RemainingTeams;
            var randomDrawTeam = allRemainingTeams[rnd.Next(0, allRemainingTeams.Count)];
           
            foreach (var fixture in model.AllFixtures)
            {
                if (fixture.HomeTeamId == null)
                {
                    fixture.HomeTeamId = randomDrawTeam.Id;
                    break;
                }
                if (fixture.HomeTeamId == null)
                {
                    fixture.HomeTeamId = randomDrawTeam.Id;
                    break;
                }
            }

            currentDraw.RemainingTeams.Remove(randomDrawTeam);
            this.data.SaveChanges();
            return randomDrawTeam;

        }
        private void DeleteDraws()
        {
            var allDraws = this.data.Draws.ToList();
            var allFixtures = this.data.Fixtures.Where(x => x.DrawId != null).ToList();
            this.data.Fixtures.RemoveRange(allFixtures);
            this.data.Draws.RemoveRange(allDraws);
            this.data.SaveChanges();
        }


    }
}

