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

    public class ModelService : IModelService
    {
        private readonly FootballManagerDbContext data;
        private readonly ICalendarService calendarService;
        private readonly IDrawService drawService;
        private readonly IMatchService matchService;

        public ModelService(FootballManagerDbContext data, ICalendarService calendarService, IDrawService drawService, IMatchService matchService)
        {
            this.data = data;
            this.calendarService = calendarService;
            this.drawService = drawService;
            this.matchService = matchService;
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
            var remainingTeams = drawService.GetRemainingTeams(currentDraw);

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
            var remainingTeams = drawService.GetRemainingTeams(currentDraw);
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
        public MatchViewModel GetMatchModel(Match match, Fixture fixture, Player player)
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
            var currentDay = this.data.Days.FirstOrDefault(x => x.DayOrder == currentGame.CurrentDayOrder);
            var isGameDay = currentDay.IsLeagueDay || currentDay.IsCupDay;

            return new MenuViewModel
            {
                CurrentDay = currentGame.CurrentDayOrder,
                CurrentMonth = currentGame.CurrentMonthOrder,
                CurrentYear = currentGame.CurrentYearOrder,
                IsDrawDay = currentDay.IsDrawDay,
                IsGameDay = isGameDay
            };
        }
        public TeamViewModel GetTeamViewModel(List<Player> currPlayers, VirtualTeam currentTeam)
        {
            return new TeamViewModel
            {
                Positions = this.data.Positions.ToList(),
                Cities = this.data.Cities.ToList(),
                Players = currPlayers,
                CurrentTeam = currentTeam,
                Nations = this.data.Nations.ToList()
            };
        }
    }
}
