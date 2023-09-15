using FootballManager.Core.Models.Inbox;

namespace FootballManager.Core.Models.Menu
{
    public class MenuViewModel
    {
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentDay { get; set; }
        public bool IsDrawDay { get; set; }
        public bool IsChampionsCupDraw { get; set; }
        public bool IsEuropeanCupDraw { get; set; }
        public bool IsCupDraw { get; set; }
        public bool IsLeagueDay { get; set; }
        public bool IsGameDay { get; set; }
        public bool IsPlayed { get; set; }
        public InboxViewModel inboxViewModel { get; set; }

    }
}
