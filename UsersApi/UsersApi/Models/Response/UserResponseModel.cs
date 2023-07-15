using Newtonsoft.Json;

namespace UsersApi.Models.Response
{
    public class UserResponseModel
    {
        [JsonProperty("id")]
        public int Id { get; set; } = -1;

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }
    }
}
