namespace FootballManager.Core.Services.Fixture
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Data.Constant;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class FixturesHelpers
    {
        private readonly FootballManagerDbContext data;
        private readonly DataConstants constants;
        private readonly Random rnd;
        public FixturesHelpers(FootballManagerDbContext data)
        {
            this.constants = new DataConstants();
            this.data = data;
            this.rnd = new Random();
        }

       



    }
}
