namespace ASP.NET_FootballManager.Hubs
{
    using FootballManager.Core.Services.Chat;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.SignalR;
    public class ChatHub : Hub
    {
        private readonly IChatService chatService;
        private UserManager<IdentityUser> userManager;
        public ChatHub(IChatService chatService, UserManager<IdentityUser> userManager)
        {
            this.chatService = chatService;
            this.userManager = userManager;
        }
        public async Task SendMessage(string message)
        {
            var user = userManager.GetUserId(Context.User);
            var currentMessage = chatService.AddMessage(message, user);
            await Clients.All.SendAsync("ReceiveMessage", currentMessage.CreatedOn, currentMessage.Sender, currentMessage.Text);
        }
    }
}
