namespace FootballManager.Core.Services.PlayerProbability
{
    using FootballManager.Infrastructure.Data.DataModels;
    using System;

    public class PlayerProbability : IPlayerProbability
    {
        public PlayerProbability()
        {
        }


        public string CompareProbabilities(PlayerAttribute attributes)
        {
            var output = "";

            var dribblingProbability = (attributes.Dribbling + attributes.BallControll + attributes.Pace + attributes.Stamina) / 4;
            var headingProbability = (attributes.Strength + attributes.Heading) / 2;
            var passingProbability = (attributes.Positioning + attributes.BallControll + attributes.Passing) / 3;
            var tacklingProbability = (attributes.Tackling + attributes.Strength + attributes.Positioning) / 3;
            var shootingProbability = (attributes.BallControll + attributes.Finishing) / 2;

            double maxProbability = Math.Max(shootingProbability, Math.Max(dribblingProbability, Math.Max(headingProbability, Math.Max(passingProbability, tacklingProbability))));

            if (maxProbability == shootingProbability)
            {
                output = "Shooting";
            }
            else if (maxProbability == dribblingProbability)
            {
                output = "Dribbling";
            }
            else if (maxProbability == headingProbability)
            {
                output = "Heading";
            }
            else if (maxProbability == passingProbability)
            {
                output = "Passing";
            }
            else if (maxProbability == tacklingProbability)
            {
                output = "Tackling";
            }

            return output;
        }




        //  public double CalculateTacklingProbability(PlayerAttribute attributes)
        //  {
        //      double tacklingEffect = attributes.Tackling * 0.05;       // A higher tackling attribute increases the chances by 5% for each point
        //      double strengthEffect = attributes.Strength * 0.05;       // A higher strength attribute increases the chances by 5% for each point
        //
        //      // The overall probability will be the sum of the effects
        //      double overallProbability = 0.5 + tacklingEffect + strengthEffect;
        //
        //      // Ensure the overall probability is between 0 and 1
        //      overallProbability = Math.Max(0, Math.Min(1, overallProbability));
        //
        //      return overallProbability;
        //  }
    }
}
