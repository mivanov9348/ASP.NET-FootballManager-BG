namespace FootballManager.Core.Services.Chat
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using ASP.NET_FootballManager.Models;
    using Microsoft.AspNetCore.Identity;

    public interface IChatService
    {
        Message AddMessage(string message, string userId);

    }
}
