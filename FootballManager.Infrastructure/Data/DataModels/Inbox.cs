namespace ASP.NET_FootballManager.Infrastructure.Data.DataModels
{
    public class Inbox
    {

        public int Id { get; set; }
        public string MessageTitle { get; set; }      

        public string FullMessage { get; set; }

        public string NewsImage { get; set; }

        public int GameId { get; set; }

        public Game Game { get; set; }

        public int Year { get; set; }

        public int Day { get; set; }
    }
}
