namespace FootballManager.Core.Extensions
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
    using FootballManager.Core.Services;
    using FootballManager.Core.Services.Attribute;
    using FootballManager.Core.Services.Calendar;
    using FootballManager.Core.Services.Draw;
    using FootballManager.Core.Services.GameOption;
    using FootballManager.Core.Services.Model;
    using FootballManager.Core.Services.Player.PlayerData;
    using FootballManager.Core.Services.Player.PlayerGenerator;
    using FootballManager.Core.Services.Player.PlayerModel;
    using FootballManager.Core.Services.Player.PlayerSorter;
    using FootballManager.Core.Services.Player.PlayerStats;
    using FootballManager.Core.Services.PlayerProbability;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {         
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IInboxService, InboxService>();
            services.AddScoped<IFixtureService, FixtureService>();
            services.AddScoped<IEuroCupService, EuroCupService>();        
            services.AddScoped<ICupService, CupService>();
            services.AddScoped<IPlayerAttributeService, PlayerAttributeService>();        
            services.AddScoped<IPlayerProbability, PlayerProbability>();
            services.AddScoped<IGameOptionService, GameOptionService>();           
            services.AddScoped<IPlayerDataService,PlayerDataService>();
            services.AddScoped<IPlayerGeneratorService, PlayerGeneratorService>();
            services.AddScoped<IPlayerModelService, PlayerModelService>();
            services.AddScoped<IPlayerSorterService, PlayerSorterService>();
            services.AddScoped<IPlayerStatsService, PlayerStatsService>();
            services.AddScoped<IDrawService,DrawService>();
            services.AddScoped<ICalendarService, CalendarService>();
            services.AddScoped<IModelService, ModelService>();
            services.AddScoped<ServiceAggregator>();        

            return services;
        }



    }
}
