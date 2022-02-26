namespace ASP.NET_FootballManager.Data.DataModels
{
    public class League
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public int NationId { get; set; }
        public Nation Nation { get; set; }
        public List<Player> Players { get; set; } = new List<Player>();
        public List<Team> Teams { get; set; } = new List<Team>();  

    }
}
