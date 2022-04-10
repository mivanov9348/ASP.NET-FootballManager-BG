namespace FootballManager.Core.Services.Chat
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    public class ChatService : IChatService
    {
        private readonly FootballManagerDbContext data;
        public ChatService(FootballManagerDbContext data)
        {
            this.data = data;
        }

        public Message AddMessage(string message, string userId)
        {
            var currentManager = this.data.Managers.FirstOrDefault(x => x.UserId == userId);
            var currMessage = message;
            var hour = DateTime.UtcNow.ToLocalTime().Hour.ToString();
            var minutes = DateTime.UtcNow.ToLocalTime().Minute.ToString();

            var newMessage = new Message
            {
                Sender = currentManager.FirstName + " " + currentManager.LastName,
                Text = currMessage,
                CreatedOn = $"{hour}:{minutes}"
            };
            this.data.Messages.Add(newMessage);
            this.data.SaveChanges();
            return newMessage;
        }


    }
}
