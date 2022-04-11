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
            await Clients.All.SendAsync("DisplayTime", DateTime.Now.Hour.ToString(), DateTime.Now.Minute.ToString());
        }
    }
}
