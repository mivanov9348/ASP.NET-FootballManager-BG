namespace FootballManager.Core.Services.Attribute
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;

    public class PlayerAttributeHelpersService
    {
        private Random rnd;
        public PlayerAttributeHelpersService()
        {
            this.rnd = new Random();
        }

        public PlayerAttribute AddWeights(PlayerAttribute attributes, string position)
        {
            switch (position)
            {
                case "Goalkeeper":
                    attributes.OneOnOneWeight = (40*20)/100;
                    attributes.OneOnOneWeight = (60*20)/100;                    
                    break;
                case "Defender":
                    attributes.Finishing = (6*20)/100;
                    attributes.Passing = (6 * 20) / 100;
                    attributes.Heading = (16 * 20) / 100;
                    attributes.Tackling = (16 * 20) / 100;
                    attributes.Positioning = (16 * 20) / 100;
                    attributes.Pace = (6 * 20) / 100;
                    attributes.Stamina = (6 * 20) / 100;
                    attributes.Strength = (16 * 20) / 100;
                    attributes.Dribbling = (6 * 20) / 100;
                    attributes.BallControll = (6 * 20) / 100;
                    break;
                case "Midlefielder":
                    attributes.Finishing = (4 * 20) / 100;
                    attributes.Passing = (14 * 20) / 100;
                    attributes.Heading = (4 * 20) / 100;
                    attributes.Tackling = (4 * 20) / 100;
                    attributes.Positioning = (14 * 20) / 100;
                    attributes.Pace = (4 * 20) / 100;
                    attributes.Stamina = (14 * 20) / 100;
                    attributes.Strength = (14 * 20) / 100;
                    attributes.Dribbling = (14 * 20) / 100;
                    attributes.BallControll = (14 * 20) / 100;
                    break;
                case "Forward":
                    attributes.Finishing = (14 * 20) / 100;
                    attributes.Passing = (4 * 20) / 100;
                    attributes.Heading = (14 * 20) / 100;
                    attributes.Tackling = (4 * 20) / 100;
                    attributes.Positioning = (4 * 20) / 100;
                    attributes.Pace = (4 * 20) / 100;
                    attributes.Stamina = (14 * 20) / 100;
                    attributes.Strength = (14 * 20) / 100;
                    attributes.Dribbling = (14 * 20) / 100;
                    attributes.BallControll = (14 * 20) / 100;
                    break;
                default:
                    break;
            }

            return null;
        }
    }
}
