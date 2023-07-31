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
    }
}
