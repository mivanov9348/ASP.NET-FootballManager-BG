namespace ASP.NET_FootballManager.Data.DataModels
{
    public class Game
    {

        public int Id { get; set; }

        public int ManagerId { get; set; }

        public Manager Manager { get; set; }

        public int TeamId { get; set; }

        public Team Team { get; set; }

        public int Season { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }

        public List<Inbox> Inboxes { get; set; } = new List<Inbox>();
    }
}
