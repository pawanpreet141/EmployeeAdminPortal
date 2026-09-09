namespace Employees.UI.Services
{
    public class HttpClientWrapper
    {
        private readonly HttpClient _httpClient;


        public HttpClientWrapper(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            return await _httpClient.GetFromJsonAsync<T>(url);
        }


        public async Task<HttpResponseMessage>PostAsync<T>(string url, T data)
        {
            return await _httpClient.PostAsJsonAsync(url, data);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string url, T data)
        {
            return await _httpClient.PutAsJsonAsync(url, data);
        }

        public async Task<HttpResponseMessage> DeleteAsync<T>(string url, T data)
        {
            return await _httpClient.DeleteAsync(url);
        }

        internal async Task PostAsync<T1, T2>(string v, T2 employee)
        {
            throw new NotImplementedException();
        }

        internal async Task PutAsync<T1, T2>(string v, T2 employee)
        {
            throw new NotImplementedException();
        }

        internal async Task DeleteAsync(string v)
        {
            throw new NotImplementedException();
        }
    }
}
