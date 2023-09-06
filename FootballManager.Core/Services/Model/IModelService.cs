namespace FootballManager.Core.Services.Model
{
    using FootballManager.Core.Models.Calendar;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Models.Inbox;
    using FootballManager.Core.Models.Match;
    using FootballManager.Core.Models.Menu;
    using FootballManager.Core.Models.Team;
    using FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels.Calendar;

    public interface IModelService
    {

        MenuViewModel GetMenuViewModel(Game currentGame);
        CalendarViewModel GetCalendarViewModel(Month CurrentMonth);
        DrawViewModel GetDrawViewModel(Draw currentDraw);
        GroupDrawViewModel GetGroupDrawViewModel(Draw currentDraw);
        InboxViewModel GetInboxViewModel(Inbox currentMessage, int gameId);
        MatchViewModel GetMatchModel(Match match, Fixture fixture, Player player);
        TeamViewModel GetTeamViewModel(List<Player> currPlayers, VirtualTeam currentTeam);


    }
}
