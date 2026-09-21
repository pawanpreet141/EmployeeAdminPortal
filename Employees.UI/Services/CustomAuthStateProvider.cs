////9

//using Microsoft.AspNetCore.Components.Authorization;
//using Blazored.LocalStorage;
//using System.Security.Claims;
//using System.Text.Json;
//using System.Net.Http.Headers;
//using Intersoft.Crosslight.Mobile;

//namespace Employees.UI.Services
//{

//    public class CustomAuthStateProvider : AuthenticationStateProvider
//    {
//        private readonly ILocalStorageService _localStorage;
//        private readonly HttpClient _httpClient;
//        private readonly AuthenticationState _anonymous;

//        public CustomAuthStateProvider(ILocalStorageService localStorage, HttpClient httpClient)
//        {
//            _localStorage = localStorage;
//            _httpClient = httpClient;
//            _anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
//        }

//        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
//        {
//            var token = await _localStorage.GetItemAsync<string>("authToken");

//            if (string.IsNullOrWhiteSpace(token))
//                return _anonymous;

//            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

//            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt")));
//        }

//        public void NotifyUserLogin(string token)
//        {
//            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt"));
//            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
//            NotifyAuthenticationStateChanged(authState);
//        }

//        public void NotifyUserLogout()
//        {
//            var authState = Task.FromResult(_anonymous);
//            NotifyAuthenticationStateChanged(authState);
//        }

//        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
//        {
//            var payload = jwt.Split('.')[1];
//            var jsonBytes = ParseBase64WithoutPadding(payload);
//            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
//            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
//        }

//        private byte[] ParseBase64WithoutPadding(string base64)
//        {
//            switch (base64.Length % 4)
//            {
//                case 2: base64 += "=="; break;
//                case 3: base64 += "="; break;
//            }
//            return Convert.FromBase64String(base64);
//        }

//    }
//}