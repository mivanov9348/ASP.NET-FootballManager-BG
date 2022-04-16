namespace ASP.NET_FootballManager.Controllers
{   
    using Microsoft.AspNetCore.Mvc;
    public class ChatController : Controller
    {       
        public ChatController()
        {
            
        }

        public async Task<IActionResult> ChatRoom()
        {
            return await Task.Run(() => View());
        }
    }
}
