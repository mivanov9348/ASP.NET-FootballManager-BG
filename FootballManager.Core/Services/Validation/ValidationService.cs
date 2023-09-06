namespace ASP.NET_FootballManager.Services.Validation
{
    using ASP.NET_FootballManager.Data;
    using ASP.NET_FootballManager.Models;
    using FootballManager.Infrastructure.Data.DataModels;
    using System.Text;
    public class ValidationService : IValidationService
    {
        private readonly FootballManagerDbContext data;
        private StringBuilder sb;
        public ValidationService(FootballManagerDbContext data)
        {
            sb = new StringBuilder();
            this.data = data;
        }

        public (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm)
        {
            bool isValid = true;            

            if (string.IsNullOrEmpty(ngvm.FirstName) || ngvm.FirstName.Length < 3 || ngvm.FirstName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("First Name is not valid!\n");
            }

            if (string.IsNullOrEmpty(ngvm.LastName) || ngvm.LastName.Length < 3 || ngvm.LastName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("Last Name is not valid!\n");
            }

            if (ngvm.BornDate.Year < 1940 || ngvm.BornDate.Year > 2003)
            {
                isValid = false;
                sb.AppendLine("Year must be between 1940 and 2003!\n");
            }

            if (ngvm.NationId < 1)
            {
                isValid = false;
                sb.AppendLine("You must to choose your nationality!\n");
            }
            if (ngvm.TeamId < 1)
            {
                isValid = false;
                sb.AppendLine("You must to choose who team to manage!\n");
            }

            return (isValid, sb.ToString().Trim());
        }

        public bool BuyValidator(int id, VirtualTeam team)
        {
            var currentPlayer = this.data.Players.FirstOrDefault(x => x.Id == id);

            if (team.Budget < currentPlayer.Price)
            {
                return false;
            }
            return true;
        }

        public bool SellValidator(VirtualTeam currentTeam)
        {
            var teamPlayers = this.data.Players.Where(x => x.TeamId == currentTeam.Id).ToList();

            if (teamPlayers.Count <= 11)
            {
                return false;
            }
            return true;
        }
    }
}
