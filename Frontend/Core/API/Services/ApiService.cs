using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;

namespace Core.API.Services
{
    public class ApiService
    {
        private const string AUTH_TOKEN_KEY = "auth_token";

        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiService> _logger;

        public string Debug { get; set; }
        public ApiService(HttpClient httpClient, ILogger<ApiService> logger, IConfiguration config)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ApiResponse<T>> GetAsync<T>(string path) where T : class
        {
            await ConfigBearerToken();
            var response = await _httpClient.GetAsync(path);

            ApiResponse<T> apiResponse = new();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var data = await response.Content.ReadFromJsonAsync<T>();
                apiResponse.Data = data;
                apiResponse.Error = null;
            }
            else
            {
                apiResponse.Error = await response.Content.ReadFromJsonAsync<ApiError>();
            }

            return apiResponse;
        }

        
        public async Task<ApiError> PostAsync(string path)
        {
            await ConfigBearerToken();

            var response = await _httpClient.PostAsync(path,null);

            if (response.StatusCode is System.Net.HttpStatusCode.OK)
                return null;

            return await response.Content.ReadFromJsonAsync<ApiError>();

        }
        public async Task<ApiError> PostAsync<T>(string path, T data)
        {
            await ConfigBearerToken();

            var response = await _httpClient.PostAsJsonAsync(path, data, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ApiError>();
        }

        public async Task<ApiResponse<TOutput>> PostWithResultAsync<TInput, TOutput>(string path, TInput data) where TInput: class where TOutput : class
        {
            await ConfigBearerToken();

            var response = await _httpClient.PostAsJsonAsync(path, data, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            ApiResponse<TOutput> apiResponse = new();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var responseData = await response.Content.ReadFromJsonAsync<TOutput>();
                apiResponse.Data = responseData;
                apiResponse.Error = null;
            }
            else
            {
                try
                {
                    apiResponse.Error = await response.Content.ReadFromJsonAsync<ApiError>();
                }
                catch(Exception ex)
                {
                    apiResponse.Error = new() { Message = ex.StackTrace };
                }
            }

            return apiResponse;
        }

        public async Task<ApiError> PutAsync<T>(string path, T data)
        {
            await ConfigBearerToken();

            var response = await _httpClient.PutAsJsonAsync(path, data, options: new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
            });

            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ApiError>();

        }

        public async Task<ApiError> DeleteAsync(string path)
        {
            await ConfigBearerToken();

            var response = await _httpClient.DeleteAsync(path);

            if (response.StatusCode is System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            return await response.Content.ReadFromJsonAsync<ApiError>();
        }

        private async Task ConfigBearerToken()
        {
            string authToken = await SecureStorage.GetAsync(AUTH_TOKEN_KEY);
            if (!string.IsNullOrEmpty(authToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", authToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }
    }
}
