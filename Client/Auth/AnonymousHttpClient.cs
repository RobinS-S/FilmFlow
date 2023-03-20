using FilmFlow.Client.Auth.Interfaces;

namespace FilmFlow.Client.Auth
{
    public class AnonymousHttpClient : IAnonymousHttpClient
    {
        public AnonymousHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpClient Client { get; }
    }
}