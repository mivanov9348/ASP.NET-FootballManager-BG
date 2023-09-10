namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using System.Runtime.CompilerServices;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json.Linq;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using System.Security.Cryptography.X509Certificates;
    using FootballManager.Core.Services.Calendar;
    using ASP.NET_FootballManager.Data.Constant;

    public class DrawService : IDrawService
    {
        private Random rnd;
        private FootballManagerDbContext data;
        private ICalendarService calendarService;
        private readonly DrawHelper helper;

        public DrawService(FootballManagerDbContext data, ICalendarService calendarService)
        {
            this.rnd = new Random();
            this.data = data;
            this.calendarService = calendarService;
            this.helper = new DrawHelper(data);
        }

        //Elimination Draw
        public Draw CreateEliminationDraw(Game currentGame, DrawViewModel model)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList();
            var draw = new Draw();

            if (model.IsChampionsCupDraw)
            {
                allTeams = allTeams.Where(x => x.IsEuroParticipant && x.isDrawed == false).ToList();
                var championsCupTeams = helper.FillTeams(allTeams, model);
                var championsCupFixtures = helper.FillFixtures(allTeams, model);
                draw = helper.CreateDraw(currentGame, championsCupTeams, championsCupFixtures);
            }
            if (model.IsEuropeanCupDraw)
            {
                allTeams = allTeams.Where(x => x.IsEuroParticipant && x.isDrawed == false).ToList();
                var europeanCupTeams = helper.FillTeams(allTeams, model);
                var europeanCupFixtures = helper.FillFixtures(allTeams, model);
                draw = helper.CreateDraw(currentGame, europeanCupTeams, europeanCupFixtures);

            }
            if (model.IsCupDraw)
            {
                allTeams = allTeams.Where(x => x.isDrawed == false && x.IsCupParticipant).ToList();
                var CupTeams = helper.FillTeams(allTeams, model);
                var CupFixtures = helper.FillFixtures(allTeams, model);
                draw = helper.CreateDraw(currentGame, CupTeams, CupFixtures);
            }

            return draw;
        }
        
        public void FillEliminationTable(Draw currentDraw, VirtualTeam team)
        {
            throw new NotImplementedException();
        }

        //Group Draw
        public Draw CreateGroupDraw(GroupDrawViewModel model, Game currentGame)
        {
            DeleteDraws();

            var allTeams = this.data.VirtualTeams.Where(x => x.IsCupParticipant == true || x.IsEuroParticipant == true).ToList();
            var numOfTeams = model.TeamsPerGroup * model.NumberOfGroups;
            var teams = new List<VirtualTeam>();

            for (int i = 0; i < numOfTeams; i++)
            {
                var team = allTeams[rnd.Next(0, allTeams.Count)];
                while (helper.IsExist(team, teams))
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
            }

            newDraw.Leagues = allDrawLeagues;

            this.data.SaveChanges();
            return newDraw;
        }
      
        public (string, string) FillGroupTable(Draw currentDraw, VirtualTeam team)
        {
            var allDrawLeagues = this.data.Leagues.Where(x => x.DrawId == currentDraw.Id).ToList();
            var currentLeagueName = "";
            foreach (var league in allDrawLeagues)
            {
                if (league.VirtualTeams.Count < currentDraw.TeamsPergroup)
                {
                    league.VirtualTeams.Add(team);
                    team.isDrawed = true;
                    currentLeagueName = league.Name;
                    break;
                }
            }
            this.data.SaveChanges();
            return (team.Name, currentLeagueName);
        }

        //Common
        public VirtualTeam DrawTeam(Draw currentDraw)
        {
            var remainingTeams = this.GetRemainingTeams(currentDraw);
            var randomDrawTeam = new VirtualTeam();
            randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];

            return randomDrawTeam;
        }
        public Draw GetDrawById(int id) => this.data.Draws
      .Include(draw => draw.Teams)
      .Include(draw => draw.Fixtures)
      .FirstOrDefault(x => x.Id == id);
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

        public (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) GetCurrentDrawDay(Game currentGame)
        {
            var europeanTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsEuroParticipant != false).ToList();
            var bulgarianCupTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsCupParticipant != false).ToList();

            var areChampionsCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false);
            var areEuropeanCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false);
            var areCupNotDrawedTeams = bulgarianCupTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false);

            return (areChampionsCupNotDrawedTeams, areEuropeanCupNotDrawedTeams, areCupNotDrawedTeams);
        }
    }
}

