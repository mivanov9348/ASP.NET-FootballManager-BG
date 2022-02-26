using ASP.NET_FootballManager.Models;
using System.Text;

namespace ASP.NET_FootballManager.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public ValidationService()
        {

        }

        public (bool isValid, string ErrorMessage) NewManagerValidator(NewGameViewModel ngvm)
        {
            bool isValid = false;
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(ngvm.FirstName) || ngvm.FirstName.Length < 3 || ngvm.FirstName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("First Name is not valid!");
            }

            if (string.IsNullOrEmpty(ngvm.LastName) || ngvm.LastName.Length < 3 || ngvm.LastName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("Last Name is not valid!");
            }

            if (ngvm.BornDate.Year < 1985 || ngvm.BornDate.Year > 2005)
            {
                isValid = false;
                sb.AppendLine("Year must be between 1985 and 2005!");
            }

            if (ngvm.NationId < 1)
            {
                isValid = false;
                sb.AppendLine("You must to choose your nationality!");
            }
            if (ngvm.TeamId < 1)
            {
                isValid = false;
                sb.AppendLine("You must to choose who team to manage!");
            }

            return (isValid, sb.ToString().Trim());
        }
    }
}
