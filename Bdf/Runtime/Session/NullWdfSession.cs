namespace Bdf.Runtime.Session
{
    public class NullBdfSession : IBdfSession
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullBdfSession Instance { get { return SingletonInstance; } }
        private static readonly NullBdfSession SingletonInstance = new NullBdfSession();

        public long? UserId { get { return null; } }

        public long? ImpersonatorUserId { get { return null; } }

        private NullBdfSession()
        {
        }
    }
}