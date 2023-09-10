namespace FootballManager.Core.Services.Draw
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using Microsoft.EntityFrameworkCore;
    using FootballManager.Core.Services.Calendar;
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
        public Draw CreateContinentalCupEliminationDraw(Game currentGame, DrawViewModel model, ContinentalCup currentCup)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id &&  x.IsEuroParticipant && x.isDrawed == false).ToList();
            var currentDay = calendarService.GetCurrentDate(currentGame);

            var draw = new Draw();

            var currentCupTeams = helper.FillTeams(allTeams, model);
            var currentCupFixtures = helper.FillFixtures(allTeams, model);
            draw = helper.CreateDraw(currentGame, currentCupTeams, currentCupFixtures);

            return draw;
        }
        public Draw CreateNationalCupEliminationDraw(Game currentGame, DrawViewModel model, Cup currentCup)
        {
            var allTeams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id && x.IsCupParticipant && x.isDrawed == false).ToList();
            var currentDay = calendarService.GetCurrentDate(currentGame);

            var draw = new Draw();

            var currentCupTeams = helper.FillTeams(allTeams, model);
            var currentCupFixtures = helper.FillFixtures(allTeams, model);
            draw = helper.CreateDraw(currentGame, currentCupTeams, currentCupFixtures);
            return draw;
        }

        public void FillEliminationFixtures(Draw currentDraw, VirtualTeam team)
        {
            foreach (var fixture in currentDraw.Fixtures)
            {
                if (fixture.HomeTeamId == null)
                {
                    fixture.HomeTeamName = team.Name;
                    fixture.HomeTeam = team;
                    fixture.HomeTeamId = team.Id;
                    this.data.SaveChanges();
                    break;
                }
                else if (fixture.AwayTeamId == null)
                {
                    fixture.AwayTeamName = team.Name;
                    fixture.AwayTeam = team;
                    fixture.AwayTeamId = team.Id;
                    this.data.SaveChanges();
                    break;
                }
            }
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
            var randomDrawTeam = remainingTeams[rnd.Next(0, remainingTeams.Count)];
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

            var championsCup = this.data.ContinentalCups.FirstOrDefault(x => x.Rank == 1);
            var europeanCup = this.data.ContinentalCups.FirstOrDefault(x => x.Rank == 2);

            var areChampionsCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false && x.EuropeanCup.Id == championsCup.Id);
            var areEuropeanCupNotDrawedTeams = europeanTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false && x.EuropeanCup.Id == europeanCup.Id);
            var areCupNotDrawedTeams = bulgarianCupTeams.Any(x => x.GameId == currentGame.Id && x.isDrawed == false);

            return (areChampionsCupNotDrawedTeams, areEuropeanCupNotDrawedTeams, areCupNotDrawedTeams);
        }

       
    }
}

