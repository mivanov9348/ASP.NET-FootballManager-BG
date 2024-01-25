namespace FootballManager.Core.Services.Attribute
{
    using ASP.NET_FootballManager.Data;
    using FootballManager.Infrastructure.Data.DataModels;
    using System.Collections.Generic;

    public class PlayerAttributeService : IPlayerAttributeService
    {
        private readonly FootballManagerDbContext data;
        private Random rnd;
        public PlayerAttributeService(FootballManagerDbContext data)
        {
            this.data = data;
            this.rnd = new Random();
        }

        public PlayerAttribute CalculatePlayerAttributes(Player player)
        {
            var newPlayerAttribute = new PlayerAttribute();

            switch (player.Position.Order)
            {
                case 1:
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
                case 2:
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
                case 3:
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
                case 4:
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
            this.data.PlayerAttributes.Add(newPlayerAttribute);

            newPlayerAttribute.PlayerId = player.Id;
            AddWeights(newPlayerAttribute, player.Position.Order);
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

            player.Overall = Math.Round(result, 2, MidpointRounding.ToEven);
        }
        public PlayerAttribute AddWeights(PlayerAttribute attributes, int positionOrder)
        {
            switch (positionOrder)
            {
                case 1:
                    attributes.OneOnOneWeight = (40 * 20) / 100.0;
                    attributes.ReflexesWeight = (60 * 20) / 100.0;
                    break;
                case 2:
                    attributes.FinishingWeight = (6 * 20) / 100.0;
                    attributes.PassingWeight = (6 * 20) / 100.0;
                    attributes.HeadingWeight = (16 * 20) / 100.0;
                    attributes.TacklingWeight = (16 * 20) / 100.0;
                    attributes.PositioningWeight = (16 * 20) / 100.0;
                    attributes.PaceWeight = (6 * 20) / 100.0;
                    attributes.StaminaWeight = (6 * 20) / 100.0;
                    attributes.StrengthWeight = (16 * 20) / 100.0;
                    attributes.DribblingWeight = (6 * 20) / 100.0;
                    attributes.BallControllWeight = (6 * 20) / 100.0;
                    break;
                case 3:
                    attributes.FinishingWeight = (4 * 20) / 100.0;
                    attributes.PassingWeight = (14 * 20) / 100.0;
                    attributes.HeadingWeight = (4 * 20) / 100.0;
                    attributes.TacklingWeight = (4 * 20) / 100.0;
                    attributes.PositioningWeight = (14 * 20) / 100.0;
                    attributes.PaceWeight = (4 * 20) / 100.0;
                    attributes.StaminaWeight = (14 * 20) / 100.0;
                    attributes.StrengthWeight = (14 * 20) / 100.0;
                    attributes.DribblingWeight = (14 * 20) / 100.0;
                    attributes.BallControllWeight = (14 * 20) / 100.0;
                    break;
                case 4:
                    attributes.FinishingWeight = (14 * 20) / 100.0;
                    attributes.PassingWeight = (4 * 20) / 100.0;
                    attributes.HeadingWeight = (14 * 20) / 100.0;
                    attributes.TacklingWeight = (4 * 20) / 100.0;
                    attributes.PositioningWeight = (4 * 20) / 100.0;
                    attributes.PaceWeight = (4 * 20) / 100.0;
                    attributes.StaminaWeight = (14 * 20) / 100.0;
                    attributes.StrengthWeight = (14 * 20) / 100.0;
                    attributes.DribblingWeight = (14 * 20) / 100.0;
                    attributes.BallControllWeight = (14 * 20) / 100.0;
                    break;
                default:
                    break;
            }

            return attributes;
        }
        public void UpdateAttributes(Game CurrentGame)
        {
            var allPlayers = this.data.Players.Where(x => x.GameId == CurrentGame.Id).ToList();
            var maxAttrStat = 20;

            foreach (var player in allPlayers)
            {
                player.Age += 1;

                var playerAttributes = this.data.PlayerAttributes.FirstOrDefault(x => x.PlayerId == player.Id);

                switch (player.Position.Order)
                {
                    case 1:

                    case 2:

                    case 3:

                    case 4:

                    default:
                        break;
                }
            }
            this.data.SaveChanges();
        }

        public List<PlayerAttribute> GetAllPlayerAttributes() => this.data.PlayerAttributes.ToList();
    }
}
