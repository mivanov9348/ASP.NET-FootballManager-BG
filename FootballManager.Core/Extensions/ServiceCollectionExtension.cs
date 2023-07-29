namespace FootballManager.Core.Extensions
{
    using ASP.NET_FootballManager.Services.Common;
    using ASP.NET_FootballManager.Services.Cup;
    using ASP.NET_FootballManager.Services.EuroCup;
    using ASP.NET_FootballManager.Services.Fixture;
    using ASP.NET_FootballManager.Services.Game;
    using ASP.NET_FootballManager.Services.Inbox;
    using ASP.NET_FootballManager.Services.League;
    using ASP.NET_FootballManager.Services.Manager;
    using ASP.NET_FootballManager.Services.Match;
    using ASP.NET_FootballManager.Services.Player;
    using ASP.NET_FootballManager.Services.Team;
    using ASP.NET_FootballManager.Services.Transfer;
    using ASP.NET_FootballManager.Services.Validation;
    using FootballManager.Core.Services.Chat;
    using FootballManager.Core.Services.PlayerAttribute;
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceCollectionExtension
    {

        public static IServiceCollection AddApplicationServices (this IServiceCollection services)
        {
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IManagerService, ManagerService>();
            services.AddScoped<IValidationService, ValidationService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ILeagueService, LeagueService>();
            services.AddScoped<IPlayerService, PlayerService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IInboxService, InboxService>();
            services.AddScoped<IFixtureService, FixtureService>();
            services.AddScoped<IEuroCupService, EuroCupService>();
            services.AddScoped<IDayService, DayService>();
            services.AddScoped<ICupService, CupService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IPlayerAttributeService, PlayerAttributeService>();
            return services;
        }



    }
}
