namespace FootballManager.Core.Models.League
{
    using FootballManager.Infrastructure.Data.DataModels;

    public class LeagueViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int NationalityId { get; set; }

        public Nation Nationality { get; set; }

        public List<Fixture> Fixtures { get; set; }

        public List<VirtualTeam> Teams { get; set; }
    }
}
