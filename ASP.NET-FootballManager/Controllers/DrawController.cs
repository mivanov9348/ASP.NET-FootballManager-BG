namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
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
            var drawViewModel = this.serviceAggregator.drawService.GetDrawViewModel(draw);

            return View("Index", drawViewModel);
        }

        public IActionResult Draw(DrawViewModel model, int drawId)
        {
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);            
            this.serviceAggregator.drawService.DrawTeam(currentDraw);
            var drawViewModel = this.serviceAggregator.drawService.GetDrawViewModel(currentDraw);
            return View("Index", drawViewModel);
        }
    }
}
