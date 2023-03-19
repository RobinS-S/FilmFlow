using FilmFlow.Client.Auth.Interfaces;

namespace FilmFlow.Client.Auth
{
    public class AuthorizedHttpClient : IAuthorizedHttpClient
    {
	    public AuthorizedHttpClient(HttpClient httpClient)
        {
            Client = httpClient;
        }

        public HttpClient Client { get; }
    }
}