namespace FootballManager.Core.Services
{
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Match;
    using ASP.NET_FootballManager.Services.Team;
    using ASP.NET_FootballManager.Services.Transfer;
    using ASP.NET_FootballManager.Services.Validation;
    using FootballManager.Core.Services.Attribute;
    using FootballManager.Core.Services.Calendar;
    using FootballManager.Core.Services.Draw.Common;
    using FootballManager.Core.Services.Draw.EliminationDraw;
    using FootballManager.Core.Services.Draw.GroupDraw;
    using FootballManager.Core.Services.GameOption;
    using FootballManager.Core.Services.Model;
    using FootballManager.Core.Services.Player.PlayerData;
    using FootballManager.Core.Services.Player.PlayerGenerator;
    using FootballManager.Core.Services.Player.PlayerSorter;
    using FootballManager.Core.Services.Player.PlayerStats;
    public class ServiceAggregator
    {
        public readonly IValidationService validationService;
        public readonly IManagerService managerService;
        public readonly IGameService gameService;
        public readonly ITeamService teamService;
        public readonly IInboxService inboxService;
        public readonly IFixtureService fixtureService;
        public readonly IEuroCupService euroCupService;
        public readonly ICupService cupService;
        public readonly ILeagueService leagueService;
        public readonly IMatchService matchService;
        public readonly ITransferService transferService;
        public readonly IGameOptionService gameOptionsService;
        public readonly IPlayerDataService playerDataService;
        public readonly IPlayerGeneratorService playerGeneratorService;
        public readonly IPlayerSorterService playerSorterService;
        public readonly IPlayerStatsService playerStatsService;
        public readonly ICalendarService calendarService;
        public readonly IModelService modelService;
        public readonly IPlayerAttributeService attributeService;
        public readonly IEliminationDrawService eliminationDrawService;
        public readonly IGroupDrawService groupDrawService;
        public readonly ICommonDrawService commonDrawService;
        public ServiceAggregator(
            IGameService gameService,
            IManagerService managerService,
            IValidationService validationService,
            ITeamService teamService,
            IInboxService inboxService,
            IFixtureService fixtureService,
            IEuroCupService euroCupService,
            ICupService cupService,
            ILeagueService leagueService,
            IMatchService matchService,
            ITransferService transferService,
            IGameOptionService gameOptionsService,
            IPlayerDataService playerDataService,
            IPlayerGeneratorService playerGeneratorService,
            IPlayerSorterService playerSorterService,
            IPlayerStatsService playerStatsService,
            ICalendarService calendarService,
            IModelService modelService,
            IPlayerAttributeService attributeService,
            IEliminationDrawService eliminationDrawService,
            IGroupDrawService groupDrawService,
            ICommonDrawService commonDrawService
            )
        {
            this.managerService = managerService;
            this.validationService = validationService;
            this.gameService = gameService;
            this.teamService = teamService;
            this.inboxService = inboxService;
            this.fixtureService = fixtureService;
            this.euroCupService = euroCupService;
            this.cupService = cupService;
            this.leagueService = leagueService;
            this.matchService = matchService;
            this.transferService = transferService;
            this.gameOptionsService = gameOptionsService;
            this.playerStatsService = playerStatsService;
            this.playerGeneratorService = playerGeneratorService;
            this.playerSorterService = playerSorterService;
            this.playerStatsService = playerStatsService;
            this.playerSorterService = playerSorterService;
            this.playerDataService = playerDataService;
            this.calendarService = calendarService;
            this.modelService = modelService;
            this.attributeService = attributeService;
            this.groupDrawService = groupDrawService;
            this.eliminationDrawService = eliminationDrawService;
            this.commonDrawService = commonDrawService;
        }
    }
}
