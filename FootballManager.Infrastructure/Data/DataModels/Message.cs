﻿namespace FootballManager.Infrastructure.Data.DataModels
{
    public class Message
    {
        public int Id { get; set; }

        public string Sender { get; set; }

        public string Text { get; set; }

        public string CreatedOn { get; set; }
    }
}
