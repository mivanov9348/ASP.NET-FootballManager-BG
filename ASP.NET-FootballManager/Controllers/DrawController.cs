namespace ASP.NET_FootballManager.Controllers
{
    using ASP.NET_FootballManager.Data.Constant;
    using FootballManager.Core.Models.Draw;
    using FootballManager.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Security.Claims;
    public class DrawController : Controller
    {
        private ServiceAggregator serviceAggregator;
        private string userId;
        public DrawController(ServiceAggregator serviceAggregator)
        {
            this.serviceAggregator = serviceAggregator;
        }
        public IActionResult Index()
        {
            CurrentUser();
            var currentGame = serviceAggregator.gameService.GetCurrentGame(userId);
            (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) = serviceAggregator.drawService.GetCurrentDrawDay(currentGame);
            var newDrawModel = new DrawViewModel();
            var currentEuropeanCups = serviceAggregator.euroCupService.GetYearEuropeanCups(currentGame);

            if (isChampionsCupDraw)
            {
                ProcessDraw(currentEuropeanCups.FirstOrDefault(x => x.Rank == 1), DataConstants.ChampionsCup.Participants);
                newDrawModel.IsChampionsCupDraw = false;
            }
            else if (isEuropeanCupDraw)
            {
                ProcessDraw(currentEuropeanCups.FirstOrDefault(x => x.Rank == 2), DataConstants.EuroCup.Participants);
                newDrawModel.IsEuropeanCupDraw = false;
            }
            else if (isCupDraw)
            {
                var cup = serviceAggregator.cupService.GetCurrentCup(currentGame);
                ProcessDraw(cup, DataConstants.BulgarianCup.Participants);
                newDrawModel.IsCupDraw = false;
            }

            void ProcessDraw(Object cup, int numberOfTeams)
            {
                var currentCup = serviceAggregator.euroCupService.GetEuropeanCupByObject(cup);
                newDrawModel.NumberOfTeams = numberOfTeams;
                var draw = serviceAggregator.drawService.CreateContinentalCupEliminationDraw(currentGame, newDrawModel, currentCup);
                newDrawModel = serviceAggregator.modelService.GetDrawViewModel(draw);
            }

            return View("EliminationDraw", newDrawModel);



         //  CurrentUser();
         //  var currentGame = serviceAggregator.gameService.GetCurrentGame(userId);
         //  (bool isChampionsCupDraw, bool isEuropeanCupDraw, bool isCupDraw) = serviceAggregator.drawService.GetCurrentDrawDay(currentGame);
         //  var currentEuropeanCups = serviceAggregator.euroCupService.GetYearEuropeanCups(currentGame);
         //  var newDrawModel = new DrawViewModel();
         //
         //  if (isChampionsCupDraw)
         //  {
         //      var championsCup = currentEuropeanCups.FirstOrDefault(x => x.Rank == 1);
         //      newDrawModel.IsChampionsCupDraw = isChampionsCupDraw;
         //      newDrawModel.NumberOfTeams = DataConstants.ChampionsCup.Participants;
         //      var draw = serviceAggregator.drawService.CreateContinentalCupEliminationDraw(currentGame, newDrawModel, championsCup);
         //      var drawModel = serviceAggregator.modelService.GetDrawViewModel(draw);
         //      return View("EliminationDraw", drawModel);
         //  }
         //  if (isEuropeanCupDraw)
         //  {
         //      var EuroCup = currentEuropeanCups.FirstOrDefault(x => x.Rank == 2);
         //      newDrawModel.IsEuropeanCupDraw = isEuropeanCupDraw;
         //      newDrawModel.NumberOfTeams = DataConstants.EuroCup.Participants;
         //      var draw = serviceAggregator.drawService.CreateContinentalCupEliminationDraw(currentGame, newDrawModel, EuroCup);
         //      var drawModel = serviceAggregator.modelService.GetDrawViewModel(draw);
         //      drawModel.IsEuropeanCupDraw = false;
         //      return View("EliminationDraw", drawModel);
         //  }
         //  if (isCupDraw)
         //  {
         //      var cup = serviceAggregator.cupService.GetCurrentCup(currentGame);
         //      newDrawModel.IsCupDraw = isCupDraw;
         //      newDrawModel.NumberOfTeams = DataConstants.BulgarianCup.Participants;
         //      var draw = serviceAggregator.drawService.CreateDomesticCupEliminationDraw(currentGame, newDrawModel, cup);
         //      var drawModel = serviceAggregator.modelService.GetDrawViewModel(draw);
         //      drawModel.IsCupDraw = false;
         //      return View("EliminationDraw", drawModel);
         //  }
         //
         //  return RedirectToAction("Index", "Inbox");
        }

        public IActionResult EliminationDraw(DrawViewModel model, int drawId)
        {
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);
            var remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);

            if (remainingTeams.Count > 0)
            {
                var drawedTeam = this.serviceAggregator.drawService.DrawTeam(currentDraw);
                this.serviceAggregator.drawService.FillEliminationFixtures(currentDraw, drawedTeam);
                remainingTeams = this.serviceAggregator.drawService.GetRemainingTeams(currentDraw);
            }

            if (remainingTeams.Count == 0)
            {
                currentDraw.IsDrawStarted = false;
            }

            var drawViewModel = this.serviceAggregator.modelService.GetDrawViewModel(currentDraw);
            return View("EliminationDraw", drawViewModel);
        }
        public IActionResult SaveDraw(int drawId)
        {
            CurrentUser();
            var currentDraw = this.serviceAggregator.drawService.GetDrawById(drawId);
            var currentGame = this.serviceAggregator.gameService.GetCurrentGame(userId);
            var currentDates = this.serviceAggregator.calendarService.GetCurrentDate(currentGame);

            serviceAggregator.drawService.SaveDraw(currentDraw);

            return RedirectToAction("Index", "Inbox");
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
            var currentModel = this.serviceAggregator.modelService.GetGroupDrawViewModel(currentDraw);

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

            var groupDrawViewModel = this.serviceAggregator.modelService.GetGroupDrawViewModel(currentDraw);
            groupDrawViewModel.DrawedTeamName = drawedTeamName;
            groupDrawViewModel.DrawedGroupName = drawedleagueName;
            return View("GroupDraw", groupDrawViewModel);
        }

        private void CurrentUser()
        {
            if (this.User.Identity.IsAuthenticated != false)
            {
                userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            }
        }
    }
}
