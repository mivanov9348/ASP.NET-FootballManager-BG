using Newtonsoft.Json;

namespace ASP.NET_FootballManager.Data.Database.ImportDto
{
    public class FirstNamesDto
    {
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("NationName")]
        public string NationName { get; set; }
       
    }
}
