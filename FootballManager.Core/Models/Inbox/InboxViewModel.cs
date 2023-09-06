namespace FootballManager.Core.Models.Inbox
{
    using FootballManager.Core.Models.Menu;
    using FootballManager.Infrastructure.Data.DataModels;
    public class InboxViewModel
    {
        public List<Inbox> News { get; set; } = new List<Inbox>();
        public Inbox CurrentNews { get; set; }
        public int MessageId { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string MessageTitle { get; set; }
        public string FullMessage { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }
        public MenuViewModel MenuModel { get; set; }

    }
}
