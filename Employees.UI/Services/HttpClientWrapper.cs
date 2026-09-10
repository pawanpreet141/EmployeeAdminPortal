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
        public async Task<HttpResponseMessage>PostAsync<T>(string url, T data)
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

    }
}
