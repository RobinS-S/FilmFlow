using FilmFlow.Client.Auth.Interfaces;

namespace FilmFlow.Client.Auth
{
    public class AuthorizedHttpClient : IAuthorizedHttpClient
    {
        private readonly HttpClient _httpClient;

        public AuthorizedHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpClient Client => _httpClient;
    }
}