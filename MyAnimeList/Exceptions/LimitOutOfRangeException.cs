namespace MyAnimeList.Exceptions;

public class LimitOutOfRangeException : Exception
{
    public LimitOutOfRangeException()
    {
    }

    public LimitOutOfRangeException(string message)
        : base(message)
    {
    }

    public LimitOutOfRangeException(string message, Exception inner)
        : base(message, inner)
    {
    }
}