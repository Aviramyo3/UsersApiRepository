using Newtonsoft.Json;

namespace UsersApi.Models.Request
{
    public class UpdateUserRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }
}
