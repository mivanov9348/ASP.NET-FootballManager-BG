namespace ASP.NET_FootballManager.Models
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class InboxViewModel
    {
        public List<Inbox> News { get; set; } = new List<Inbox>();
        public Inbox CurrentNews { get; set; }
        public int MessageId { get; set; }
        public string Type { get; set; }
        public string ImageUrl { get; set; }
        public string MessageReview { get; set; }
        public string FullMessage { get; set; }
        public int Year { get; set; }
        public int Day { get; set; }

    }
}
