using System;

namespace MyAnimeList.Exceptions
{
    [Serializable]
    public class AuthenticationUrlIncomplete : Exception
    {
        public AuthenticationUrlIncomplete()
        {
        }

        public AuthenticationUrlIncomplete(string message) 
            : base(message)
        {
        }

        public AuthenticationUrlIncomplete(string message, Exception inner)
            : base(message, inner)
        {
        }

    }
}