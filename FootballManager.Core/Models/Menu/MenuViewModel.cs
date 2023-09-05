namespace FootballManager.Core.Models.Menu
{
    public class MenuViewModel
    {
        public int CurrentYear { get; set; }
        public int CurrentMonth { get; set; }
        public int CurrentDay { get; set; }
        public bool IsDrawDay { get; set; }
        public bool IsGameDay { get; set; }
    }
}
