namespace FilmFlow.Client.Auth.Interfaces
{
    public interface IAuthorizedHttpClient
    {
        HttpClient Client { get; }
    }
}
