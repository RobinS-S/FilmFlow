using FilmFlow.Client.Auth.Interfaces;

namespace FilmFlow.Client.Auth
{
    public class AnonymousHttpClient : IAnonymousHttpClient
    {
        private readonly HttpClient _httpClient;

        public AnonymousHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient Client => _httpClient;
    }
}