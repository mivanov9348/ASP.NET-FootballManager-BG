namespace FootballManager.Infrastructure.Data.DataModels
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Draw
    {
        public int Id { get; set; }
        public List<Fixture> Fиxtures { get; set; } = new List<Fixture>();
        public List<VirtualTeam> Teams { get; set; } = new List<VirtualTeam>();

    }
}
