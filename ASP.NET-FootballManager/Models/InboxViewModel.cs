namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Data.DataModels;
    public class InboxViewModel
    {
        public List<Inbox> News { get; set; } = new List<Inbox>();
        public int MessageId { get; set; }

        public string Type { get; set; }
        public string Message { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }

    }
}
