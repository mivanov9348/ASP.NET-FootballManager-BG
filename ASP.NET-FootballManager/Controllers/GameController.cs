using ASP.NET_FootballManager.Models;
using ASP.NET_FootballManager.Services.League;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_FootballManager.Controllers
{
    public class GameController : Controller
    {
        private readonly ILeagueService leagueService;
        public GameController(ILeagueService leagueService)
        {
            this.leagueService = leagueService;
        }

       


    }
}
