namespace AnimeScraper.Codebase.Helpers.Exceptions;

public class InvalidRangeException : Exception
{
    public InvalidRangeException()
    {
    }

    public InvalidRangeException(string message) : base(message)
    {
    }

    public InvalidRangeException(string message, Exception inner) : base(message, inner)
    {
    }
}