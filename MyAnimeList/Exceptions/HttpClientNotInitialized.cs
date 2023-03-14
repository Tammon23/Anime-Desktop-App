namespace MyAnimeList.Exceptions;

[Serializable]
public class HttpClientNotInitialized : Exception
{
    public HttpClientNotInitialized()
    {
    }

    public HttpClientNotInitialized(string message)
        : base(message)
    {
    }

    public HttpClientNotInitialized(string message, Exception inner)
        : base(message, inner)
    {
    }
}