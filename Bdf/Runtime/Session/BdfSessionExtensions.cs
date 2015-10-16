namespace Bdf.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="IBdfSession"/>.
    /// </summary>
    public static class BdfSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="BdfException"/> if <see cref="IBdfSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this IBdfSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new BdfException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }
    }
}