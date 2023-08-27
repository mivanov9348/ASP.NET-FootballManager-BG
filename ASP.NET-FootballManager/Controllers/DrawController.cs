namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services;
    using FootballManager.Infrastructure.Data.DataModels;
    using Microsoft.AspNetCore.Mvc;
    using Newtonsoft.Json.Linq;
    using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
    using System.Security.Claims;

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

        [HttpGet]
        public IActionResult EliminationDraw()
        {
            return View(new DrawViewModel());
        }

        public IActionResult GenerateEliminationDraw(DrawViewModel model)
        {
            var draw = this.serviceAggregator.drawService.CreateEliminationDraw(model);
            var drawViewModel = this.serviceAggregator.drawService.GetDrawViewModel(draw);
            return View("EliminationDraw", drawViewModel);
        }
        public IActionResult EliminationDraw(DrawViewModel model, int drawId)
        {
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);
            var remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);

            if (remainingTeams.Count > 0)
            {
                var drawedTeam = this.serviceAggregator.drawService.DrawTeam(currentDraw);
                this.serviceAggregator.drawService.FillEliminationTable(currentDraw, drawedTeam);
                remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);
            }

            if (remainingTeams.Count == 0)
            {
                currentDraw.IsDrawStarted = false;
            }

            var drawViewModel = this.serviceAggregator.drawService.GetDrawViewModel(currentDraw);
            return View("EliminationDraw", drawViewModel);
        }
        public IActionResult FinalizeEliminationDraw(DrawViewModel model, int drawId)
        {
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);
            var remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);

            if (remainingTeams.Any())
            {
                this.serviceAggregator.drawService.AutomaticFill(currentDraw);
            }


            currentDraw.IsDrawStarted = false;


            var drawViewModel = this.serviceAggregator.drawService.GetDrawViewModel(currentDraw);
            return View("EliminationDraw", drawViewModel);
        }
        public IActionResult ResetEliminationDraw(GroupDrawViewModel model)
        {
            this.serviceAggregator.drawService.DeleteDraws();
            return View("EliminationDraw", new DrawViewModel
            {
                IsDrawStarted = false
            });
        }

        //Group Draw

        [HttpGet]
        public IActionResult GroupDraw()
        {
            return View(new GroupDrawViewModel
            {
                IsDrawStarted = false,
            });
        }

        [HttpPost]
        public IActionResult GroupDraw(GroupDrawViewModel model)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var currentGame = this.serviceAggregator.gameService.GetCurrentGame(userId);
            var currentDraw = this.serviceAggregator.drawService.CreateGroupDraw(model, currentGame);
            var remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);
            var currentModel = this.serviceAggregator.drawService.GetGroupDrawViewModel(currentDraw);

            return View(currentModel);
        }
        public IActionResult DrawAGroupTeam(GroupDrawViewModel model, int drawId)
        {
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);
            var remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);
            var drawedleagueName = "";
            var drawedTeamName = "";

            if (remainingTeams.Count > 0)
            {
                var drawedTeam = this.serviceAggregator.drawService.DrawTeam(currentDraw);
                var teamAndLeagueName = this.serviceAggregator.drawService.FillGroupTable(currentDraw, drawedTeam);
                remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);
                drawedTeamName = teamAndLeagueName.Item1;
                drawedleagueName = teamAndLeagueName.Item2;
            }

            if (remainingTeams.Count == 0)
            {
                currentDraw.IsDrawStarted = false;
            }

            var groupDrawViewModel = this.serviceAggregator.drawService.GetGroupDrawViewModel(currentDraw);
            groupDrawViewModel.DrawedTeamName = drawedTeamName;
            groupDrawViewModel.DrawedGroupName = drawedleagueName;
            return View("GroupDraw", groupDrawViewModel);
        }

        public IActionResult ResetGroupDraw(GroupDrawViewModel model)
        {
            this.serviceAggregator.drawService.DeleteDraws();

            return View("GroupDraw", new GroupDrawViewModel
            {
                IsDrawStarted = false
            });
        }
    }
}
