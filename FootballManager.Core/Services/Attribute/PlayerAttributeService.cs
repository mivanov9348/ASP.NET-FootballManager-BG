namespace FootballManager.Core.Services.Attribute
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;

    public class PlayerAttributeService : IPlayerAttributeService
    {
        private readonly FootballManagerDbContext data;
        private PlayerAttributeHelpersService helpers;
        private Random rnd;
        public PlayerAttributeService(FootballManagerDbContext data, Random rnd, PlayerAttributeHelpersService helpers)
        {
            this.data = data;
            this.rnd = rnd;
            this.helpers = helpers;
        }

        public PlayerAttribute CalculatePlayerAttributes(Player player)
        {
            var newPlayerAttribute = new PlayerAttribute();

            switch (player.Position.Name)
            {
                case "Goalkeeper":
                    newPlayerAttribute.OneOnOne = rnd.Next(1, 20);
                    newPlayerAttribute.Reflexes = rnd.Next(1, 20);
                    newPlayerAttribute.Finishing = rnd.Next(1, 3);
                    newPlayerAttribute.Passing = rnd.Next(1, 3);
                    newPlayerAttribute.Heading = rnd.Next(1, 3);
                    newPlayerAttribute.Tackling = rnd.Next(1, 3);
                    newPlayerAttribute.Positioning = rnd.Next(1, 3);
                    newPlayerAttribute.Pace = rnd.Next(1, 3);
                    newPlayerAttribute.Stamina = rnd.Next(1, 3);
                    newPlayerAttribute.Strength = rnd.Next(1, 3);
                    newPlayerAttribute.Dribbling = rnd.Next(1, 3);
                    newPlayerAttribute.BallControll = rnd.Next(1, 3);
                    break;
                case "Defender":
                    newPlayerAttribute.OneOnOne = 0;
                    newPlayerAttribute.Reflexes = 0;
                    newPlayerAttribute.Finishing = rnd.Next(1, 20);
                    newPlayerAttribute.Passing = rnd.Next(1, 20);
                    newPlayerAttribute.Heading = rnd.Next(8, 20);
                    newPlayerAttribute.Tackling = rnd.Next(8, 20);
                    newPlayerAttribute.Positioning = rnd.Next(8, 20);
                    newPlayerAttribute.Pace = rnd.Next(1, 20);
                    newPlayerAttribute.Stamina = rnd.Next(1, 20);
                    newPlayerAttribute.Strength = rnd.Next(8, 20);
                    newPlayerAttribute.Dribbling = rnd.Next(1, 20);
                    newPlayerAttribute.BallControll = rnd.Next(1, 20);
                    break;
                case "Midlefielder":
                    newPlayerAttribute.OneOnOne = 0;
                    newPlayerAttribute.Reflexes = 0;
                    newPlayerAttribute.Finishing = rnd.Next(1, 20);
                    newPlayerAttribute.Passing = rnd.Next(8, 20);
                    newPlayerAttribute.Heading = rnd.Next(1, 20);
                    newPlayerAttribute.Tackling = rnd.Next(1, 20);
                    newPlayerAttribute.Positioning = rnd.Next(8, 20);
                    newPlayerAttribute.Pace = rnd.Next(1, 20);
                    newPlayerAttribute.Stamina = rnd.Next(8, 20);
                    newPlayerAttribute.Strength = rnd.Next(8, 20);
                    newPlayerAttribute.Dribbling = rnd.Next(8, 20);
                    newPlayerAttribute.BallControll = rnd.Next(8, 20);
                    break;
                case "Forward":
                    newPlayerAttribute.OneOnOne = 0;
                    newPlayerAttribute.Reflexes = 0;
                    newPlayerAttribute.Finishing = rnd.Next(8, 20);
                    newPlayerAttribute.Passing = rnd.Next(1, 20);
                    newPlayerAttribute.Heading = rnd.Next(8, 20);
                    newPlayerAttribute.Tackling = rnd.Next(1, 20);
                    newPlayerAttribute.Positioning = rnd.Next(1, 20);
                    newPlayerAttribute.Pace = rnd.Next(1, 20);
                    newPlayerAttribute.Stamina = rnd.Next(8, 20);
                    newPlayerAttribute.Strength = rnd.Next(8, 20);
                    newPlayerAttribute.Dribbling = rnd.Next(8, 20);
                    newPlayerAttribute.BallControll = rnd.Next(8, 20);
                    break;
                default:
                    break;
            }

            newPlayerAttribute.PlayerId = player.Id;
            helpers.AddWeights(newPlayerAttribute, player.Position.Name);

            this.data.SaveChanges();
            return newPlayerAttribute;
        }
        public void CalculateOverall(Player player)
        {
            var playerAttributes = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == player.Id);

            var result = (playerAttributes.OneOnOne * playerAttributes.OneOnOneWeight)
                        + (playerAttributes.Reflexes * playerAttributes.ReflexesWeight)
                        + (playerAttributes.Finishing * playerAttributes.FinishingWeight)
                        + (playerAttributes.Passing * playerAttributes.PassingWeight)
                        + (playerAttributes.Heading * playerAttributes.HeadingWeight)
                        + (playerAttributes.Tackling * playerAttributes.TacklingWeight)
                        + (playerAttributes.Positioning * playerAttributes.PositioningWeight)
                        + (playerAttributes.Pace * playerAttributes.PaceWeight)
                        + (playerAttributes.Stamina * playerAttributes.StaminaWeight)
                        + (playerAttributes.Strength * playerAttributes.StrengthWeight)
                        + (playerAttributes.Dribbling * playerAttributes.DribblingWeight)
                        + (playerAttributes.BallControll * playerAttributes.BallControllWeight);
            result = result / 20;

            player.Overall = result;
        }
        public void UpdateAttributes(Game CurrentGame)
        {
            // var allPlayers = this.data.Players.Where(x => x.GameId == CurrentGame.Id).ToList();
            //
            // foreach (var player in allPlayers)
            // {
            //     player.Attack += player.Goals / 2;
            //     player.Defense += player.Passes / 2;
            //     player.Age += 1;
            //
            //     if (player.Position.Name == "Striker" || player.Position.Name == "Midlefielder" && player.Goals <= 5)
            //     {
            //         player.Attack -= 3;
            //     }
            //
            //     if (player.Position.Name == "Defender" || player.Position.Name == "Goalkeeper" && player.Passes <= 5)
            //     {
            //         player.Defense -= 3;
            //     }
            //
            //     if (player.Attack > 100)
            //     {
            //         player.Attack = 100;
            //     }
            //
            //     if (player.Defense > 100)
            //     {
            //         player.Defense = 100;
            //     }
            // }
            // this.data.SaveChanges();
        }
    }
}
