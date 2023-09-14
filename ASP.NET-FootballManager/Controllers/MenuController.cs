namespace ASP.NET_FootballManager.Controllers
{
    using FootballManager.Core.Models.Menu;
    using Microsoft.AspNetCore.Mvc;
    public class MenuController : Controller
    {
        public IActionResult Index()
        {
            return View(new MenuViewModel());
        }
    }
}
