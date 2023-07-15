using Newtonsoft.Json;

namespace UsersApi.Models.Response
{
    public class GetUserResponseModel
    {
        [JsonProperty("data")]
        public UserResponseModel User { get; set; }
    }
}
