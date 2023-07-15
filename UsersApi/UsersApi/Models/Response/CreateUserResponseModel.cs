namespace UsersApi.Models.Response
{
    public class CreateUserResponseModel
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
