namespace FootballManager.Core.Services.Attribute
{
    using ASP.NET_FootballManager.Infrastructure.Data.DataModels;
    using FootballManager.Infrastructure.Data.DataModels;

    public interface IPlayerAttributeService
    {
        PlayerAttribute CalculatePlayerAttributes(Player Player);
        void CalculateOverall(Player player);
        void UpdateAttributes(Game CurrentGame);
        PlayerAttribute AddWeights(PlayerAttribute attributes, int positionOrder);
    }
}
