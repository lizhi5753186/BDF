using Bdf.Runtime.Session;

namespace Bdf.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for applicaion services
    /// </summary>
    public abstract class ApplicationService : BdfServiceBase, IApplicationService
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IBdfSession BdfSession { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            BdfSession = NullBdfSession.Instance;
        }
    }
}