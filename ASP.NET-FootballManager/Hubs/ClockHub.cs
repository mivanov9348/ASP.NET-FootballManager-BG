using Microsoft.AspNetCore.SignalR;

namespace FootballManager.Core.Extensions
{
   
    public class ClockHub : Hub
    {
        public ClockHub()
        {
        }

        public async Task DisplayTime()
        {
            await Clients.All.SendAsync("DisplayTime", DateTime.Now.TimeOfDay);
        }
    }
}
