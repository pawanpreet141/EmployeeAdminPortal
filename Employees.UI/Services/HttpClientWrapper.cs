using System.Net;

namespace Employees.UI.Services
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;


        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        //Get
        public async Task<T?> GetAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }


        //Post
        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            return await _httpClient.PostAsJsonAsync(url, data);
        }



        //Put
        public async Task<HttpResponseMessage> PutAsync<T>(string url, T data)
        {
            return await _httpClient.PutAsJsonAsync(url, data);
        }

        //Delete
        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await _httpClient.DeleteAsync(url);
        }



        // signup, login 

        public async Task<ApiResponse?> Signup(
            SignupRequest request)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/Account/signup",
                    request);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content
                    .ReadFromJsonAsync<ApiResponse>();
            }

            var error =
                await response.Content.ReadAsStringAsync();

            return new ApiResponse
            {
                Success = false,
                Message = error
            };
        }

        public async Task<LoginResponse?> Login(
            LoginRequest request)
        {
            var response =
                await _httpClient.PostAsJsonAsync(
                    "api/Account/login",
                    request);

            if (response.IsSuccessStatusCode)
            {
                var result =
                    await response.Content
                        .ReadFromJsonAsync<LoginResponse>();

                if (result != null)
                {
                    result.Success = true;
                }

                return result;
            }

            var error =
                await response.Content.ReadAsStringAsync();

            return new LoginResponse
            {
                Success = false,
                Message = error
            };
        }
    }

    public class SignupRequest
    {
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }

    public class ApiResponse
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }

        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}
