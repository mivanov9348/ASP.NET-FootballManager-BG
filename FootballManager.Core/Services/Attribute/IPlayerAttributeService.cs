namespace FootballManager.Core.Services.Attribute
{
    using FootballManager.Infrastructure.Data.DataModels;

    public interface IPlayerAttributeService
    {
        PlayerAttribute CalculatePlayerAttributes(Player Player);
        void CalculateOverall(Player player);
        void UpdateAttributes(Game CurrentGame);
        PlayerAttribute AddWeights(PlayerAttribute attributes, int positionOrder);
        List<PlayerAttribute> GetAllPlayerAttributes();
    }
}
