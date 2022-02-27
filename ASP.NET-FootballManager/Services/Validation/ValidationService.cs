using ASP.NET_FootballManager.Models;
using System.Text;

namespace ASP.NET_FootballManager.Services.Validation
{
    public class ValidationService : IValidationService
    {
        public ValidationService()
        {

        }

        public (bool isValid, string ErrorMessage) NewManagerValidator(NewManagerViewModel ngvm)
        {
            bool isValid = true;
            var sb = new StringBuilder();

            if (string.IsNullOrEmpty(ngvm.FirstName) || ngvm.FirstName.Length < 3 || ngvm.FirstName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("\r\nFirst Name is not valid!");
            }

            if (string.IsNullOrEmpty(ngvm.LastName) || ngvm.LastName.Length < 3 || ngvm.LastName.Length > 20)
            {
                isValid = false;
                sb.AppendLine("\r\nLast Name is not valid!");
            }

            if (ngvm.BornDate.Year < 1940 || ngvm.BornDate.Year > 2003)
            {
                isValid = false;
                sb.AppendLine("\r\nYear must be between 1940 and 2003!");
            }

            if (ngvm.NationId < 1)
            {
                isValid = false;
                sb.AppendLine("\r\nYou must to choose your nationality!");
            }
            if (ngvm.TeamId < 1)
            {
                isValid = false;
                sb.AppendLine("\r\nYou must to choose who team to manage!");
            }

            return (isValid, sb.ToString().Trim());
        }
    }
}
