namespace ASP.NET_FootballManager.Hub
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;
    public class GameHub : Hub
    {
        public async Task SendMessageToClient(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
