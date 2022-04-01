using Newtonsoft.Json;

namespace ASP.NET_FootballManager.Data.Database.ImportDto
{
    public class LastNamesDto
    {
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("NationId")]
        public string NationId { get; set; }
    }
}
