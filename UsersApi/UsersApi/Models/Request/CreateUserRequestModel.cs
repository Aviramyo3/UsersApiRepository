using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace UsersApi.Models.Request
{
    public class CreateUserRequestModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("job")]
        public string Job { get; set; }
    }
}
