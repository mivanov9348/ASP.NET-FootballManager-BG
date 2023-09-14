namespace FootballManager.Core.Services.Model
{

    using ASP.NET_FootballManager.Data;
    using FootballManager.Core.Models.Calendar;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Models.Inbox;
    using FootballManager.Core.Models.Match;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Services.Calendar;
    using FootballManager.Core.Services.Draw;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;
    using ASP.NET_FootballManager.Services.Match;
    using FootballManager.Core.Models.Team;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Player;
    using Microsoft.Extensions.Options;
    using static ASP.NET_FootballManager.Data.Constant.DataConstants;
    using FootballManager.Core.Services.Draw.Common;

    public class ModelService : IModelService
    {
        private readonly FootballManagerDbContext data;
        private readonly ICalendarService calendarService;
        private readonly IMatchService matchService;
        private readonly ICommonDrawService commonDrawService;

        public ModelService(FootballManagerDbContext data, ICalendarService calendarService, IMatchService matchService, ICommonDrawService commonDrawService)
        {
            this.data = data;
            this.calendarService = calendarService;
            this.matchService = matchService;
            this.commonDrawService = commonDrawService;
        }
        public CalendarViewModel GetCalendarViewModel(Month CurrentMonth)
        {
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == CurrentMonth.GameId);
            var monthDays = calendarService.GetAllDaysInMonth(CurrentMonth);
            var startOffsetDays = calendarService.GetStartOffsetDays(CurrentMonth);
            var endOffsetDays = calendarService.GetEndOffsetDays(CurrentMonth);
            var menuModel = GetMenuViewModel(currentGame);

            var newCalendarViewModel = new CalendarViewModel
            {
                MonthName = CurrentMonth.MonthName,
                Days = monthDays,
                Year = CurrentMonth.Year.YearOrder,
                MonthId = CurrentMonth.Id,
                StartOffsetDays = startOffsetDays,
                EndOffsetDays = endOffsetDays,
                CurrentDayOrder = currentGame.CurrentDayOrder,
                MenuViewModel = menuModel
            };

            return newCalendarViewModel;
        }
        public DrawViewModel GetDrawViewModel(Draw currentDraw)
        {
            var remainingTeams = commonDrawService.GetRemainingTeams(currentDraw);

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
            var remainingTeams = commonDrawService.GetRemainingTeams(currentDraw);
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
        public InboxViewModel GetInboxViewModel(Inbox currentMessage, int gameId)
        {
            var currentInboxMessages = this.data.Inboxes.Where(x => x.GameId == gameId).ToList();
            var lastMessage = currentInboxMessages.OrderByDescending(x => x.Id).First();
            var menuModel = GetMenuViewModel(this.data.Games.FirstOrDefault(x => x.Id == gameId));

            var newInboxViewModel = new InboxViewModel
            {
                News = currentInboxMessages.OrderByDescending(x => x.Id).ToList(),
                MessageTitle = lastMessage.MessageTitle,
                ImageUrl = lastMessage.NewsImage,
                FullMessage = lastMessage.FullMessage,
                CurrentNews = currentMessage,
                Year = currentMessage.Year,
                Day = currentMessage.Day,
                MenuModel = menuModel
            };
            return newInboxViewModel;
        }
        public MatchViewModel GetMatchModel(Infrastructure.Data.DataModels.Match match, Fixture fixture, Player player)
        {
            return new MatchViewModel
            {
                CurrentMatch = match,
                HomeTeam = fixture.HomeTeam,
                AwayTeam = fixture.AwayTeam,
                HomeTeamName = fixture.HomeTeamName,
                AwayTeamName = fixture.AwayTeamName,
                Positions = this.data.Positions.ToList(),
                HomeTeamPlayers = matchService.GetStarting11(fixture.HomeTeamId),
                AwayTeamPlayers = matchService.GetStarting11(fixture.AwayTeamId),
                CurrentPlayerName = player.FirstName + " " + player.LastName
            };
        }

        public MenuViewModel GetMenuViewModel(Game currentGame)
        {
            var currentDate = calendarService.GetCurrentDate(currentGame);
            var isGameDay = currentDate.day.IsLeagueDay || currentDate.day.IsCupDay;
            (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) = commonDrawService.GetCurrentDrawDay(currentGame);

            return new MenuViewModel
            {
                CurrentDay = currentGame.CurrentDayOrder,
                CurrentMonth = currentGame.CurrentMonthOrder,
                CurrentYear = currentGame.CurrentYearOrder,
                IsDrawDay = currentDate.day.IsDrawDay,
                IsGameDay = isGameDay,
                IsLeagueDay = currentDate.day.IsLeagueDay,
                IsPlayed = currentDate.day.IsPlayed,
                IsChampionsCupDraw = isChampionsCupDraw,
                IsCupDraw = isCupDraw,
                IsEuropeanCupDraw = isEuropeanCupDraw,
            };
        }

        public PlayerDetailsViewModel GetPlayerDetailsViewModel(Player currentPlayer, Game currentGame)
        {
            var playerAttributes = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == currentPlayer.Id);
            var playerStats = this.data.PlayerStats.FirstOrDefault(x => x.PlayerId == currentPlayer.Id);
            var nations = this.data.Nations.ToList();
            var positions = this.data.Positions.ToList();
            var teams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList();
            var menuModel = GetMenuViewModel(currentGame);

            var playerDetailsViewModel = new PlayerDetailsViewModel
            {
                FullName = currentPlayer.FirstName + " " + currentPlayer.LastName,
                Age = currentPlayer.Age,
                City = currentPlayer.Team.Name,
                Position = positions.FirstOrDefault(x => x.Id == currentPlayer.PositionId).Name,
                ImageUrl = currentPlayer.ProfileImage,
                Goals = playerStats.Goals,
                Overall = currentPlayer.Overall,
                Nation = nations.FirstOrDefault(x => x.Id == currentPlayer.NationId).Name,
                Team = teams.FirstOrDefault(x => x.Id == currentPlayer.TeamId).Name,
                PlayerAttributes = this.data.PlayerAttributes.ToList(),
                OneOnOne = playerAttributes.OneOnOne,
                Reflexes = playerAttributes.Reflexes,
                Finishing = playerAttributes.Finishing,
                Passing = playerAttributes.Passing,
                Heading = playerAttributes.Heading,
                Tackling = playerAttributes.Tackling,
                Stamina = playerAttributes.Stamina,
                Strength = playerAttributes.Strength,
                Dribbling = playerAttributes.Dribbling,
                Positioning = playerAttributes.Positioning,
                BallControll = playerAttributes.BallControll,
                Pace = playerAttributes.Pace,
                MenuViewModel = menuModel
            };
            return playerDetailsViewModel;
        }

        public TeamViewModel GetTeamViewModel(VirtualTeam currentTeam)
        {
            var originalTeam = this.data.Teams.FirstOrDefault(x => x.Id == currentTeam.TeamId);
            var currentGame = this.data.Games.FirstOrDefault(x => x.Id == currentTeam.GameId);
            var menuModel = GetMenuViewModel(currentGame);

            return new TeamViewModel
            {
                Team = originalTeam,
                CurrentTeam = currentTeam,
                Players = this.data.Players.Where(x => x.TeamId == currentTeam.Id).ToList(),
                Nations = this.data.Nations.ToList(),
                Teams = this.data.VirtualTeams.Where(x => x.GameId == currentGame.Id).ToList(),
                Cities = this.data.Cities.ToList(),
                Positions = this.data.Positions.ToList(),
                Leagues = this.data.Leagues.ToList(),
                PlayerAttributes = this.data.PlayerAttributes.ToList(),
                PlayerStats = this.data.PlayerStats.ToList(),
                MenuViewModel = menuModel

            };
        }


    }
}
