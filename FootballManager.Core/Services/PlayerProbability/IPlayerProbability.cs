namespace FootballManager.Core.Services.PlayerProbability
{
    using FootballManager.Infrastructure.Data.DataModels;

    public interface IPlayerProbability
    {
                string CompareProbabilities(PlayerAttribute attributes);

    }
}
