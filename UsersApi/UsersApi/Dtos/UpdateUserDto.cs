using Newtonsoft.Json;
using UsersApi.Models.Request;

namespace UsersApi.Dtos
{
    public class UpdateUserDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        public UpdateUserRequestModel updateUserRequestModel { get; set; }

        public static UpdateUserDto Create(int  id, UpdateUserRequestModel updateUserRequestModel)
        {
            return new UpdateUserDto
            {
                Id = id,
                updateUserRequestModel = updateUserRequestModel
            };
        }
    }
}
