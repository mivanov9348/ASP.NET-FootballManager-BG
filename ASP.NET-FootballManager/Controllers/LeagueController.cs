using ASP.NET_FootballManager.Models;
using ASP.NET_FootballManager.Services.League;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_FootballManager.Controllers
{
    public class LeagueController : Controller
    {
        private readonly ILeagueService leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

       


    }
}
