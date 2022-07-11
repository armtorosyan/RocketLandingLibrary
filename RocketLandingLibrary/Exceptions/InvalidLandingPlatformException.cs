namespace RocketLandingLibrary.Exceptions
{
    public class InvalidLandingPlatformException : Exception
    {
        public InvalidLandingPlatformException()
        {
        }

        public InvalidLandingPlatformException(string message) : base(message)
        {
        }

        public InvalidLandingPlatformException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}
