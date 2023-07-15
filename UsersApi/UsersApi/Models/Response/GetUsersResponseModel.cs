using Newtonsoft.Json;
using UsersApi.Dtos;

namespace UsersApi.Models.Response
{
    public class GetUsersResponseModel
    {
        [JsonProperty("data")]
        public List<UserResponseModel> Users { get; set; }
    }
}
