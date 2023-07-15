using Newtonsoft.Json.Linq;
using UsersApi.Models.Request;

namespace UsersApi.Utils
{
    public class FirebaseUtils
    {
        public static async Task<string> GetFirebaseIdToken(LoginModel loginModel)
        {
            var apiKey = "AIzaSyDmHu-ZM7BPMBuY37Y8c5FqHEbsZfO8pJw";
            //var email = "usersapi@authentication.com";
            //var password = "123456";

            using var httpClient = new HttpClient();
            var requestUri = $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={apiKey}";

            var requestBody = new
            {
                email=loginModel.Email,
                password=loginModel.Password,
                returnSecureToken = true
            };

            var response = await httpClient.PostAsJsonAsync(requestUri, requestBody);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                // Extract the ID token from the response
                var idToken = JObject.Parse(responseContent)["idToken"].ToString();
                return idToken;
            }
            else
            {
                // Handle the error response
                Console.WriteLine($"Failed to get Firebase ID token. Response: {responseContent}");
                return null;
            }
        }
    }
}
