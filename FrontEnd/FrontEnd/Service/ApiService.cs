using FrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Diagnostics;
using System.Net.Http.Json;
using FrontEnd.Models.ViewModels;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Specialized;

namespace FrontEnd.Service
{
    public class ApiService
    {
        private UserAccountSession _userAccountSession;
        private readonly HttpClient _httpClient;
        private readonly SQLiteAsyncConnection _database;
        private const string USER_SESSION_TOKEN = "user_account_session";
        public ApiService()
        {
            //var dbPath = Path.Combine(FileSystem.AppDataDirectory, "user.db");
            //_database = new SQLiteAsyncConnection(dbPath);
            _httpClient = new HttpClient() { BaseAddress = new Uri("http://localhost:5000") };
        }
        public async Task<bool> Register(RegisterPageViewModel registerPageViewModel)
        {
            var registerData = new { Email = registerPageViewModel.Email, Name = registerPageViewModel.Name, Password = registerPageViewModel.Password };
            var json = System.Text.Json.JsonSerializer.Serialize(registerData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            //return await _database.InsertAsync(content);
            
            var response = await _httpClient.PostAsync("api/user/register", content);
            if (!response.IsSuccessStatusCode || response is null) { return false; }
            else
            {
                return true;
            }
        }
        public async Task<UserAccountSession?> LoginAsync(string email, string password)
        {
            var loginData = new { Email = email, Password = password };
            var json = System.Text.Json.JsonSerializer.Serialize(loginData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/user/login", content);
            var result = await response.Content.ReadAsStringAsync();
            UserAccountSession? deseralizedResult = System.Text.Json.JsonSerializer.Deserialize<UserAccountSession>(result);
            return deseralizedResult;

        }
        //public async Task<string> LoginAsync(string email, string password)
        //{
        //    var loginData = new { Email = email, Password = password };
        //    var json = System.Text.Json.JsonSerializer.Serialize(loginData);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");
        //    var response = await _httpClient.PostAsync("api/user/login", content);
        //    var result = await response.Content.ReadAsStringAsync();
        //    UserAccountSession? deseralizedResult = System.Text.Json.JsonSerializer.Deserialize<UserAccountSession>(result);
        //    return deseralizedResult.accessToken;

        //}
        public async Task<UserDetailModel?> GetUserDetailAsync()
        {   

            var token = Preferences.Get("auth_token", string.Empty);
            Debug.WriteLine($"Stored Token: {token}");
            if (string.IsNullOrEmpty(token))
            { 
                return null;
            }
            var bearerFormat = new AuthenticationHeaderValue("Bearer", token);
            _httpClient.DefaultRequestHeaders.Authorization = bearerFormat;
            //_httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_userAccountSession.accessToken}");
            var response = await _httpClient.GetAsync("/api/user/data");
            if (!response.IsSuccessStatusCode) return null;
            var result = await response.Content.ReadAsStringAsync();
            UserDetailModel? deseralizedResult = System.Text.Json.JsonSerializer.Deserialize<UserDetailModel>(result);
            var user = new UserDetailModel
            {
                name = deseralizedResult.name,
                email = deseralizedResult.email,
            };
            return user;

            //var json = await response.Content.ReadAsStringAsync();
            //return System.Text.Json.JsonSerializer.Deserialize<UserDetail>(json);
        }


    }
}
