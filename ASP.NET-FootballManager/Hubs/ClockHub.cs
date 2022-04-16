namespace ASP.NET_FootballManager.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    public class ClockHub : Hub
    {
        public ClockHub()
        {
        }
        public async Task PrintTime()
        {
            var currentTime = DateTime.Now;
            await Clients.All.SendAsync("DisplayTime", currentTime.Hour, currentTime.Minute, currentTime.Second);
        }

    }
}
