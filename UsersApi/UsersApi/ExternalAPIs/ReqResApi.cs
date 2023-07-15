using AutoMapper;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text.Json;
using UsersApi.Configs;
using UsersApi.Dtos;
using UsersApi.Interfaces;
using UsersApi.Models.Request;
using UsersApi.Models.Response;

namespace UsersApi.ExternalAPIs
{
    public class ReqResApi : IReqResApi
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;


        public ReqResApi(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_appSettings.ReqResApiBaseAddress);
        }

        public async Task<List<UserResponseModel>> GetAllUsersByPage(int page)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"users?page={page}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var usersModel  = JsonConvert.DeserializeObject<GetUsersResponseModel>(content);
            return usersModel.Users ?? new List<UserResponseModel>();
        }

        public async Task<UserResponseModel> GetUserById(int userId)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"users/{userId}");
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var usersModel = JsonConvert.DeserializeObject<GetUserResponseModel>(content);
            return usersModel.User ?? new UserResponseModel();
        }

        public async Task<CreateUserResponseModel> CreateUser(CreateUserRequestModel createUserModel)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"users",createUserModel);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var usersModel = JsonConvert.DeserializeObject<CreateUserResponseModel>(content);
            return usersModel ?? new CreateUserResponseModel();
        }

        public async Task<UpdateUserResponseModel> UpdateUser(UpdateUserDto updateUserDto)
        {
            HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"users/{updateUserDto.Id}",updateUserDto.updateUserRequestModel);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            var usersModel = JsonConvert.DeserializeObject<UpdateUserResponseModel>(content);
            return usersModel ?? new UpdateUserResponseModel();
        }

        public async Task DeleteUserById(int userId)
        {
            HttpResponseMessage response = await _httpClient.DeleteAsync($"users/{userId}");
            response.EnsureSuccessStatusCode();
        }
    }
}
