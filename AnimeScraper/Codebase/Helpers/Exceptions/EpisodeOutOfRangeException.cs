namespace AnimeScraper.Codebase.Helpers.Exceptions;

public class EpisodeOutOfRangeException : Exception
{
    public EpisodeOutOfRangeException()
    {
    }

    public EpisodeOutOfRangeException(string message) : base(message)
    {
    }

    public EpisodeOutOfRangeException(string message, Exception inner) : base(message, inner)
    {
    }
}