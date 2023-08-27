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
        public Draw CreateEliminationDraw(DrawViewModel model)
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
        public Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame)
        {
            DeleteDraws();

            var allTeams = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true || x.IsEuroParticipant == true).ToList();
            var numOfTeams = model.TeamsPerGroup * model.NumberOfGroups;
            var teams = new List<VirtualTeam>();

            for (int i = 0; i < numOfTeams; i++)
            {
                var team = allTeams[rnd.Next(0, allTeams.Count)];
                while (IsExist(team, teams))
                {
                    team = allTeams[rnd.Next(0, allTeams.Count)];
                }
                teams.Add(team);
            }

            var newDraw = new Draw
            {
                Teams = teams,
                TeamsPergroup = model.TeamsPerGroup,
                NumOfGroups = model.NumberOfGroups,
                IsDrawStarted = true
            };
            this.data.Draws.Add(newDraw);

            var allDrawLeagues = new List<League>();

            for (int i = 1; i <= model.NumberOfGroups; i++)
            {
                var newLeague = new League
                {
                    Name = $"Group {i}",
                    DrawId = newDraw.Id
                };
                allDrawLeagues.Add(newLeague);

                var allLeagueVirtualTeams = new List<VirtualTeam>();
                for (int j = 0; j < model.TeamsPerGroup; j++)
                {
                    var newVirtualTeam = new VirtualTeam
                    {
                        Game = currentGame,
                        GameId = currentGame.Id,
                        League = newLeague,
                        LeagueId = newLeague.Id
                    };
                    this.data.VirtualTeams.Add(newVirtualTeam);
                    allLeagueVirtualTeams.Add(newVirtualTeam);
                }
                newLeague.VirtualTeams = allLeagueVirtualTeams;
            }

            newDraw.Leagues = allDrawLeagues;

            this.data.SaveChanges();
            return newDraw;
        }
        public VirtualTeam DrawTeam(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);
            var randomDrawTeam = new VirtualTeam();
            randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];

            return randomDrawTeam;
        }

        public void FillEliminationTable(Draw currentDraw, VirtualTeam team)
        {
            foreach (var fixture in currentDraw.Fixtures)
            {
                if (fixture.HomeTeamId == null)
                {
                    fixture.HomeTeamId = team.Id;
                    fixture.HomeTeamName = team.Name;
                    this.data.SaveChanges();
                    break;  // Break after assigning the home team
                }
                else if (fixture.AwayTeamId == null)
                {
                    fixture.AwayTeamId = team.Id;
                    fixture.AwayTeamName = team.Name;
                    this.data.SaveChanges();
                    break;  // Break after assigning the away team
                }
            }
            team.isDrawed = true;
            this.data.SaveChanges();
        }
        public void FillGroupTable(Draw currentDraw, VirtualTeam team)
        {
            var allDrawLeagues = this.data.Leagues.Where(x => x.DrawId == currentDraw.Id).ToList();
            foreach (var league in allDrawLeagues)
            {
                var leagueVirtualTeams = this.data.VirtualTeams.Where(x => x.LeagueId == league.Id).ToList();

                if (leagueVirtualTeams.Count < currentDraw.TeamsPergroup)
                {
                    var firstEmptyTeam = leagueVirtualTeams.FirstOrDefault(x => x.Team == null);
                    firstEmptyTeam = team;
                    team.isDrawed = true;
                    break;
                }
            }
            this.data.SaveChanges();
        }
        public void AutomaticFill(Draw currentDraw)
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
                        fixture.HomeTeamName = randomDrawTeam.Name;
                        this.data.SaveChanges();
                        break;
                    }
                    else if (fixture.AwayTeamId == null)
                    {
                        fixture.AwayTeamId = randomDrawTeam.Id;
                        fixture.AwayTeamName = randomDrawTeam.Name;
                        this.data.SaveChanges();
                        break;
                    }
                }
                randomDrawTeam.isDrawed = true;
                remainingTeams = this.GetRemainingTeams(currentDraw);
            }
            this.data.SaveChanges();
        }
        public Draw GetDrawById(int id) => this.data.Draws
            .Include(draw => draw.Teams)
            .Include(draw => draw.Fixtures)
            .FirstOrDefault(x => x.Id == id);
        public DrawViewModel GetDrawViewModel(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);

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
        public GroupDrawViewModel GetGroupDrawViewModel(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);
            var allNations = this.data.Nations.ToList();
            var allTeams = this.data.Teams.ToList();

            var newViewModel = new GroupDrawViewModel
            {
                Teams = allTeams,
                IsDrawStarted = currentDraw.IsDrawStarted,
                DrawId = currentDraw.Id,
                RemainingTeams = remainingTeams,
                NumberOfGroups = currentDraw.NumOfGroups,
                TeamsPerGroup = currentDraw.TeamsPergroup,
                Leagues = currentDraw.Leagues,
                Nations = allNations,

            };

            return newViewModel;
        }
        public List<VirtualTeam> GetRemainingTeams(Draw currentDraw)
        {
            var currentDrawTeams = currentDraw.Teams;
            return currentDrawTeams.Where(x => x.isDrawed == false).ToList();
        }
        public void DeleteDraws()
        {
            var allDraws = this.data.Draws.ToList();
            var allFixtures = this.data.Fixtures.Where(x => x.DrawId != null).ToList();
            var allLeagues = this.data.Leagues.Where(x => x.DrawId != null).ToList();

            foreach (var teams in this.data.VirtualTeams)
            {
                teams.isDrawed = false;
            }
            this.data.Fixtures.RemoveRange(allFixtures);
            this.data.Leagues.RemoveRange(allLeagues);

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

