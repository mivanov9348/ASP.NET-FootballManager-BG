namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using System.Runtime.CompilerServices;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;

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
                while (IsExist(team, teams))
                {
                    team = allTeams[rnd.Next(0, allTeams.Count)];
                }
                teams.Add(team);
            }

            for (int i = 0; i < model.NumberOfTeams / 2; i++)
            {
                var newFixture = new Fixture();
                fixtures.Add(newFixture);
            }
            this.data.Fixtures.AddRange(fixtures);

            var newDraw = new Draw
            {
                Teams = teams,

                Fixtures = fixtures,
                IsDrawStarted = true
            };

            this.data.Draws.Add(newDraw);
            this.data.SaveChanges();
            return newDraw;
        }
        public void DrawTeam(Draw currentDraw)
        {

            var remainingTeams = this.GetRemainingTeams(currentDraw);

            if (remainingTeams.Count > 0)
            {
                var randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];

                foreach (var fixture in currentDraw.Fixtures)
                {
                    if (fixture.HomeTeamId == null)
                    {
                        fixture.HomeTeamId = randomDrawTeam.Id;
                        this.data.SaveChanges();
                        break;  // Break after assigning the home team
                    }
                    else if (fixture.AwayTeamId == null)
                    {
                        fixture.AwayTeamId = randomDrawTeam.Id;
                        this.data.SaveChanges();
                        break;  // Break after assigning the away team
                    }
                }
                randomDrawTeam.isDrawed = true;

            }
            else
            {
                currentDraw.IsDrawStarted = false;
            }
            this.data.SaveChanges();
        }

        public void FinalizeDraw(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);

            while (remainingTeams.Count > 0)
            {
                remainingTeams = this.GetRemainingTeams(currentDraw);
                var randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];

                foreach (var fixture in currentDraw.Fixtures)
                {
                    if (fixture.HomeTeamId == null)
                    {
                        fixture.HomeTeamId = randomDrawTeam.Id;
                        this.data.SaveChanges();
                        break;
                    }
                    else if (fixture.AwayTeamId == null)
                    {
                        fixture.AwayTeamId = randomDrawTeam.Id;
                        this.data.SaveChanges();
                        break;
                    }
                }
                randomDrawTeam.isDrawed = true;
                remainingTeams = this.GetRemainingTeams(currentDraw);
            }
            currentDraw.IsDrawStarted = false;
            this.data.SaveChanges();
        }

        public Draw GetDrawById(int id) => this.data.Draws
            .Include(draw => draw.Teams)
            .Include(draw => draw.Fixtures)
            .FirstOrDefault(x => x.Id == id);

        public DrawViewModel GetDrawViewModel(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);

            if (remainingTeams.Count == 0)
            {
                currentDraw.IsDrawStarted = false;
                this.data.SaveChanges();
            }

            var newViewModel = new DrawViewModel
            {
                Teams = currentDraw.Teams,
                IsDrawStarted = currentDraw.IsDrawStarted,
                AllFixtures = currentDraw.Fixtures,
                CurrentDrawId = currentDraw.Id,
                RemainingTeams = remainingTeams
            };

            return newViewModel;
        }

        public List<VirtualTeam> GetRemainingTeams(Draw currentDraw)
        {
            var currentDrawTeams = currentDraw.Teams;
            return currentDrawTeams.Where(x => x.isDrawed == false).ToList();
        }

        private void DeleteDraws()
        {
            var allDraws = this.data.Draws.ToList();
            var allFixtures = this.data.Fixtures.Where(x => x.DrawId != null).ToList();
            foreach (var teams in this.data.VirtualTeams)
            {
                teams.isDrawed = false;
            }
            this.data.Fixtures.RemoveRange(allFixtures);
            this.data.Draws.RemoveRange(allDraws);
            this.data.SaveChanges();
        }

        private bool IsExist(VirtualTeam team, List<VirtualTeam> teams)
        {
            if (teams.Contains(team))
            {
                return true;
            }

            return false;
        }

    }
}

