namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;

    public class DrawController : Controller
    {
        private ServiceAggregator serviceAggregator;
        public DrawController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }

        public IActionResult Index()
        {
            return View(new DrawViewModel());
        }

        public IActionResult GenerateDraw(DrawViewModel model)
        {
            var draw = this.serviceAggregator.drawService.CreateDraw(model);

            return View("Index", new DrawViewModel
            {
                Teams = draw.Teams,
                IsDrawStarted = true,
                AllFixtures = draw.Fixtures,
                RemainingTeams = draw.RemainingTeams,
                CurrentDrawId = draw.Id
            });
        }

        public IActionResult Draw(DrawViewModel model, int drawId)
        {
            var team = this.serviceAggregator.drawService.DrawTeam(model);

            return View("Index", new DrawViewModel
            {
                Teams = model.Teams,
                IsDrawStarted = true,
                currentDrawedTeam = team,
                AllFixtures = model.AllFixtures,
                RemainingTeams = model.RemainingTeams
            });
        }
    }
}
